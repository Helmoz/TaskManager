<template>
  <v-container grid-list-xs class="pr-0 pl-0 pt-0">
    <v-layout row wrap class="pl-4 pr-4 pb-4 pt-4 grey lighten-3">
      <v-flex xs12>
        <v-list-tile>
          <v-list-tile-avatar>
            <v-icon v-html="typeIcons[currentProject.type]"></v-icon>
          </v-list-tile-avatar>
          <v-list-tile-content>
            <v-list-tile-title>{{currentProject.name }}</v-list-tile-title>
            <v-list-tile-sub-title>{{currentProject.description}}</v-list-tile-sub-title>
          </v-list-tile-content>
          <v-list-tile-action v-if="currentProject.type===1">
            <v-progress-circular
              :rotate="360"
              :size="40"
              :width="5"
              :value="currentProject.progress"
              color="teal"
            >
              <small>{{ currentProject.progress }}</small>
            </v-progress-circular>
          </v-list-tile-action>
          <v-list-tile-action class="ml-4">
            <v-chip color="primary" text-color="white">
              <v-avatar class="secondary">{{currentProject.tasks.length}}</v-avatar>
              {{getNumEnding}}
            </v-chip>
          </v-list-tile-action>
        </v-list-tile>
      </v-flex>
    </v-layout>
    <v-divider v-if="$vuetify.breakpoint.mdAndUp"></v-divider>
    <v-layout row wrap class="pl-4 pr-4 pb-4 pt-4" v-if="$vuetify.breakpoint.mdAndUp">
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
          >
            <template slot="selection" slot-scope="data">
              <v-chip
                :selected="data.selected"
                close
                class="chip--select-multi"
                @input="removeAssigned(data.item)"
              >
                <v-avatar>
                  <img :src="data.item.photoUrl">
                </v-avatar>
                {{ data.item.name }}
              </v-chip>
            </template>
            <template slot="item" slot-scope="data">
              <template v-if="typeof data.item !== 'object'">
                <v-list-tile-content v-text="data.item"></v-list-tile-content>
              </template>
              <template v-else>
                <v-list-tile-avatar>
                  <img :src="data.item.photoUrl">
                </v-list-tile-avatar>
                <v-list-tile-content>
                  <v-list-tile-title v-html="data.item.name"></v-list-tile-title>
                </v-list-tile-content>
              </template>
            </template>
          </v-autocomplete>
          <v-flex xs12 sm4>
            <v-text-field value="Тэги" readonly prepend-icon="turned_in"></v-text-field>
          </v-flex>

          <v-item-group multiple class="text-xs-left mt-2" ref="itemGroup">
            <v-item
              v-for="tag in currentProject.tags"
              :key="tag.name"
              active-class="primary"
              :value="tag"
            >
              <v-chip
                small
                disabled
                slot-scope="{ active, toggle }"
                :selected="active"
                @click="toggle"
                class="pr-2 pb-2 pt-3 pb-3 white--text grey"
              ># {{tag.name}}</v-chip>
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
                class="pr-2 pb-2 pt-3 pb-3 white--text grey"
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
            @click="onSubmit"
          >Создать</v-btn>
        </v-form>
      </v-flex>
    </v-layout>
    <v-divider v-if="$vuetify.breakpoint.mdAndUp"></v-divider>
    <v-container>
      <v-flex xs12 class="text-xs-center pt-2 pb-2" v-if="loading">
        <v-progress-circular indeterminate color="primary" :width="7" :size="70"></v-progress-circular>
      </v-flex>
      <v-text-field v-model="search" placeholder="Search" append-icon="search" solo v-if="!loading"></v-text-field>
      <!-- <v-data-iterator
        :items="filteredProjects"
        :rows-per-page-items="rowsPerPageItems"
        rows-per-page-text="Элементов на странице:"
        :pagination.sync="pagination"
        content-tag="v-layout"
        row
        wrap
        v-if="!loading"
      >
        <v-flex slot="item" slot-scope="props" xs12>
          <v-list two-line>
            <v-list-tile @click.prevent="setProjectToTasks(props.item)" ripple>
              <v-list-tile-avatar>
                <v-icon v-html="typeIcons[props.item.type]"></v-icon>
              </v-list-tile-avatar>
              <v-list-tile-content>
                <v-list-tile-title>{{ props.item.name }}</v-list-tile-title>
                <v-list-tile-sub-title>{{props.item.description}}</v-list-tile-sub-title>
              </v-list-tile-content>
              <v-list-tile-action v-if="props.item.type===1">
                <v-progress-circular
                  :rotate="360"
                  :size="40"
                  :width="5"
                  :value="props.item.progress"
                  color="teal"
                >
                  <small>{{ props.item.progress }}</small>
                </v-progress-circular>
              </v-list-tile-action>
              <v-list-tile-action>
                <v-btn icon ripple @click.prevent="setProject(props.item)">
                  <v-icon color="grey lighten-1">info</v-icon>
                </v-btn>
              </v-list-tile-action>
            </v-list-tile>
          </v-list>
        </v-flex>
      </v-data-iterator>-->
    </v-container>
  </v-container>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import { required, maxLength } from 'vuelidate/lib/validators'
export default {
  validations: {
    name: { required, maxLength: maxLength(80) }
  },
  data() {
    return {
      rowsPerPageItems: [3, 6, 12],
      pagination: {
        rowsPerPage: 3
      },
      search: '',
      name: '',
      assign: [],
      items: [
        {
          name: 'Foo',
          avatar: 'https://cdn.vuetifyjs.com/images/lists/1.jpg'
        },
        {
          name: 'Bar',
          avatar: 'https://cdn.vuetifyjs.com/images/lists/2.jpg'
        }
      ],
      specialTags: [{ name: 'Priority', activeColor: 'error', icon: 'warning' }, { name: 'Suggestion', activeColor: 'accent', icon: 'info' }]
    }
  },
  computed: {
    ...mapGetters([, 'loading', 'currentProject', 'typeIcons']),
    getNumEnding() {
      var words = ['Задача', 'Задачи', 'Задач']
      var count = this.currentProject.tasks.length % 100
      if (count >= 11 && count <= 19) {
        return words[2]
      } else {
        switch (count % 10) {
          case 1:
            return words[0]
            break
          case 2:
          case 3:
          case 4:
            return words[1]
            break
          default:
            return words[2]
        }
      }
    }
  },
  methods: {
    ...mapActions([]),
    onSubmit() {},
    removeAssigned(member) {
      this.assign.splice(this.assign.indexOf(member), 1)
      this.assign = [...this.assign]
    },
    test() {
      console.log(this.$refs['itemGroup'].selectedItems.map(x => x.value))
    }
  }
}
</script>

