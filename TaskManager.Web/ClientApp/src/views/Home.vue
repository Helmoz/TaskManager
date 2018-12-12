<template>
  <v-container grid-list-xs class="pr-0 pl-0">
    <create-project></create-project>
    <v-divider v-if="$vuetify.breakpoint.mdAndUp"></v-divider>
    <list-of-projects></list-of-projects>
  </v-container>
</template>

<script>
import { mapActions } from 'vuex'

import CreateProject from '../components/Home/CreateProject.vue'
import ListOfProjects from '../components/Home/ListOfProjects.vue'

export default {
  components: {
    CreateProject,
    ListOfProjects
  },
  methods: {
    ...mapActions(['loadProjects'])
  },
  created() {
    this.loadProjects()
  }
}
</script>

