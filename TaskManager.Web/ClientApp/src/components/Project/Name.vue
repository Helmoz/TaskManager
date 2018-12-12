<template>
  <v-flex xs6>
    <v-text-field
      v-model="newName"
      label="Имя проекта"
      counter="80"
      required
      :error-messages="nameErrors"
      @input="$v.name.$touch(); $emit('update:name', newName)"
      @blur="$v.name.$touch(); $emit('update:name', newName)"
    ></v-text-field>
  </v-flex>
</template>

<script>
import { required, maxLength, email } from 'vuelidate/lib/validators'

export default {
  validations: {
    name: { required, maxLength: maxLength(80) }
  },
  props: ['name'],
  data() {
    return {
      newName: this.name
    }
  },
  computed: {
    nameErrors() {
      const errors = []
      if (!this.$v.name.$dirty) return errors
      !this.$v.name.required && errors.push('Это поле обязательно к заполнению.')
      return errors
    }
  }
}
</script>


