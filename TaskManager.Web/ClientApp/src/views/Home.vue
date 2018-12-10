<template>
  <v-container grid-list-xs class="pr-0 pl-0">
    <CreateProject></CreateProject>
    <v-divider v-if="$vuetify.breakpoint.mdAndUp"></v-divider>
    <ListOfProjects></ListOfProjects>
    <BottomSheet></BottomSheet>
  </v-container>
</template>

<script>
import { mapActions } from 'vuex'

import CreateProject from '../components/Projects/CreateProject.vue'
import ListOfProjects from '../components/Projects/ListOfProjects.vue'
import BottomSheet from '../components/Projects/BottomSheet.vue'

export default {
  components: {
    CreateProject,
    ListOfProjects,
    BottomSheet
  },
  methods: {
    ...mapActions(['loadProjects'])
  },
  created() {
    this.loadProjects()
  }
}
</script>

