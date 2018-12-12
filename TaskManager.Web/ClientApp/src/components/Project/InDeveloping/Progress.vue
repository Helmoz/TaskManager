<template>
  <v-flex xs6>
    <v-subheader class="pl-0">Выполнено на {{newProgress}}%</v-subheader>
    <v-slider
      @input="$emit('update:progress', newProgress)"
      v-model="newProgress"
      class
      thumb-label
    ></v-slider>
  </v-flex>
</template>

<script>
export default {
  props: ['progress'],
  data() {
    return {
      newProgress: this.progress
    }
  }
}
</script>


