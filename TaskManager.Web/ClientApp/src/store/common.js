export default {
  state: {
    loading: false,
    error: null
  },
  mutations: {
    setLoading(state, payload) {
      state.loading = payload
    },
    setError(state, payload) {
      state.error = payload
    },
    clearError(state) {
      state.error = null
    }
  },
  actions: {
    setLoading({ commit }, payload) {
      commit('setLoading', payload)
    },
    setError({ commit }, payload) {
      var errorMessage = ''
      if (payload.code === 'auth/user-not-found')
        errorMessage =
          'Пользователь с таким e-mail не найден. Проверьте e-mail и повторите попытку.'
      if (payload.code === 'auth/wrong-password')
        errorMessage =
          'К сожалению, вы ввели неверный пароль. Проверьте пароль еще раз.'
      if (payload.code === 'auth/email-already-in-use')
        errorMessage = 'Этот e-mail уже используется другой учетной записью'

      commit('setError', errorMessage)
    },
    clearError({ commit }) {
      commit('clearError')
    }
  },
  getters: {
    loading(state) {
      return state.loading
    },
    error(state) {
      return state.error
    }
  }
}
