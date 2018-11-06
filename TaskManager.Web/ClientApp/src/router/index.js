import Vue from 'vue'
import Router from 'vue-router'
import Home from '../views/Home.vue'
import auth from './auth'

Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'TaskManager',
      component: Home,
      beforeEnter: auth
    },
    {
      path: '/signin',
      name: 'Войти • TaskManager',
      component: () => import('../views/SignIn.vue')
    },
    {
      path: '/authentication',
      name: 'Аутентифицироваться • TaskManager',
      component: () => import('../views/Authentication.vue')
    },
    {
      path: '/registration',
      name: 'Зарегистрироваться • TaskManager',
      component: () => import('../views/Registration.vue')
    },
    {
      path: '/profile',
      name: 'Профиль • TaskManager',
      component: () => import('../views/Profile.vue'),
      beforeEnter: auth
    }
  ]
})
