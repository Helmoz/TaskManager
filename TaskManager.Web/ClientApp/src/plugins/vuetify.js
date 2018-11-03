import Vue from 'vue'
import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.min.css'

Vue.use(Vuetify, {
  theme: {
    primary: '#06a77d',
    secondary: '#036357',
    accent: '#65afff',
    error: '#FF5252',
    info: '#01434a',
    success: '#4CAF50',
    warning: '#FFC107'
  },
  customProperties: true,
  iconfont: 'md'
})
