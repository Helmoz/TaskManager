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
        commit(
          'setUser',
          new User(user.user.uid, user.user.email, user.user.displayName)
        )
        commit('setLoading', false)
      } catch (error) {
        commit('setLoading', false)
        dispatch('setError', error)
        throw error
      }
    },
    async registerUser({ commit, dispatch }, { email, password, name }) {
      commit('clearError')
      commit('setLoading', true)
      try {
        await firebase.auth().createUserWithEmailAndPassword(email, password)

        var currentUser = firebase.auth().currentUser

        currentUser.updateProfile({
          displayName: name
        })

        commit(
          'setUser',
          new User(currentUser.uid, currentUser.email, currentUser.displayName)
        )

        commit('setLoading', false)
      } catch (error) {
        commit('setLoading', false)
        dispatch('setError', error)
        throw error
      }
    },
    authCheck({ state, commit }) {
      return new Promise((resolve, reject) => {
        firebase.auth().onAuthStateChanged(
          user => {
            if (user) {
              commit(
                'setUser',
                new User(user.uid, user.email, user.displayName)
              )
            }
            resolve(state.user)
          },
          error => reject(error)
        )
      })
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
