<template>
	<div>
		<v-navigation-drawer
			persistent
			:mini-variant="miniVariant"
			clipped
			v-model="sideNavigation"
			disable-resize-watcher
			fixed
			app
			mobile-break-point="600"
			v-show="user"
		>
			<v-list style="height: 100%;">
				<v-list-tile v-for="(item, i) in items" :key="i" :to="item.link" :style="fullHeight()">
					<v-list-tile-action>
						<v-icon :style="iconSize()" v-html="item.icon"></v-icon>
					</v-list-tile-action>
					<v-list-tile-content>
						<v-list-tile-title v-text="item.title"></v-list-tile-title>
					</v-list-tile-content>
				</v-list-tile>
				<v-list-tile
					v-for="(item, i) in userItems"
					:key="i+items.length"
					:to="item.link"
					@click="item.method"
					class="hidden-sm-and-up"
				>
					<v-list-tile-action>
						<v-icon v-html="item.icon"></v-icon>
					</v-list-tile-action>
					<v-list-tile-content>
						<v-list-tile-title v-text="item.title"></v-list-tile-title>
					</v-list-tile-content>
				</v-list-tile>
			</v-list>
		</v-navigation-drawer>
		<v-toolbar app clipped-left dark class="primary">
			<v-toolbar-side-icon @click="sideNav = !sideNav" class="hidden-sm-and-up" v-if="user"></v-toolbar-side-icon>
			<v-toolbar-title>
				<router-link to="/" tag="span" style="cursor: pointer" class="logo">TaskManager</router-link>
			</v-toolbar-title>
			<v-spacer></v-spacer>
			<v-toolbar-items class="hidden-xs-only">
				<v-menu right left transition="slide-x-transition" offset-y v-if="user" v-model="menu">
					<v-btn flat slot="activator">
						{{user.name}}
						<v-avatar :size="40" style="margin-left: 20px; margin-right: 20px">
							<img :src="user.photo" alt="avatar">
						</v-avatar>
						<v-icon v-html="menu ? 'expand_less' : 'expand_more'"></v-icon>
					</v-btn>
					<v-list>
						<v-list-tile v-for="(item, i) in userItems" :key="i" :to="item.link" @click="item.method">
							<v-list-tile-action>
								<v-icon v-html="item.icon"></v-icon>
							</v-list-tile-action>
							<v-list-tile-content>
								<v-list-tile-title v-text="item.title"></v-list-tile-title>
							</v-list-tile-content>
						</v-list-tile>
					</v-list>
				</v-menu>
			</v-toolbar-items>
		</v-toolbar>
	</div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'Navigation',
  data() {
    return {
      sideNav: false,
      menu: false,
      items: [
        { title: 'Список задач', icon: 'list', link: '/tasks' },
        { title: 'Создать задачу', icon: 'create', link: '/create' }
      ],
      userItems: [
        {
          title: 'Профиль',
          icon: 'account_circle',
          link: '/profile',
          method: function() {}
        },
        { title: 'Выйти', icon: 'exit_to_app', method: this.onLogout }
      ]
    }
  },
  computed: {
    sideNavigation: {
      get() {
        if (this.$vuetify.breakpoint.xs) {
          if (this.sideNav) {
            return true
          }
          return false
        }
      },
      set(newValue) {
        this.sideNav = newValue
      }
    },
    miniVariant() {
      return !this.$vuetify.breakpoint.xs
    },
    ...mapGetters(['user'])
  },
  methods: {
    onLogout() {
      this.$store.dispatch('signOut')
      this.$router.push('/authentication')
    },
    iconSize() {
      if (this.$vuetify.breakpoint.smAndUp) {
        return { 'font-size': '50px' }
      }
    },
    fullHeight() {
      if (this.$vuetify.breakpoint.smAndUp) {
        return { height: '100px' }
      }
    }
  },
  mounted() {
    if (this.$vuetify.breakpoint.smAndUp) {
      var links = document.getElementsByClassName(
        'v-list__tile v-list__tile--link'
      )
      for (let link of links) {
        link.setAttribute('style', 'height:100%;')
      }
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
