import axios from 'axios'

export default {
  state: {
    currentTask: null
  },
  mutations: {
    setCurrentTask(state, payload) {
      state.currentTask = payload
    }
  },
  actions: {
    async addTask({ commit, getters }, payload) {
      commit('setLoading', true)
      try {
        let response = await axios.post('/api/Task/AddTask', payload)
        commit('setCurrentProject', { ...response.data, userId: getters.user.id })
        commit('setLoading', false)
      } catch (err) {
        console.log(err)
        commit('setLoading', false)
      }
    },
    async editTask({ commit, getters }, payload) {
      commit('setLoading', true)
      try {
        let response = await axios.put('/api/Task/UpdateTask', payload)
        commit('setCurrentProject', { ...response.data, userId: getters.user.id })
        commit('setCurrentTask', response.data.tasks.filter(x => x.id == payload.id)[0])
        commit('setLoading', false)
        return true
      } catch (err) {
        console.log(err)
        commit('setLoading', false)
        return false
      }
    },
    async completeTask({ commit, getters }, payload) {
      commit('setLoading', true)
      try {
        let response = await axios.put('/api/Task/CompleteChangeTask', payload)
        commit('setCurrentProject', { ...response.data, userId: getters.user.id })
        commit('setCurrentTask', response.data.tasks.filter(x => x.id == payload.id)[0])
        commit('setLoading', false)
        return true
      } catch (err) {
        console.log(err)
        commit('setLoading', false)
        return false
      }
    },
    async deleteTask({ commit, getters }, payload) {
      commit('setLoading', true)
      try {
        let response = await axios.put('/api/Task/DeleteTask', payload)
        commit('setCurrentProject', { ...response.data, userId: getters.user.id })
        commit('setCurrentTask', response.data.tasks.filter(x => x.id == payload.id)[0])
        commit('setLoading', false)
        return true
      } catch (err) {
        console.log(err)
        commit('setLoading', false)
        return false
      }
    },
    setCurrentTask({ commit }, payload) {
      commit('setCurrentTask', payload)
    }
  },
  getters: {
    currentTask(state) {
      return state.currentTask
    }
  }
}
