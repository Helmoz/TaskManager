<template>
  <v-container grid-list-xs style="height: 100%">
    <v-layout row wrap justify-center align-center style="height: 100%">
      <v-flex xs12 lg10 xl7>
        <v-container grid-list-xs>
          <v-layout row wrap justify-center align-center>
            <v-flex v-if="showImage" lg6>
              <img src="../assets/iphones.png" alt>
            </v-flex>
            <v-flex xs12 sm6 md5 lg6 :style="flexBasisOnXl()">
              <RegistrationCard></RegistrationCard>
            </v-flex>
          </v-layout>
        </v-container>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import RegistrationCard from '../components/Authentication/RegistrationCard.vue'

export default {
  components: {
    RegistrationCard
  },
  computed: {
    showImage() {
      return this.$vuetify.breakpoint.lgAndUp
    }
  },
  methods: {
    flexBasisOnXl() {
      if (this.$vuetify.breakpoint.lgAndUp) return { 'flex-basis': '50%', 'max-width': '40%' }
    }
  }
}
</script>