import Vue from 'vue'
import Vuex from 'vuex'

import common from './common'
import user from './user'
import projects from './projects'
import tasks from './tasks'

import firebase from 'firebase/app'
import 'firebase/auth'
import 'firebase/database'
import config from '../config'

Vue.use(Vuex)

firebase.initializeApp(config)

export const store = new Vuex.Store({
  modules: {
    common,
    user,
    projects,
    tasks
  }
})
