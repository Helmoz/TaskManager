export default {
  state: {
    projects: [
      {
        name: 'Frozen Yogurt',
        description: 'Lorem ipsum dolor, sit amet consectetur',
        icon: 'add',
        progress: 20,
        type: 0
      },
      {
        name: 'Ice cream sandwich',
        description:
          'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Quod obcaecati',
        icon: 'person',
        progress: 40,
        type: 1
      },
      {
        name: 'Eclair',
        description:
          'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Quod obcaecati iure deserunt illo ',
        icon: 'face',
        progress: 0,
        type: 2
      },
      {
        name: 'Cupcake',
        description:
          'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Quod obcaecati iure deserunt illo quos',
        icon: 'check',
        progress: 7,
        type: 1
      },
      {
        name: 'Gingerbread',
        description:
          'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Quod obcaecati iure deserunt illo quos rerum ',
        icon: 'info',
        progress: 85,
        type: 2
      },
      {
        name: 'Jelly bean',
        description:
          'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Quod obcaecati iure deserunt illo quos rerum eveniet nam quidem et cumque.',
        icon: 'list',
        progress: 100,
        type: 0
      }
    ],
    currentProject: null,
    typeIcons: ['lightbulb_outline', 'build', 'check']
  },
  mutations: {
    addProject(state, payload) {
      state.projects.push(payload)
    },
    setCurrentProject(state, payload) {
      state.currentProject = payload
    }
  },
  actions: {
    async addProject({ commit }, payload) {
      commit('setLoading', true)
      commit('addProject', payload)
      commit('setLoading', false)
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
