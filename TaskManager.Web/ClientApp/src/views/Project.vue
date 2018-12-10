<template>
  <v-container class="pb-5">
    <v-layout row wrap>
      <v-flex xs12>
        <v-layout row wrap>
          <v-flex xs6>
            <v-text-field
              v-model="name"
              label="Имя проекта"
              counter="80"
              required
              :error-messages="nameErrors"
              @input="$v.name.$touch()"
              @blur="$v.name.$touch()"
            ></v-text-field>
          </v-flex>
          <v-flex xs12>
            <v-textarea label="Описание" v-model="description" counter="300"></v-textarea>
          </v-flex>
          <v-flex xs12>
            <v-layout align-end justify-space-between v-if="type===1">
              <v-flex xs6>
                <v-subheader class="pl-0">Выполнено на {{progress}}%</v-subheader>
                <v-slider v-model="progress" class thumb-label></v-slider>
              </v-flex>
              <v-flex xs5>
                <v-subheader class="pl-0">Дедлайн</v-subheader>
                <v-menu
                  :close-on-content-click="false"
                  v-model="datePicker"
                  :nudge-right="40"
                  lazy
                  transition="scale-transition"
                  offset-y
                  full-width
                  min-width="290px"
                >
                  <v-text-field
                    slot="activator"
                    v-model="deadline"
                    prepend-icon="event"
                    class
                    readonly
                  ></v-text-field>
                  <v-date-picker v-model="deadline" @input="datePicker = false"></v-date-picker>
                </v-menu>
              </v-flex>
            </v-layout>
          </v-flex>
          <v-flex xs12>
            <v-layout row wrap>
              <v-flex xs12 sm8>
                <v-subheader class="pl-0">Тип проекта</v-subheader>
                <v-layout row wrap>
                  <v-flex xs1 class="mr-4">
                    <v-icon large v-html="typeIcons[type]"></v-icon>
                  </v-flex>
                  <v-flex xs9 sm7>
                    <v-select
                      :items="types"
                      v-model="type"
                      item-value="type"
                      item-text="name"
                      class="pt-0 mt-0"
                    ></v-select>
                  </v-flex>
                </v-layout>
              </v-flex>
            </v-layout>
          </v-flex>
          <v-flex xs12>
            <v-layout row wrap>
              <v-flex xs12 sm4>
                <v-text-field
                  v-model="tag.name"
                  label="Добавить тэг"
                  append-icon="add"
                  @click:append="addTag"
                  @keyup.enter="addTag"
                  prefix="#"
                  counter="20"
                  required
                  @input="$v.tag.$touch()"
                  @blur="$v.tag.$touch()"
                  :error-messages="tagErrors"
                ></v-text-field>
              </v-flex>
              <v-flex xs12 sm7 class="ml-2">
                <v-item-group multiple>
                  <v-item v-for="tag in tags" :key="tag.name">
                    <v-chip
                      slot-scope="{ active, toggle }"
                      close
                      @input="removeTag(tag)"
                    ># {{tag.name}}</v-chip>
                  </v-item>
                </v-item-group>
              </v-flex>
            </v-layout>
          </v-flex>
          <v-flex xs12 class="mt-3">
            <v-subheader class="pl-0">Участники
              <v-chip class="ml-3" label outline color="primary">{{members ? members.length : '0'}}</v-chip>
              <v-btn round outline color="primary" @click="show = !show">Добавить участника</v-btn>
            </v-subheader>
            <v-list two-line v-if="members">
              <transition-group
                name="list"
                v-bind:css="false"
                v-on:before-enter="beforeEnter"
                v-on:enter="enter"
                v-on:leave="leave"
              >
                <v-list-tile
                  v-for="(item, index) in members"
                  :key="item.name"
                  avatar
                  v-bind:data-index="index"
                >
                  <v-list-tile-avatar>
                    <img :src="item.photoUrl">
                  </v-list-tile-avatar>

                  <v-list-tile-content>
                    <v-list-tile-title>{{ item.name }}</v-list-tile-title>
                    <v-list-tile-sub-title>{{ item.email }}</v-list-tile-sub-title>
                  </v-list-tile-content>

                  <v-list-tile-action>
                    <v-btn icon ripple @click="removeMember(item)">
                      <v-icon color="grey lighten-1">delete</v-icon>
                    </v-btn>
                  </v-list-tile-action>
                </v-list-tile>
              </transition-group>
            </v-list>
          </v-flex>
          <transition
            name="staggered-fade"
            v-bind:css="false"
            v-on:before-enter="beforeEnter"
            v-on:enter="enter"
            v-on:leave="leave"
          >
            <v-flex xs12 class="mt-3" v-if="show">
              <v-subheader class="pl-0">Добавить участника</v-subheader>
              <v-flex xs12 sm6>
                <v-text-field
                  v-model="email"
                  append-icon="search"
                  label="Email"
                  @keyup.enter="loadUser"
                  @click:append="loadUser"
                ></v-text-field>
                <transition
                  name="staggered-fade"
                  v-bind:css="false"
                  v-on:before-enter="beforeEnter"
                  v-on:enter="enter"
                  v-on:leave="leave"
                >
                  <v-list-tile avatar @click v-if="user">
                    <v-list-tile-avatar>
                      <img :src="user.photoUrl">
                    </v-list-tile-avatar>

                    <v-list-tile-content>
                      <v-list-tile-title v-html="user.name"></v-list-tile-title>
                    </v-list-tile-content>

                    <v-list-tile-action @click="addMember">
                      <v-icon color="primary">add</v-icon>
                    </v-list-tile-action>
                  </v-list-tile>

                  <v-list-tile avatar v-else-if="user == '' && email != ''">
                    <v-list-tile-content>
                      <v-list-tile-title>Участник с таким Email не найден</v-list-tile-title>
                    </v-list-tile-content>
                  </v-list-tile>
                </transition>
              </v-flex>
            </v-flex>
          </transition>
          <v-btn
            class="mt-4"
            color="success"
            block
            depressed
            :disabled="$v.$invalid"
            @click="saveChanges()"
          >
            <v-icon class="mr-2">save</v-icon>Сохранить изменения
          </v-btn>
        </v-layout>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import 'velocity-animate'
import { mapGetters, mapActions } from 'vuex'
import { required, maxLength } from 'vuelidate/lib/validators'
export default {
  validations: {
    tag: { required, maxLength: maxLength(20) },
    name: { required, maxLength: maxLength(80) }
  },
  data() {
    return {
      name: '',
      description: '',
      progress: null,
      deadline: null,
      datePicker: false,
      type: null,
      types: [
        { name: 'Идея', type: 0 },
        { name: 'В разработке', type: 1 },
        { name: 'Завершен', type: 2 }
      ],
      members: null,
      tag: { name: '' },
      tags: [],
      email: '',
      show: false,
      user: null
    }
  },
  computed: {
    ...mapGetters(['currentProject', 'typeIcons']),
    tagErrors() {
      const errors = []
      if (!this.$v.tag.$dirty) return errors
      !this.$v.tag.maxLength && errors.push('Слишком длинный тэг')
      !this.$v.tag.required && errors.push('Введите тэг')
      this.tags.map(x => x.name).indexOf(this.tag.name) != -1 &&
        errors.push('Данный тэг уже добавлен')
      return errors
    },
    nameErrors() {
      const errors = []
      if (!this.$v.name.$dirty) return errors
      !this.$v.name.maxLength && errors.push('Слишком длинное имя')
      !this.$v.name.required && errors.push('Это поле обязательно к заполнению.')
      return errors
    }
  },
  methods: {
    ...mapActions(['editProject', 'loadUserByEmail', 'addMemberToProject']),
    addTag() {
      if (
        this.tag.name != '' &&
        this.tag.name.length <= 20 &&
        this.tags.map(x => x.name).indexOf(this.tag.name) == -1
      ) {
        this.tags.push({ name: this.tag.name })
        this.tag.name = ''
      }
    },
    removeTag(tag) {
      this.tags.splice(this.tags.indexOf(tag), 1)
      this.tags = [...this.tags]
    },
    removeMember(member) {
      this.members.splice(this.members.indexOf(member), 1)
      this.members = [...this.members]
    },
    saveChanges() {
      var date = new Date(this.deadline)
      date.setHours(10, 10, 10)
      if (this.progress === 100) this.type = 2
      const project = {
        id: this.currentProject.id,
        name: this.name,
        description: this.description,
        progress: this.progress,
        deadline: date,
        type: this.type,
        tags: this.tags,
        members: this.members
      }
      this.editProject(project)
    },
    loadUser() {
      this.loadUserByEmail(this.email).then(user => {
        this.user = user.data
      })
    },
    addMember() {
      this.members.push(this.user)
      this.email = ''
    },
    beforeEnter: function(el) {
      el.style.opacity = 0
      el.style.height = 0
    },
    enter: function(el, done) {
      var delay = el.dataset.index * 150
      setTimeout(function() {
        Velocity(el, { opacity: 1, height: '100%' }, { complete: done })
      }, delay)
    },
    leave: function(el, done) {
      var delay = el.dataset.index * 150
      setTimeout(function() {
        Velocity(el, { opacity: 0, height: 0 }, { complete: done })
      }, delay)
    }
  },
  mounted() {
    this.name = this.currentProject.name
    this.description = this.currentProject.description
    this.progress = this.currentProject.progress
    this.type = this.currentProject.type
    this.deadline = new Date(this.currentProject.deadline).toISOString().substr(0, 10)
    this.tags = this.currentProject.tags
    this.members = this.currentProject.members
  },
  created() {}
}
</script>



