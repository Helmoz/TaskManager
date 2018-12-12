<template>
  <v-container class="pb-5">
    <v-layout row wrap>
      <v-flex xs12>
        <v-layout row wrap>
          <name :name.sync="currentProject.name"></name>
          <description :description.sync="currentProject.description"></description>
          <in-developing
            :type="currentProject.type"
            :progress.sync="currentProject.progress"
            :deadline.sync="currentProject.deadline"
          ></in-developing>
          <type :type.sync="currentProject.type"></type>
          <tags :tags.sync="currentProject.tags"></tags>
          <members :members.sync="currentProject.members"></members>
          <v-btn class="mt-4" color="success" block depressed @click="saveChanges()">
            <v-icon class="mr-2">save</v-icon>Сохранить изменения
          </v-btn>
        </v-layout>
      </v-flex>
    </v-layout>
    <v-snackbar v-model="snackbar" color="success" timeout="3000">Проект успешно обновлен
      <v-btn dark flat @click="snackbar = false">
        <v-icon>close</v-icon>
      </v-btn>
    </v-snackbar>
  </v-container>
</template>

<script>
import 'velocity-animate'

import { mapGetters, mapActions } from 'vuex'
import { required, maxLength } from 'vuelidate/lib/validators'

import Name from '../components/Project/Name.vue'
import Description from '../components/Project/Description.vue'
import InDeveloping from '../components/Project/InDeveloping/InDeveloping.vue'
import Type from '../components/Project/Type.vue'
import Tags from '../components/Project/Tags.vue'
import Members from '../components/Project/Members/Members.vue'

export default {
  components: {
    Name,
    Description,
    InDeveloping,
    Type,
    Tags,
    Members
  },
  data() {
    return {
      snackbar: false
    }
  },
  computed: {
    ...mapGetters(['currentProject', 'loading'])
  },
  methods: {
    ...mapActions(['editProject']),
    saveChanges() {
      if (this.currentProject.progress === 100) {
        this.currentProject.type = 2
      }
      if (this.currentProject.name !== '' && this.currentProject.name.length <= 80) {
        this.editProject(this.currentProject).then(x => (this.snackbar = x))
      }
    }
  }
}
</script>



