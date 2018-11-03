import firebase from 'firebase/app'

import User from './helpers/user_help'

export default {
  state: {
    user: null
  },
  mutations: {
    setUser(state, payload) {
      state.user = payload
    }
  },
  actions: {
    async loginUser({ commit, dispatch }, { email, password }) {
      commit('clearError')
      commit('setLoading', true)
      try {
        const user = await firebase
          .auth()
          .signInWithEmailAndPassword(email, password)
        commit('setUser', new User(user.user.uid))
        commit('setLoading', false)
      } catch (error) {
        commit('setLoading', false)
        dispatch('setError', error)
        throw error
      }
    },
    loggedUser({ commit }, payload) {
      commit('setUser', new User(payload.uid))
    },
    logoutUser({ commit }) {
      firebase.auth().signOut()
      commit('setUser', null)
    }
  },
  getters: {
    user(state) {
      return state.user
    }
  }
}
