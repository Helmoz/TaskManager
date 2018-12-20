<template>
  <v-layout row wrap>
    <v-flex xs12>
      <v-form lazy-validation class="text-xs-right">
        <v-text-field
          v-model="name"
          label="Имя задачи"
          prepend-icon="playlist_add"
          clearable
          counter="80"
          required
          @input="$v.name.$touch()"
          @blur="$v.name.$touch()"
          @keyup.enter="onSubmit"
        ></v-text-field>
        <v-autocomplete
          v-model="assign"
          :items="currentProject.members"
          chips
          prepend-icon="face"
          label="Задачи для"
          item-text="name"
          multiple
          return-object
        >
          <template slot="selection" slot-scope="data">
            <v-chip
              :selected="data.selected"
              close
              class="chip--select-multi"
              @input="removeAssigned(data.item)"
            >
              <v-avatar>
                <img :src="data.item.member.photoUrl">
              </v-avatar>
              {{ data.item.member.name }}
            </v-chip>
          </template>
          <template slot="item" slot-scope="data">
            <template v-if="typeof data.item !== 'object'">
              <v-list-tile-content v-text="data.item"></v-list-tile-content>
            </template>
            <template v-else>
              <v-list-tile-avatar>
                <img :src="data.item.member.photoUrl">
              </v-list-tile-avatar>
              <v-list-tile-content>
                <v-list-tile-title v-html="data.item.member.name"></v-list-tile-title>
              </v-list-tile-content>
            </template>
          </template>
        </v-autocomplete>

        <v-item-group multiple class="text-xs-left mt-2" ref="itemGroup">
          <v-item
            v-for="tag in currentProject.tags"
            :key="tag.tag.name"
            active-class="primary"
            :value="tag"
          >
            <v-chip
              small
              disabled
              slot-scope="{ active, toggle }"
              :selected="active"
              @click="toggle"
              class="pr-2 pl-2 pt-3 pb-3 white--text grey"
            ># {{tag.tag.name}}</v-chip>
          </v-item>
          <v-item
            v-for="tag in specialTags"
            :key="tag.name"
            :active-class="tag.activeColor"
            :value="tag"
          >
            <v-chip
              small
              disabled
              slot-scope="{ active, toggle }"
              :selected="active"
              @click="toggle"
              class="pr-2 pl-2 pt-3 pb-3 white--text grey"
            >
              {{tag.name}}
              <v-icon v-html="tag.icon" class="ml-2"></v-icon>
            </v-chip>
          </v-item>
        </v-item-group>
        <v-btn
          color="primary"
          class="mr-0"
          depressed
          :disabled="$v.$invalid"
          :loading="loading"
          @click="onSubmit"
        >Создать</v-btn>
      </v-form>
    </v-flex>
  </v-layout>
</template>

<script>
import { required, maxLength } from 'vuelidate/lib/validators'
import { mapGetters, mapActions } from 'vuex'
export default {
  props: ['currentProject'],
  validations: {
    name: { required, maxLength: maxLength(80) }
  },
  data() {
    return {
      name: '',
      assign: [],
      specialTags: [{ name: 'Важная', activeColor: 'error', icon: 'warning' }, { name: 'Специальная', activeColor: 'warning', icon: 'info' }]
    }
  },
  computed: {
    ...mapGetters(['loading', 'user']),
    tags() {
      return this.$refs['itemGroup'].selectedItems.map(x => x.value).filter(x => x.hasOwnProperty('id'))
    },
    isPriority() {
      return this.$refs['itemGroup'].selectedItems.map(x => x.value).filter(x => x.name == 'Важная').length === 1
    },
    isSpecial() {
      return this.$refs['itemGroup'].selectedItems.map(x => x.value).filter(x => x.name == 'Специальная').length === 1
    }
  },
  methods: {
    ...mapActions(['addTask']),
    onSubmit() {
      const task = {
        name: this.name,
        projectId: this.currentProject.id,
        details: '....',
        tags: this.tags,
        assignedTo: this.assign,
        creator: {
          Uid: this.user.id,
          Name: this.user.name,
          PhotoUrl: this.user.photoURL,
          Email: this.user.email
        },
        isCompleted: false,
        isPriority: this.isPriority,
        isSpecial: this.isSpecial
      }
      this.addTask(task)
      this.name = ''
      this.assign = []
      var items = this.$refs['itemGroup'].selectedValues
      items.splice(0, items.length)
    },
    removeAssigned(member) {
      this.assign.splice(this.assign.indexOf(member), 1)
      this.assign = [...this.assign]
    }
  }
}
</script>

