<template>
  <v-flex xs12>
    <v-textarea
      label="Описание"
      v-model="newDescription"
      counter="300"
      @input="$emit('update:description', newDescription)"
      @blur="$emit('update:description', newDescription)"
    ></v-textarea>
  </v-flex>
</template>

<script>
export default {
  props: ['description'],
  data() {
    return {
      newDescription: this.description
    }
  }
}
</script>


