<template>
	<v-container grid-list-xs fluid style="height: 100%">
		<v-layout row wrap="" style="height: 75%" justify-center align-center>
			<v-flex xs12 sm6 md5 lg3 xl3 :style="flexBasisOnXl()">
				<SignInCard></SignInCard>
			</v-flex>
		</v-layout>
	</v-container>
</template>

<script>
import SignInCard from '../components/SignInCard.vue'
export default {
  data() {
    return {}
  },
  components: {
    SignInCard
  },
  methods: {
    flexBasisOnXl() {
      if (this.$vuetify.breakpoint.xl) return { 'flex-basis': '19%' }
    }
  }
}
</script>