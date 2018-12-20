<template>
  <v-layout
    row
    wrap
    class="grey lighten-3 pt-2 pb-2"
    :class="{'pl-4 pr-4 pb-4 pt-4' : $vuetify.breakpoint.smAndUp}"
  >
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
            {{countOfTasksEnding}}
          </v-chip>
        </v-list-tile-action>
      </v-list-tile>
    </v-flex>
  </v-layout>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'ProjectInfo',
  props: ['currentProject'],
  data() {
    return {}
  },
  computed: {
    ...mapGetters(['typeIcons']),
    countOfTasksEnding() {
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
  }
}
</script>


