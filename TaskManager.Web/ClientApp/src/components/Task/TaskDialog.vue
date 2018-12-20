<template>
  <div>
    <v-dialog v-model="currentDialog" max-width="600">
      <v-card>
        <v-card-title class="headline primary white--text">
          <v-spacer></v-spacer>
          <v-btn color="white" flat icon @click="currentDialog = false">
            <v-icon>close</v-icon>
          </v-btn>
        </v-card-title>
        <v-container grid-list-xs>
          <v-layout row wrap>
            <v-flex xs12>
              <v-card-title class="headline pa-0">
                {{currentTask.name}}
                <v-chip
                  v-if="currentTask.isPriority"
                  class="ml-3 error white--text"
                  close
                  @input="currentTask.isPriority = false"
                >
                  <v-icon class="mr-2">info</v-icon>Важная
                </v-chip>
                <v-chip
                  v-if="currentTask.isSpecial"
                  class="ml-2 warning white--text"
                  close
                  @input="currentTask.isSpecial = false"
                >
                  <v-icon class="mr-2">warning</v-icon>Специальная
                </v-chip>
                <v-btn
                  outline
                  color="error"
                  v-if="!currentTask.isPriority"
                  @click="currentTask.isPriority = true"
                >Пометить важной</v-btn>
                <v-btn
                  outline
                  color="warning"
                  v-if="!currentTask.isSpecial"
                  @click="currentTask.isSpecial = true"
                >Пометить специальной</v-btn>
              </v-card-title>

              <v-container class="pa-0 mt-4">
                <v-text-field
                  v-model="currentTask.name"
                  label="Имя проекта"
                  counter="80"
                  required
                  :error-messages="nameErrors"
                  @input="$v.currentTask.name.$touch()"
                  @blur="$v.currentTask.name.$touch()"
                ></v-text-field>
                <v-textarea v-model="currentTask.details" label="Описание" counter="300" required></v-textarea>
              </v-container>

              <v-card-actions class="pa-0">
                <v-btn class="mt-4" color="success" block depressed @click="saveChanges">
                  <v-icon class="mr-2">save</v-icon>Сохранить изменения
                </v-btn>
              </v-card-actions>
              <v-layout row wrap class="mt-4">
                <v-flex xs6>
                  <v-subheader>Создатель</v-subheader>
                  <v-list-tile avatar>
                    <v-list-tile-avatar>
                      <img :src="currentTask.creator.photoUrl">
                    </v-list-tile-avatar>
                    <v-list-tile-content>
                      <v-list-tile-title v-html="currentTask.creator.name"></v-list-tile-title>
                    </v-list-tile-content>
                  </v-list-tile>
                </v-flex>
                <v-flex xs6 v-if="currentTask.completedBy">
                  <v-subheader>Завершено</v-subheader>
                  <v-list-tile avatar>
                    <v-list-tile-avatar>
                      <img :src="currentTask.completedBy.photoUrl">
                    </v-list-tile-avatar>
                    <v-list-tile-content>
                      <v-list-tile-title v-html="currentTask.completedBy.name"></v-list-tile-title>
                    </v-list-tile-content>
                  </v-list-tile>
                </v-flex>
              </v-layout>
              <v-card-actions class="pa-0 mt-4" tag="v-layout" align-center>
                <v-btn class="grey--text grey lighten-3 mr-2 px-0" block depressed @click>
                  <v-icon class="mr-2">delete</v-icon>Удалить
                </v-btn>
                <v-divider></v-divider>
                <v-btn
                  v-if="!currentTask.isCompleted"
                  color="success ml-2 px-0"
                  block
                  depressed
                  @click="complete"
                >
                  <v-icon class="mr-2">check</v-icon>Завершить
                </v-btn>
                <v-btn v-else color="secondary ml-2 px-0" block depressed @click="complete">
                  <v-icon class="mr-2">check</v-icon>Возобновить
                </v-btn>
              </v-card-actions>
            </v-flex>
          </v-layout>
        </v-container>
      </v-card>
    </v-dialog>
    <v-snackbar v-model="snackbar" :timeout="3000" color="success">
      <v-icon dark class="mr-2">info</v-icon>
      <span>Задача успешно обновлена</span>
      <v-btn dark flat @click="snackbar = false">
        <v-icon>close</v-icon>
      </v-btn>
    </v-snackbar>
  </div>
</template>

<script>
import { required, maxLength, email } from 'vuelidate/lib/validators'
import { mapActions, mapGetters } from 'vuex'

export default {
  validations: {
    currentTask: { name: { required, maxLength: maxLength(80) } }
  },
  props: ['dialog'],
  components: {},
  data() {
    return {
      currentDialog: this.dialog,
      snackbar: false
    }
  },
  methods: {
    ...mapActions(['editTask', 'completeTask']),
    saveChanges() {
      const task = {
        id: this.currentTask.id,
        name: this.currentTask.name,
        details: this.currentTask.details,
        isPriority: this.currentTask.isPriority,
        isSpecial: this.currentTask.isSpecial
      }
      this.editTask(task).then(response => (this.snackbar = response))
    },
    complete() {
      if (this.currentTask.isCompleted) {
        this.completeTask({ id: this.currentTask.id })
        this.currentTask.isCompleted = !this.currentTask.isCompleted
      } else {
        this.completeTask({ id: this.currentTask.id, completedBy: { uid: this.user.id } })
        this.currentTask.isCompleted = !this.currentTask.isCompleted
      }
    }
  },
  computed: {
    ...mapGetters(['user', 'currentTask']),
    nameErrors() {
      const errors = []
      if (!this.$v.currentTask.name.$dirty) return errors
      !this.$v.currentTask.name.required && errors.push('Это поле обязательно к заполнению.')
      return errors
    }
  },
  watch: {
    currentDialog() {
      this.$emit('update:dialog', this.currentDialog)
    },
    dialog() {
      this.currentDialog = this.dialog
    }
  }
}
</script>

