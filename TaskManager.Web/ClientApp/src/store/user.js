import firebase from 'firebase/app'

import User from './helpers/user_help'

import axios from 'axios'

export default {
  state: {
    user: null,
    userByEmail: null
  },
  mutations: {
    setUser(state, payload) {
      state.user = payload
    },
    setUserByEmail(state, payload) {
      state.userByEmail = payload
    }
  },
  actions: {
    async signIn({ commit, dispatch }, { email, password }) {
      commit('clearError')
      commit('setLoading', true)
      try {
        const user = await firebase.auth().signInWithEmailAndPassword(email, password)
        commit(
          'setUser',
          new User(user.user.uid, user.user.email, user.user.displayName, user.user.photoURL)
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

        var currentUser = await firebase.auth().currentUser

        await currentUser.updateProfile({
          displayName: name,
          photoURL: 'https://zcoin.io/wp-content/uploads/2017/01/blank-avatar-300x300.png'
        })

        commit(
          'setUser',
          new User(
            currentUser.uid,
            currentUser.email,
            currentUser.displayName,
            currentUser.photoURL
          )
        )

        commit('setLoading', false)
      } catch (error) {
        commit('setLoading', false)
        dispatch('setError', error)
        throw error
      }
    },
    async addUser({ commit, dispatch }, payload) {
      commit('setLoading', true)
      try {
        await axios.post('/api/User/AddUser', {
          UId: payload.id,
          Name: payload.name,
          PhotoUrl: payload.photo,
          Email: payload.email
        })
      } catch (err) {
        console.log(err)
        commit('setLoading', false)
      }
    },
    async loadMemberByEmail({ commit }, payload) {
      commit('setLoading', true)
      try {
        var member = await axios.get('/api/User/GetUserByEmail/' + payload)
        commit('setLoading', false)
        return member
      } catch (err) {
        console.log(err)
        commit('setLoading', false)
      }
    },
    authCheck({ state, commit }) {
      return new Promise((resolve, reject) => {
        firebase.auth().onAuthStateChanged(
          user => {
            if (user) {
              commit('setUser', new User(user.uid, user.email, user.displayName, user.photoURL))
            }
            resolve(state.user)
          },
          error => reject(error)
        )
      })
    },
    signOut({ commit }) {
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
