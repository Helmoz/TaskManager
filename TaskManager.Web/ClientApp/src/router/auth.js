import { store } from '../store/index.js'

export default (to, from, next) => {
  store
    .dispatch('authCheck')
    .then(user => {
      if (user) {
        next()
      } else {
        next('/authentication')
      }
    })
    .catch(error => console.log(error))
}
