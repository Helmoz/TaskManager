<template>
  <v-container>
    <v-data-iterator
      :items="filteredTasks"
      :rows-per-page-items="rowsPerPageItems"
      rows-per-page-text="Элементов на странице:"
      :pagination.sync="pagination"
      content-tag="v-layout"
      row
      wrap
      v-if="!loading"
    >
      <v-flex slot="item" slot-scope="props" xs12 md6>
        <v-list two-line>
          <transition-group
            name="ListOfProjects"
            v-bind:css="false"
            v-on:before-enter="beforeEnter"
            v-on:enter="MembersEnter"
            v-on:leave="MembersLeave"
          >
            <v-list-tile ripple @click="dialog = true; setTask(props.item)" :key="props.item.name">
              <v-list-tile-action>
                <v-icon v-html="icon(props.item.isCompleted)"></v-icon>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>{{ props.item.name }}</v-list-tile-title>
                <v-list-tile-sub-title>
                  <span v-for="tag in props.item.tags">#{{tag.tag.name+' '}}</span>
                </v-list-tile-sub-title>
              </v-list-tile-content>
              <v-list-tile-avatar style="min-width: 40px" v-if="props.item.isPriority">
                <v-icon color="red">info</v-icon>
              </v-list-tile-avatar>
              <v-list-tile-avatar style="min-width: 40px" v-if="props.item.isSpecial">
                <v-icon color="warning">warning</v-icon>
              </v-list-tile-avatar>
            </v-list-tile>
          </transition-group>
        </v-list>
      </v-flex>
    </v-data-iterator>
    <task-dialog v-if="currentTask" :dialog.sync="dialog"></task-dialog>
  </v-container>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'

import TaskDialog from '../components/Task/TaskDialog.vue'

export default {
  components: {
    TaskDialog
  },
  data() {
    return {
      rowsPerPageItems: [10, 14, 18],
      pagination: {
        rowsPerPage: 10
      },
      dialog: false
    }
  },
  computed: {
    ...mapGetters(['loading', 'currentProject', 'user', 'currentTask']),
    filteredTasks() {
      return this.currentProject.tasks.filter(task => {
        return task.isCompleted
      })
    }
  },
  methods: {
    ...mapActions(['setCurrentTask']),
    icon(completed) {
      if (completed) return 'check_box'
      else return 'check_box_outline_blank'
    },
    setTask(task) {
      this.setCurrentTask(task)
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
    }
  }
}
</script>

