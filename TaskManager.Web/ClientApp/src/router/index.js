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
      path: '/tasks',
      name: 'Список задач • TaskManager',
      component: () => import('../views/Tasks.vue'),
      beforeEnter: auth
    },
    {
      path: '/assigned',
      name: 'Мои задачи • TaskManager',
      component: () => import('../views/Assigned.vue'),
      beforeEnter: auth
    },
    {
      path: '/completed',
      name: 'Завершенные • TaskManager',
      component: () => import('../views/Completed.vue'),
      beforeEnter: auth
    },
    {
      path: '/profile',
      name: 'Профиль • TaskManager',
      component: () => import('../views/Profile.vue'),
      beforeEnter: auth
    },
    {
      path: '/about',
      name: 'О проекте • TaskManager',
      component: () => import('../views/About.vue'),
      beforeEnter: auth
    },
    {
      path: '/project',
      name: 'Проект• TaskManager',
      component: () => import('../views/Project.vue'),
      beforeEnter: auth
    }
  ]
})
