<template>
  <v-flex xs5>
    <v-subheader class="pl-0">Дедлайн</v-subheader>
    <v-menu
      :close-on-content-click="false"
      v-model="datePicker"
      :nudge-right="40"
      lazy
      transition="scale-transition"
      offset-y
      full-width
      min-width="290px"
    >
      <v-text-field slot="activator" v-model="newDeadline" prepend-icon="event" class readonly></v-text-field>
      <v-date-picker
        v-model="newDeadline"
        @input="datePicker = false; $emit('update:deadline', validDate(newDeadline))"
      ></v-date-picker>
    </v-menu>
  </v-flex>
</template>

<script>
export default {
  props: ['deadline'],
  data() {
    return {
      datePicker: false,
      newDeadline: new Date(this.deadline).toISOString().substr(0, 10)
    }
  },
  methods: {
    validDate(date) {
      var deadline = new Date(date)
      deadline.setHours(10, 10, 10)
      return deadline
    }
  }
}
</script>


