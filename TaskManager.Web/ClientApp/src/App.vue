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
				<v-list-tile v-if="userIsAuthenticated" @click="onLogout">
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
			<v-toolbar-title>
				<router-link to="/" tag="span" style="cursor: pointer">Task Manager</router-link>
			</v-toolbar-title>
			<v-spacer></v-spacer>
			<v-toolbar-items class="hidden-xs-only">
				<v-btn flat v-for="item in menuItems" :key="item.title" :to="item.link">
					<v-icon left>{{item.icon}}</v-icon>
					{{item.title}}
				</v-btn>
				<v-btn flat v-if="userIsAuthenticated" @click="onLogout">
					<v-icon left>exit_to_app</v-icon>Выйти
				</v-btn>
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
    menuItems() {
      let menuItems = [
        { icon: 'lock_open', title: 'Авторизация', link: '/signin' }
      ]
      if (this.userIsAuthenticated) {
        menuItems = [{ icon: 'vpn_key', title: 'Задачи', link: '/tasks' }]
      }
      return menuItems
    }
  },
  methods: {
    onLogout() {
      this.$store.dispatch('logoutUser')
      this.$router.push('/signin')
    }
  }
}
</script>
