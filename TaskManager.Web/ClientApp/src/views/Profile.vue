<template>
	<v-container grid-list-xs class="user_container">
		<v-layout row wrap="" justify-center>
			<v-flex xs12 sm8 lg6 xl6>
				<v-layout row wrap="">
					<v-flex xs4 lg3 class="user">
						<img :src="user.photo" alt="" class="user_avatar">
					</v-flex>
					<v-flex xs8 lg9 class="user_info">
						<h2>{{user.name}}</h2>
					</v-flex>
				</v-layout>
			</v-flex>
		</v-layout>
	</v-container>
</template>


<script>
import { mapGetters } from 'vuex'

export default {
  data() {
    return {}
  },
  computed: {
    ...mapGetters(['user'])
  }
}
</script>

<style lang="stylus" scoped>
.user
	&_container
		margin-top 35px
	&_avatar
		width 100%
		border-radius 50%
		border 1px solid rgba(0,0,0, .3)
	&_info
		padding-left 50px !important
</style>

