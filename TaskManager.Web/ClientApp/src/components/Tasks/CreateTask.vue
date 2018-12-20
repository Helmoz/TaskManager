<template>
  <v-container
    class="pl-0 pr-0 pb-0 pt-0"
    :style="{height: $vuetify.breakpoint.mdAndUp ? '100%' : '0px'}"
  >
    <create-task-form
      :current-project="currentProject"
      class="pl-4 pr-4 pb-4 pt-4"
      v-if="$vuetify.breakpoint.mdAndUp"
    ></create-task-form>
    <v-bottom-sheet v-model="sheet" v-if="$vuetify.breakpoint.smAndDown">
      <v-btn
        slot="activator"
        fixed
        bottom
        right
        color="primary"
        dark
        fab
        style="margin-bottom: 60px"
      >
        <v-icon dark>add</v-icon>
      </v-btn>
      <v-list>
        <v-container grid-list-xs>
          <create-task-form class="pl-4 pr-4 pb-0 pt-0" :current-project="currentProject"></create-task-form>
        </v-container>
      </v-list>
    </v-bottom-sheet>
  </v-container>
</template>

<script>
import CreateTaskForm from './CreateTaskForm.vue'

export default {
  props: ['currentProject'],
  components: {
    CreateTaskForm
  },
  data() {
    return {
      sheet: false
    }
  }
}
</script>

