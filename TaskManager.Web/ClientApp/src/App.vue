<template>
	<v-app>
		<Navigation></Navigation>
		<v-content>
			<transition name="slide-fade" mode="out-in">
				<router-view></router-view>
			</transition>
		</v-content>
	</v-app>
</template>

<script>
import Navigation from './components/Navigation.vue'
export default {
  name: 'App',
  components: {
    Navigation
  },
  data() {
    return {}
  }
}
</script>


<style lang="stylus" scoped>
@font-face 
	font-family 'Billabong'
	src url('./assets/fonts/Billabong.eot')
	src url('./assets/fonts/Billabong.woff2') format('woff2'),
        url('./assets/fonts/Billabong.woff') format('woff'),
        url('./assets/fonts/Billabong.ttf') format('truetype')
	font-weight normal
	font-style normal

.slide-fade-enter-active 
	transition all .5s cubic-bezier(1.0, 0.5, 0.8, 1.0)

.slide-fade-leave-active 
	transition all .5s cubic-bezier(1.0, 0.5, 0.8, 1.0)

.slide-fade-enter, .slide-fade-leave-to 
	transform translateX(10px)
	opacity 0
</style>

