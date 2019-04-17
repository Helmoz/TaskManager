<template>
  <v-flex xs12>
    <v-subheader class="pl-0"
      >Участники
      <v-chip class="ml-3" label outline color="primary">{{ members ? members.length : '0' }}</v-chip>
      <v-btn round outline color="primary" @click="$emit('addingShow')">Добавить участника</v-btn>
    </v-subheader>
    <v-list two-line v-if="newMembers">
      <transition-group name="MembersList" v-bind:css="false" v-on:before-enter="beforeEnter" v-on:enter="MembersEnter" v-on:leave="MembersLeave">
        <v-list-tile v-for="(item, index) in newMembers" :key="index" avatar v-bind:data-index="index">
          <v-list-tile-avatar>
            <img :src="item.photoUrl" />
          </v-list-tile-avatar>

          <v-list-tile-content>
            <v-list-tile-title>{{ item.name }}</v-list-tile-title>
            <v-list-tile-sub-title>{{ item.email }}</v-list-tile-sub-title>
          </v-list-tile-content>

          <v-list-tile-action>
            <v-btn icon ripple @click="removeMember(item)">
              <v-icon color="grey lighten-1">delete</v-icon>
            </v-btn>
          </v-list-tile-action>
        </v-list-tile>
      </transition-group>
    </v-list>
  </v-flex>
</template>

<script>
export default {
  props: ['members'],
  data() {
    return {
      newMembers: this.members.map(x => x.member)
    }
  },
  watch: {
    members() {
      this.newMembers = this.members.map(x => x.member)
    }
  },
  methods: {
    removeMember(member) {
      this.newMembers.splice(this.newMembers.indexOf(member), 1)
      this.newMembers = [...this.newMembers]
      this.$emit('update:members', this.newMembers)
    },
    beforeEnter(el) {
      el.style.opacity = 0
      el.style.height = 0
    },
    MembersEnter(el, done) {
      Velocity(el, { opacity: 1, height: '72px' }, { complete: done })
    },
    MembersLeave(el, done) {
      Velocity(el, { opacity: 0, height: 0 }, { complete: done })
    }
  }
}
</script>


