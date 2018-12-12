<template>
  <v-flex xs12 sm8>
    <v-subheader class="pl-0">Тип проекта</v-subheader>
    <v-layout row wrap>
      <v-flex xs1 class="mr-4">
        <v-icon large v-html="typeIcons[newType]"></v-icon>
      </v-flex>
      <v-flex xs9 sm7>
        <v-select
          :items="types"
          v-model="newType"
          item-value="type"
          item-text="name"
          class="pt-0 mt-0"
          @input="$emit('update:type', newType)"
        ></v-select>
      </v-flex>
    </v-layout>
  </v-flex>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  props: ['type'],
  data() {
    return {
      newType: this.type,
      types: [{ name: 'Идея', type: 0 }, { name: 'В разработке', type: 1 }, { name: 'Завершен', type: 2 }]
    }
  },
  watch: {
    type() {
      this.newType = this.type
    }
  },
  computed: {
    ...mapGetters(['typeIcons'])
  }
}
</script>


