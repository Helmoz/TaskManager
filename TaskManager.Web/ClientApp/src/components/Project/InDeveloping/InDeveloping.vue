<template>
  <v-flex xs12>
    <transition
      name="InDeveloping"
      v-bind:css="false"
      v-on:before-enter="beforeEnter"
      v-on:enter="InDevelopingEnter"
      v-on:leave="InDevelopingLeave"
    >
      <v-layout align-end justify-space-between v-if="type===1">
        <Progress
          :progress="newProgress"
          @update:progress="newProgress = $event; $emit('update:progress', newProgress)"
        ></Progress>
        <deadline
          :deadline="newDeadline"
          @update:deadline="newDeadline = $event; $emit('update:deadline', newDeadline)"
        ></deadline>
      </v-layout>
    </transition>
  </v-flex>
</template>

<script>
import 'velocity-animate'

import Progress from './Progress.vue'
import Deadline from './Deadline.vue'

export default {
  props: ['type', 'progress', 'deadline'],
  components: {
    Progress,
    Deadline
  },
  data() {
    return {
      newProgress: this.progress,
      newDeadline: this.deadline
    }
  },
  methods: {
    beforeEnter(el) {
      el.style.opacity = 0
      el.style.height = 0
    },
    InDevelopingEnter(el, done) {
      Velocity(el, { opacity: 1, height: '116px' }, { complete: done })
    },
    InDevelopingLeave(el, done) {
      Velocity(el, { opacity: 0, height: 0 }, { complete: done })
    }
  }
}
</script>


