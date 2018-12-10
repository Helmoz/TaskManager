import axios from 'axios'

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
      state.currentProject = payload
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
    async editProject({ commit }, payload) {
      commit('setLoading', true)
      try {
        let response = await axios.put('/api/Project/UpdateProject', payload)
        commit('setLoading', false)
      } catch (err) {
        console.log(err)
        commit('setLoading', false)
      }
    },
    setCurrentProject({ commit }, payload) {
      commit('setCurrentProject', payload)
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
