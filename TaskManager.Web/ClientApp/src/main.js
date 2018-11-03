import '@babel/polyfill'
import Vue from 'vue'
import './plugins/vuetify'
import Vuelidate from 'vuelidate'

import App from './App.vue'
import router from './router'
import store from './store/'

import firebase from 'firebase/app'
import 'firebase/auth'
import 'firebase/database'

import './registerServiceWorker'
import 'roboto-fontface/css/roboto/roboto-fontface.css'
import 'material-design-icons-iconfont/dist/material-design-icons.css'

Vue.use(Vuelidate)

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App),
  created() {
    var config = {
      apiKey: 'AIzaSyATQFwQLtnnkvpxn5ftQ-uAGfrv81O-JpE',
      authDomain: 'taskmanager-2.firebaseapp.com',
      databaseURL: 'https://taskmanager-2.firebaseio.com',
      projectId: 'taskmanager-2',
      storageBucket: 'taskmanager-2.appspot.com',
      messagingSenderId: '327553353317'
    }
    firebase.initializeApp(config)

    firebase.auth().onAuthStateChanged(user => {
      if (user) {
        this.$store.dispatch('loggedUser', user)
      }
    })
  }
}).$mount('#app')
