<template>
    <div width=100% style="overflow: hidden">
        
        <transition mode="out-in" 
                    name="router-anim" 
                    enter-active-class="animated faster fadeIn" 
                    leave-active-class="animated faster fadeOut">

            <dashboard v-if="$store.getters['identity/isLoggedIn']"></dashboard>
            <router-view v-else></router-view>
        </transition>
        
    </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

import dashboard from "./Dashboard.vue"

@Component({ components: { dashboard } })
export default class AppComponent extends Vue { }
</script>

<style>
@import "./../css/animate.css";
@import "./../css/hamburger.css";


.litgraph-title {
  margin: 20% auto 0px auto;
  color: #2eccfa;
  font-size: 100px;
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
</style>