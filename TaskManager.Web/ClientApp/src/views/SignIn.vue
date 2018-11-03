<template>
	<v-container grid-list-xs fluid style="height: 100%">
		<v-layout row wrap="" style="height: 70%" justify-center align-center>
			<v-flex xs12 sm6 md5 lg3 xl3 :style="flexBasisOnXl()">
				<LoginCard></LoginCard>
			</v-flex>
		</v-layout>
	</v-container>
</template>

<script>
import LoginCard from '../components/LoginCard.vue'
export default {
  data() {
    return {}
  },
  components: {
    LoginCard
  },
  methods: {
    flexBasisOnXl() {
      if (this.$vuetify.breakpoint.xl) return { 'flex-basis': '19%' }
    }
  }
}
</script>

<style lang="stylus" scoped>

</style>
