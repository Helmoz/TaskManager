<template>
  <v-app>
    <SideNavigation v-if="user"></SideNavigation>
    <v-bottom-nav color="primary" fixed :value="!mdAndUp" shift>
      <v-btn dark :to="`/`" exact>
        <span>Проекты</span>
        <v-icon>star</v-icon>
      </v-btn>
      <v-btn exact dark v-for="item in mainMenu" :key="item.title" icon :to="item.link">
        <span>{{item.title}}</span>
        <v-icon v-html="item.icon"></v-icon>
      </v-btn>
    </v-bottom-nav>
    <v-toolbar color="secondary" dark :extended="mdAndUp" flat v-if="user">
      <v-toolbar-title class="hidden-md-and-up">
        <router-link to="/" tag="span" style="cursor: pointer" class="logo">TaskManager</router-link>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items class="hidden-md-and-up">
        <v-btn exact v-for="item in subMenu" :key="item.title" icon :to="item.link">
          <v-icon v-html="item.icon"></v-icon>
        </v-btn>
      </v-toolbar-items>
    </v-toolbar>
    <v-content :style="backgroudColor">
      <!-- MdAndUp -->
      <v-container class="pb-0 pt-0" v-if="user && mdAndUp">
        <v-layout row justify-center>
          <v-flex xs10 lg7>
            <v-card class="card--flex-toolbar">
              <transition name="slide-fade" mode="out-in">
                <router-view></router-view>
              </transition>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
      <!-- MdAndDown -->
      <v-container grid-list-xs v-else-if="user">
        <v-layout row wrap justify-center>
          <v-flex xs12>
            <transition name="slide-fade" mode="out-in">
              <router-view></router-view>
            </transition>
          </v-flex>
        </v-layout>
      </v-container>
      <!-- USER === NULL -->
      <transition name="slide-fade" mode="out-in" v-else>
        <router-view></router-view>
      </transition>
    </v-content>
  </v-app>
</template>

<script>
import SideNavigation from './components/SideNavigation.vue'
import { mapGetters } from 'vuex'
export default {
  name: 'App',
  components: {
    SideNavigation
  },
  data() {
    return {}
  },
  computed: {
    ...mapGetters(['user', 'subMenu', 'mainMenu']),
    mdAndUp() {
      return this.$vuetify.breakpoint.mdAndUp
    },
    backgroudColor() {
      if (this.mdAndUp) return { 'background-color': '#f5f5f5' }
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
	font-size 35px

.slide-fade-enter-active 
	transition all .5s cubic-bezier(1.0, 0.5, 0.8, 1.0)

.slide-fade-leave-active 
	transition all .5s cubic-bezier(1.0, 0.5, 0.8, 1.0)

.slide-fade-enter, .slide-fade-leave-to 
	transform translateX(10px)
	opacity 0

.card--flex-toolbar
	margin-top -64px
</style>

