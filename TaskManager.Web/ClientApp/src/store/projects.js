export default {
  state: {
    projects: [
      {
        name: 'Frozen Yogurt',
        description: 'Lorem ipsum dolor, sit amet consectetur',
        icon: 'add'
      },
      {
        name: 'Ice cream sandwich',
        description:
          'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Quod obcaecati',
        icon: 'person'
      },
      {
        name: 'Eclair',
        description:
          'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Quod obcaecati iure deserunt illo ',
        icon: 'face'
      },
      {
        name: 'Cupcake',
        description:
          'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Quod obcaecati iure deserunt illo quos',
        icon: 'check'
      },
      {
        name: 'Gingerbread',
        description:
          'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Quod obcaecati iure deserunt illo quos rerum ',
        icon: 'info'
      },
      {
        name: 'Jelly bean',
        description:
          'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Quod obcaecati iure deserunt illo quos rerum eveniet nam quidem et cumque.',
        icon: 'list'
      }
    ]
  },
  mutations: {
    addProject(state, payload) {
      state.projects.push(payload)
    }
  },
  actions: {
    async addProject({ commit }, payload) {
      commit('setLoading', true)
      commit('addProject', payload)
      commit('setLoading', false)
    }
  },
  getters: {
    projects(state) {
      return state.projects
    }
  }
}
