<template>
  <v-app width="100%" style="overflow: hidden">
    <transition
      mode="out-in"
      name="router-anim"
      enter-active-class="animated faster fadeIn"
      leave-active-class="animated faster fadeOut">
      <template v-if="isFatalError">
        <error></error>
      </template>
      <template v-else>
        <router-view></router-view>
      </template>
    </transition>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

import dashboard from "./Dashboard.vue";
import error from "./Error.vue";

import { mapGetters } from "vuex";

@Component({
  components: { error },
  computed: {
    ...mapGetters({
      isLoggedIn: "identity/oidcIsAuthenticated",
      isFatalError: "errors/isFatal"
    })
  }
})
export default class AppComponent extends Vue { }
</script>

<style lang="scss">
@import "./../styles/common.scss";

body {
    margin: 0;
}

::-webkit-scrollbar {
  width: 0;
}

.styled-title {
  @extend .primary-colored;
  @extend %unselectable;

  font-family: $font-stack;
  font-weight: bold;
}
</style>