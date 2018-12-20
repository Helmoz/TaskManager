<template>
  <v-flex xs12 class="mt-3">
    <v-subheader class="pl-0">Добавить участника</v-subheader>
    <v-flex xs12 sm6>
      <v-text-field
        v-model="email"
        append-icon="search"
        label="Email"
        @keyup.enter="loadMember"
        @click:append="loadMember"
      ></v-text-field>
      <transition name="fade" mode="out-in">
        <v-list-tile avatar @click v-if="member" key="member">
          <v-list-tile-avatar>
            <img :src="member.photoUrl">
          </v-list-tile-avatar>

          <v-list-tile-content>
            <v-list-tile-title v-html="member.name"></v-list-tile-title>
          </v-list-tile-content>

          <v-list-tile-action @click="addMember">
            <v-icon color="primary">add</v-icon>
          </v-list-tile-action>
        </v-list-tile>

        <v-list-tile avatar v-else-if="member == '' && email != ''" key="NotFound">
          <v-list-tile-content>
            <v-list-tile-title>Участник с таким Email не найден</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </transition>
    </v-flex>
  </v-flex>
</template>

<script>
import { mapActions } from 'vuex'

export default {
  props: ['members'],
  data() {
    return {
      member: null,
      email: '',
      newMembers: this.members
    }
  },
  watch: {
    members() {
      this.newMembers = this.members
    }
  },
  methods: {
    ...mapActions(['loadMemberByEmail']),
    addMember() {
      this.newMembers.push({ member: this.member })
      this.email = ''
      this.$emit('update:members', this.newMembers)
    },
    loadMember() {
      this.loadMemberByEmail(this.email).then(member => {
        this.member = member.data
      })
    },
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


