<template>
	<div>
		<v-navigation-drawer floating fixed app mobile-break-point="960">
			<v-toolbar color="primary" extended flat dark>
				<v-toolbar-title>
					<router-link to="/" tag="span" style="cursor: pointer" class="logo">TaskManager</router-link>
				</v-toolbar-title>
				<v-spacer></v-spacer>
			</v-toolbar>
			<v-list subheader>
				<!--  -->
				<v-list-tile avatar>
					<v-list-tile-avatar>
						<img :src="user.photo">
					</v-list-tile-avatar>
					<v-list-tile-content>
						<v-list-tile-title v-html="user.name"></v-list-tile-title>
					</v-list-tile-content>
					<v-list-tile-action>
						<v-tooltip right>
							<v-btn icon slot="activator" @click="onSignOut()">
								<v-icon dark>power_settings_new</v-icon>
							</v-btn>
							<span>Выйти</span>
						</v-tooltip>
					</v-list-tile-action>
				</v-list-tile>
				<!--  -->
				<v-divider></v-divider>
				<v-subheader>Мои проекты</v-subheader>
				<!--  -->
				<v-list-tile avatar :to="`/`">
					<v-list-tile-avatar>
						<v-icon>star</v-icon>
					</v-list-tile-avatar>
					<v-list-tile-content>
						<v-list-tile-title>Проекты</v-list-tile-title>
					</v-list-tile-content>
				</v-list-tile>
				<!--  -->
				<v-subheader>Задачи</v-subheader>
				<!--  -->
				<v-list-tile v-for="(item, index) in mainMenu" :key="item.title" avatar :to="item.link">
					<v-list-tile-avatar>
						<v-badge overlap color="primary" v-if="index === 1">
							<span slot="badge">0</span>
							<v-icon v-html="item.icon"></v-icon>
						</v-badge>
						<v-icon v-else v-html="item.icon"></v-icon>
					</v-list-tile-avatar>
					<v-list-tile-content>
						<v-list-tile-title v-html="item.title"></v-list-tile-title>
					</v-list-tile-content>
				</v-list-tile>
				<v-divider></v-divider>
				<!--  -->
				<v-list-tile v-for="(item) in subMenu" :key="item.title" avatar :to="item.link">
					<v-list-tile-avatar>
						<v-icon v-html="item.icon"></v-icon>
					</v-list-tile-avatar>
					<v-list-tile-content>
						<v-list-tile-title v-html="item.title"></v-list-tile-title>
					</v-list-tile-content>
				</v-list-tile>
				<!--  -->
			</v-list>
		</v-navigation-drawer>
	</div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
export default {
  name: 'SideNavigation',
  data() {
    return {
      drawer: true
    }
  },
  computed: {
    ...mapGetters(['user', 'subMenu', 'mainMenu'])
  },
  methods: {
    ...mapActions(['signOut']),
    onSignOut() {
      this.signOut()
      this.$router.push('/authentication')
    }
  }
}
</script>

<style lang="stylus" scoped>
.logo 
	font-family Billabong
	font-size 40px
	margin-bottom 10px

</style>
