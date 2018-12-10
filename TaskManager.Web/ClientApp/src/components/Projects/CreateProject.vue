<template>
  <v-layout row wrap class="pr-4 pl-4 mb-4" v-if="$vuetify.breakpoint.mdAndUp">
    <v-flex xs12>
      <v-form lazy-validation class="text-xs-right">
        <v-text-field
          v-model="name"
          label="Имя проекта"
          prepend-icon="add"
          clearable
          counter="80"
          required
          @input="$v.name.$touch()"
          @blur="$v.name.$touch()"
          @keyup.enter="onSubmit"
        ></v-text-field>
        <v-btn
          color="primary"
          class="mr-0"
          depressed
          :disabled="$v.$invalid"
          @click="onSubmit"
        >Создать</v-btn>
      </v-form>
    </v-flex>
  </v-layout>
</template>

<script>
import { required, maxLength } from 'vuelidate/lib/validators'
import { mapGetters, mapActions } from 'vuex'
export default {
  validations: {
    name: { required, maxLength: maxLength(80) }
  },
  data() {
    return {
      name: '',
      member: null
    }
  },
  computed: {
    ...mapGetters(['loading', 'user'])
  },
  methods: {
    ...mapActions(['addProject']),
    onSubmit() {
      var date = new Date(Date.now())
      date.setHours(10, 10, 10)
      const project = {
        name: this.name,
        description: '....',
        type: 0,
        deadline: date,
        members: [
          {
            Uid: '',
            Name: this.user.name,
            PhotoUrl: this.user.photoURL,
            Email: this.user.email
          }
        ]
      }
      this.addProject(project)
      this.name = ''
    }
  }
}
</script>

