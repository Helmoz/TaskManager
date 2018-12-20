<template>
  <v-flex xs12>
    <v-layout row wrap>
      <v-flex xs12 sm4>
        <v-text-field
          v-model="tag.name"
          label="Добавить тэг"
          append-icon="add"
          @click:append="addTag"
          @keyup.enter="addTag"
          prefix="#"
          counter="20"
          required
          @input="$v.tag.$touch()"
          @blur="$v.tag.$touch()"
          :error-messages="tagErrors"
        ></v-text-field>
      </v-flex>
      <v-flex xs12 sm7 class="ml-2">
        <v-item-group multiple>
          <v-item v-for="tag in newTags" :key="tag.name">
            <v-chip slot-scope="{ active, toggle }" close @input="removeTag(tag)"># {{tag.tag.name}}</v-chip>
          </v-item>
        </v-item-group>
      </v-flex>
    </v-layout>
  </v-flex>
</template>

<script>
import { mapGetters } from 'vuex'
import { required, maxLength } from 'vuelidate/lib/validators'

export default {
  props: ['tags'],
  validations: {
    tag: { required, maxLength: maxLength(20) }
  },
  data() {
    return {
      tag: { name: '' },
      newTags: this.tags
    }
  },
  computed: {
    tagErrors() {
      const errors = []
      if (!this.$v.tag.$dirty) return errors
      !this.$v.tag.maxLength && errors.push('Слишком длинный тэг')
      !this.$v.tag.required && errors.push('Введите тэг')
      this.tags.map(x => x.name).indexOf(this.tag.name) !== -1 && errors.push('Данный тэг уже добавлен')
      return errors
    }
  },
  methods: {
    addTag() {
      if (this.tag.name != '' && this.tag.name.length <= 20 && this.newTags.map(x => x.name).indexOf(this.tag.name) === -1) {
        this.newTags.push({ tag: { name: this.tag.name } })
        this.tag.name = ''
        this.$emit('update:tags', this.newTags)
      }
    },
    removeTag(tag) {
      this.newTags.splice(this.newTags.indexOf(tag), 1)
      this.newTags = [...this.newTags]
      this.$emit('update:tags', this.newTags)
    }
  }
}
</script>


