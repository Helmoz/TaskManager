<template>
  <div>
    <v-card flat class="card">
      <v-container class="card_container">
        <v-layout row wrap justify-center>
          <v-flex xs12>
            <h1 class="text-xs-center card_header">TaskManager</h1>
            <v-form ref="form" lazy-validation class="registration_form">
              <v-text-field
                outline
                v-model="name"
                :error-messages="nameErrors"
                label="Имя и фамилия"
                required
                @input="$v.name.$touch()"
                @blur="$v.name.$touch()"
                @keyup.enter="onSubmit"
              ></v-text-field>
              <v-text-field
                v-model="email"
                label="E-mail"
                :error-messages="emailErrors"
                @input="$v.email.$touch()"
                @blur="$v.email.$touch()"
                @keyup.enter="onSubmit"
                required
                outline
              ></v-text-field>
              <v-text-field
                outline
                v-model="password"
                :append-icon="showPassword ? 'visibility_off' : 'visibility'"
                :type="showPassword ? 'text' : 'password'"
                label="Пароль"
                :error-messages="passwordErrors"
                @input="$v.password.$touch()"
                @blur="$v.password.$touch()"
                @keyup.enter="onSubmit"
                required
                @click:append="showPassword = !showPassword"
              ></v-text-field>
              <v-btn
                :loading="loading"
                color="primary"
                block
                :disabled="$v.$invalid"
                @click="onSubmit"
              >Регистрация</v-btn>
              <div v-if="error" class="registration_error">
                <v-divider></v-divider>
                <p class="registration_error_message">{{error}}</p>
              </div>
            </v-form>
          </v-flex>
        </v-layout>
      </v-container>
    </v-card>
    <v-card flat style="margin-top: 10px" class="card card_login">Есть аккаунт?
      <router-link to="/signin" style="cursor: pointer">Вход</router-link>
    </v-card>
  </div>
</template>

<script>
import { required, minLength, email } from 'vuelidate/lib/validators'
import { mapGetters, mapActions } from 'vuex'

export default {
  validations: {
    password: { required, minLength: minLength(8) },
    email: { required, email },
    name: { required }
  },
  data() {
    return {
      showPassword: false,
      email: '',
      password: '',
      name: ''
    }
  },
  computed: {
    ...mapGetters(['loading', 'error', 'user']),
    passwordErrors() {
      const errors = []
      if (!this.$v.password.$dirty) return errors
      !this.$v.password.minLength && errors.push('Создайте пароль минимум из 8 симоволов')
      !this.$v.password.required && errors.push('Это поле обязательно к заполнению.')
      return errors
    },
    emailErrors() {
      const errors = []
      if (!this.$v.email.$dirty) return errors
      !this.$v.email.email && errors.push('Введите корректный электронный адрес.')
      !this.$v.email.required && errors.push('Это поле обязательно к заполнению.')
      return errors
    },
    nameErrors() {
      const errors = []
      if (!this.$v.name.$dirty) return errors
      !this.$v.name.required && errors.push('Это поле обязательно к заполнению.')
      return errors
    }
  },
  methods: {
    ...mapActions(['registerUser', 'addUser']),
    onSubmit() {
      this.$v.$touch()
      if (!this.$v.$invalid) {
        const user = {
          email: this.email,
          password: this.password,
          name: this.name
        }
        this.registerUser(user)
          .then(() => {
            this.addUser(this.user)
            this.$router.push('/')
          })
          .catch(err => {
            console.log(err)
          })
      }
    }
  }
}
</script>


<style lang="stylus" scoped>
.registration
	&_form
		margin-top 30px
	&_error
		margin-top 20px
		&_message
			margin-top 20px
			text-align center
			color #FF5252
			margin-bottom 0
.card
	border 1px solid #e6e6e6
	border-radius 1px
	&_container
		padding 40px
	&_login
		padding 25px 15px
		text-align center
	&_header
		font-family Billabong
		font-size 50px

a
	text-decoration none
	
</style>

