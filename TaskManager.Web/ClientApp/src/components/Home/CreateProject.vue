<template>
  <v-container
    class="pl-0 pr-0 pb-0 pt-0"
    :style="{height: $vuetify.breakpoint.mdAndUp ? '100%' : '0px'}"
  >
    <v-layout row wrap v-if="$vuetify.breakpoint.mdAndUp" class="pl-4 pr-4 pb-4 pt-4">
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
    <v-bottom-sheet v-model="sheet" v-if="$vuetify.breakpoint.smAndDown">
      <v-btn
        slot="activator"
        fixed
        bottom
        right
        color="primary"
        dark
        fab
        style="margin-bottom: 60px"
      >
        <v-icon dark>add</v-icon>
      </v-btn>
      <v-list>
        <v-container grid-list-xs>
          <v-layout row wrap>
            <v-flex xs12>
              <v-form lazy-validation class="text-xs-right">
                <v-text-field
                  v-model="name"
                  label="Имя проекта"
                  prepend-icon="add"
                  clearable
                  required
                  @input="$v.name.$touch()"
                  @blur="$v.name.$touch()"
                  @keyup.enter="onSubmit"
                ></v-text-field>
                <v-btn
                  :loading="loading"
                  color="primary"
                  class="mr-0"
                  depressed
                  :disabled="$v.$invalid"
                  @click="onSubmit"
                >Создать</v-btn>
              </v-form>
            </v-flex>
          </v-layout>
        </v-container>
      </v-list>
    </v-bottom-sheet>
  </v-container>
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
      sheet: false,
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
            member: {
              Uid: this.user.id,
              Name: this.user.name,
              PhotoUrl: this.user.photoURL,
              Email: this.user.email
            }
          }
        ]
      }
      this.addProject(project)
      this.name = ''
    }
  }
}
</script>

