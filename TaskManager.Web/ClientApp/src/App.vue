<template>
	<v-app>
		<v-navigation-drawer v-model="sideNav" absolute temporary>
			<v-list class="pt-0">
				<v-list-tile v-for="item in menuItems" :key="item.title" @click :to="item.link">
					<v-list-tile-action>
						<v-icon>{{ item.icon }}</v-icon>
					</v-list-tile-action>
					<v-list-tile-content>
						<v-list-tile-title>{{ item.title }}</v-list-tile-title>
					</v-list-tile-content>
				</v-list-tile>
				<v-list-tile @click="onLogout">
					<v-list-tile-action>
						<v-icon>exit_to_app</v-icon>
					</v-list-tile-action>
					<v-list-tile-content>
						<v-list-tile-title>Выйти</v-list-tile-title>
					</v-list-tile-content>
				</v-list-tile>
			</v-list>
		</v-navigation-drawer>
		<v-toolbar dark class="primary">
			<v-toolbar-side-icon @click="sideNav = !sideNav" class="hidden-sm-and-up"></v-toolbar-side-icon>
			<v-toolbar-title class="title">
				<router-link to="/" tag="span" style="cursor: pointer" class="logo">TaskManager</router-link>
			</v-toolbar-title>
			<v-spacer></v-spacer>
			<v-toolbar-items class="hidden-xs-only">
				<v-btn flat v-for="item in menuItems" :key="item.title" :to="item.link">
					<v-icon left>{{item.icon}}</v-icon>
					{{item.title}}
				</v-btn>
				<v-menu bottom offset-y v-if="user">
					<v-btn flat slot="activator">
						<v-icon left>person</v-icon>
						{{user.name}}
					</v-btn>
					<v-list style="width: 220px">
						<v-list-tile @click>
							<v-list-tile-action>
								<v-icon>account_circle</v-icon>
							</v-list-tile-action>
							<v-list-tile-content>
								<v-list-tile-title>Профиль</v-list-tile-title>
							</v-list-tile-content>
						</v-list-tile>
						<v-divider></v-divider>
						<v-list-tile @click="onLogout">
							<v-list-tile-action>
								<v-icon>exit_to_app</v-icon>
							</v-list-tile-action>
							<v-list-tile-content>
								<v-list-tile-title>Выйти</v-list-tile-title>
							</v-list-tile-content>
						</v-list-tile>
					</v-list>
				</v-menu>
			</v-toolbar-items>
		</v-toolbar>
		<main>
			<transition name="slide-fade" mode="out-in">
				<router-view></router-view>
			</transition>
		</main>
	</v-app>
</template>

<script>
export default {
  name: 'App',
  data() {
    return {
      sideNav: null
    }
  },
  computed: {
    userIsAuthenticated() {
      return this.$store.getters.user !== null
    },
    user() {
      return this.$store.getters.user
    },
    menuItems() {
      let menuItems = [{ icon: 'lock_open', title: 'Войти', link: '/signin' }]
      if (this.userIsAuthenticated) {
        menuItems = [{ icon: 'vpn_key', title: 'Задачи', link: '/tasks' }]
      }
      return menuItems
    }
  },
  methods: {
    onLogout() {
      this.$store.dispatch('logoutUser')
      this.$router.push('/authentication')
    }
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

.logo 
	font-family Billabong
	font-size 40px
	margin-bottom 10px

.title 
	display flex
	width 200px
	height 100%
	align-items flex-end
	justify-content center

.slide-fade-enter-active 
	transition all .5s cubic-bezier(1.0, 0.5, 0.8, 1.0)

.slide-fade-leave-active 
	transition all .5s cubic-bezier(1.0, 0.5, 0.8, 1.0)

.slide-fade-enter, .slide-fade-leave-to 
	transform translateX(10px)
	opacity 0

</style>

