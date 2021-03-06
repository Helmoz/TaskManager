import axios from 'axios'
import { stat } from 'fs'

export default {
  state: {
    projects: [],
    currentProject: null,
    typeIcons: ['lightbulb_outline', 'build', 'check']
  },
  mutations: {
    addProject(state, payload) {
      state.projects.push(payload)
    },
    setCurrentProject(state, payload) {
      let task = []
      payload.tasks.forEach(el => {
        el.assignedTo.forEach(elemenet => {
          if (elemenet.member.uId == payload.userId && !el.isCompleted) task.push(el)
        })
      })
      state.currentProject = { ...payload, assignedToCurrent: task }
    },
    setProjects(state, payload) {
      state.projects = payload
    }
  },
  actions: {
    async loadProjects({ commit }) {
      commit('setLoading', true)
      try {
        let response = await axios.get('/api/Project/GetProjects/')
        commit('setProjects', response.data)
        commit('setLoading', false)
      } catch (err) {
        console.log(err)
        commit('setLoading', false)
      }
    },
    async addProject({ commit }, payload) {
      commit('setLoading', true)
      try {
        let response = await axios.post('/api/Project/AddProject', payload)
        commit('addProject', response.data)
        commit('setLoading', false)
      } catch (err) {
        console.log(err)
        commit('setLoading', false)
      }
    },
    async editProject({ commit, getters }, payload) {
      commit('setLoading', true)
      try {
        let response = await axios.put('/api/Project/UpdateProject', payload)
        commit('setCurrentProject', { ...response.data, userId: getters.user.id })
        commit('setLoading', false)
        return true
      } catch (err) {
        console.log(err)
        commit('setLoading', false)
        return false
      }
    },
    setCurrentProject({ commit, getters }, payload) {
      commit('setCurrentProject', { ...payload, userId: getters.user.id })
    }
  },
  getters: {
    projects(state) {
      return state.projects
    },
    currentProject(state) {
      return state.currentProject
    },
    typeIcons(state) {
      return state.typeIcons
    }
  }
}
