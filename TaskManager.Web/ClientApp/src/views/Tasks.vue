<template>
  <v-container grid-list-xs class="pr-0 pl-0 pt-0">
    <project-info :currentProject="currentProject"></project-info>
    <v-divider v-if="$vuetify.breakpoint.mdAndUp"></v-divider>
    <create-task :currentProject="currentProject"></create-task>
    <v-divider v-if="$vuetify.breakpoint.mdAndUp"></v-divider>
    <v-container>
      <v-flex xs12 class="text-xs-center pt-2 pb-2" v-if="loading">
        <v-progress-circular indeterminate color="primary" :width="7" :size="70"></v-progress-circular>
      </v-flex>
      <v-text-field v-model="search" placeholder="Search" append-icon="search" solo v-if="!loading"></v-text-field>
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
          </v-list>
        </v-flex>
      </v-data-iterator>
      <task-dialog v-if="currentTask" :dialog.sync="dialog" :name="currentTask.name"></task-dialog>
    </v-container>
  </v-container>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'

import ProjectInfo from '../components/Tasks/ProjectInfo.vue'
import CreateTask from '../components/Tasks/CreateTask.vue'
import TaskDialog from '../components/Task/TaskDialog.vue'

export default {
  components: {
    ProjectInfo,
    CreateTask,
    TaskDialog
  },
  data() {
    return {
      rowsPerPageItems: [6, 8, 12],
      pagination: {
        rowsPerPage: 6
      },
      search: '',
      dialog: false
    }
  },
  computed: {
    ...mapGetters(['loading', 'currentProject', 'currentTask']),
    filteredTasks() {
      return this.currentProject.tasks.filter(task => {
        return task.name.toLowerCase().indexOf(this.search.toLowerCase()) !== -1 && !task.isCompleted
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
    }
  }
}
</script>

