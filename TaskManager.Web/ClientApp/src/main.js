import '@babel/polyfill'
import Vue from 'vue'
import './plugins/vuetify'
import Vuelidate from 'vuelidate'

import App from './App.vue'
import router from './router/'
import { store } from './store/'

import './registerServiceWorker'
import 'roboto-fontface/css/roboto/roboto-fontface.css'
import 'material-design-icons-iconfont/dist/material-design-icons.css'

Vue.use(Vuelidate)

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')

router.afterEach((to, from) => {
  Vue.nextTick(() => {
    document.title = to.name
  })
})
