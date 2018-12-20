<template>
  <v-container>
    <v-flex xs12 class="text-xs-center pt-2 pb-2" v-if="loading">
      <v-progress-circular indeterminate color="primary" :width="7" :size="70"></v-progress-circular>
    </v-flex>
    <v-text-field v-model="search" placeholder="Search" append-icon="search" solo v-if="!loading"></v-text-field>
    <v-data-iterator
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
          <transition-group
            name="ListOfProjects"
            v-bind:css="false"
            v-on:before-enter="beforeEnter"
            v-on:enter="MembersEnter"
            v-on:leave="MembersLeave"
          >
            <v-list-tile
              @click="setProjectToTasks(props.item)"
              ripple
              :key="props.item.name"
              :to="`/tasks`"
            >
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
                  color="primary"
                >
                  <small>{{ props.item.progress }}</small>
                </v-progress-circular>
              </v-list-tile-action>
              <v-list-tile-action @click.prevent="setProject(props.item)">
                <v-btn icon ripple>
                  <v-icon color="grey lighten-1">info</v-icon>
                </v-btn>
              </v-list-tile-action>
            </v-list-tile>
          </transition-group>
        </v-list>
      </v-flex>
    </v-data-iterator>
  </v-container>
</template>


<script>
import 'velocity-animate'
import { mapGetters, mapActions } from 'vuex'
export default {
  data() {
    return {
      rowsPerPageItems: [3, 6, 12],
      pagination: {
        rowsPerPage: 3
      },
      search: ''
    }
  },
  computed: {
    ...mapGetters(['projects', 'loading', 'typeIcons', 'user']),
    filteredProjects() {
      return this.projects.filter(project => {
        return project.name.toLowerCase().indexOf(this.search.toLowerCase()) !== -1
      })
    }
  },
  methods: {
    ...mapActions(['setCurrentProject']),
    setProject(item) {
      this.setCurrentProject(item)
      this.$router.push('/project')
    },
    filter(item) {
      let task = []
      item.tasks.forEach(el => {
        el.assignedTo.forEach(elemenet => {
          if (elemenet.member.uId == this.user.id) task.push(el)
        })
      })
      return task.length
    },
    beforeEnter: function(el) {
      el.style.opacity = 0
      el.style.height = 0
    },
    MembersEnter: function(el, done) {
      Velocity(el, { opacity: 1, height: '72px' }, { complete: done })
    },
    MembersLeave: function(el, done) {
      Velocity(el, { opacity: 0, height: 0 }, { complete: done })
    },
    setProjectToTasks(item) {
      this.setCurrentProject(item)
    }
  }
}
</script>

