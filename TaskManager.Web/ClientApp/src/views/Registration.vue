<template>
  <v-container grid-list-xs style="height: 100%">
    <v-layout row wrap style="height: 85%" justify-center align-center>
      <v-flex xs12 sm6 md5 lg3 xl3 :style="flexBasisOnXl()">
        <registration-card></registration-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import RegistrationCard from '../components/Authentication/RegistrationCard.vue'
export default {
  data() {
    return {}
  },
  components: {
    RegistrationCard
  },
  methods: {
    flexBasisOnXl() {
      if (this.$vuetify.breakpoint.xl) return { 'flex-basis': '19%' }
    }
  }
}
</script>