<template>
  <div width="100%" style="overflow: hidden">
    <transition
      mode="out-in"
      name="router-anim"
      enter-active-class="animated faster fadeIn"
      leave-active-class="animated faster fadeOut">
      <template v-if="isFatalError">
        <error></error>
      </template>
      <template v-else>
        <dashboard v-if="isLoggedIn"></dashboard>
        <router-view v-else></router-view>
      </template>
    </transition>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

import dashboard from "./Dashboard.vue";
import error from "./Error.vue";

import { mapGetters } from "vuex";

@Component({
  components: { dashboard, error },
  computed: {
    ...mapGetters({
      isLoggedIn: "identity/oidcIsAuthenticated",
      isFatalError: "errors/isFatal"
    })
  }
})
export default class AppComponent extends Vue { }
</script>

<style>
@import "./../css/common.css";

body {
    overflow: hidden;
    margin: 0;
}

h1,
h2,
h3 {
  color: #2eccfa;
  font-family: "Charmonman";
  cursor: default;
  font-weight: bold;
  -webkit-touch-callout: none;
  -webkit-user-select: none;
  -khtml-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
}

.litgraph-title {
  margin: 20% auto 0px auto;
  font-size: 100px;
}
</style>