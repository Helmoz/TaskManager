<template>
  <v-container class="pr-0 pl-0 pb-0">
    <list-of-members
      :members="newMembers"
      @update:members="newMembers = $event; $emit('update:members', newMembers)"
      @addingShow="show = !show"
    ></list-of-members>
    <transition
      name="Adding"
      :css="false"
      v-on:before-enter="beforeEnter"
      v-on:enter="AddingEnter"
      v-on:leave="AddingLeave"
    >
      <add-member
        v-if="show"
        :members="newMembers"
        @update:members="newMembers = $event; $emit('update:members', newMembers)"
      ></add-member>
    </transition>
  </v-container>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'

import ListOfMembers from './ListOfMembers.vue'
import AddMember from './AddMember.vue'

export default {
  props: ['members'],
  data() {
    return {
      show: false,
      newMembers: this.members
    }
  },
  components: {
    ListOfMembers,
    AddMember
  },
  computed: {},
  methods: {
    beforeEnter(el) {
      el.style.opacity = 0
      el.style.height = 0
    },
    AddingEnter(el, done) {
      Velocity(el, { opacity: 1, height: '170px' }, { complete: done })
    },
    AddingLeave(el, done) {
      Velocity(el, { opacity: 0, height: 0, translateY: '-170px' }, { complete: done })
    }
  }
}
</script>


