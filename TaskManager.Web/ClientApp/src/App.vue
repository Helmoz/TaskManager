<template>
	<v-app>
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
				<v-menu bottom offset-y v-if="user">
					<v-btn flat slot="activator">
						<v-icon left>person</v-icon>
						{{user.name}}
					</v-btn>
					<v-list style="width: 220px">
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
		<v-content>
			<transition name="slide-fade" mode="out-in">
				<router-view></router-view>
			</transition>
		</v-content>
	</v-app>
</template>

<script>
export default {
  name: 'App',
  data() {
    return {
      sideNav: false,
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
    userIsAuthenticated() {
      return this.$store.getters.user !== null
    },
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
    user() {
      return this.$store.getters.user
    }
  },
  methods: {
    onLogout() {
      this.$store.dispatch('logoutUser')
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
      var a = document.getElementsByClassName('v-list__tile v-list__tile--link')
      for (let item of a) {
        item.setAttribute('style', 'height:100%;')
      }
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

