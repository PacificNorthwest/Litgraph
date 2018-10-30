<template>
    <div width=100%>
        <transition mode="out-in" 
                    name="router-anim" 
                    enter-active-class="animated faster fadeIn" 
                    leave-active-class="animated faster fadeOut">
            <router-view></router-view>
        </transition>
    </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

@Component
export default class AppComponent extends Vue {
    
 }
</script>

<style>
@import "./../css/animate.css";

.litgraph-title {
    margin: 20% auto 0px auto;
    color: #2eccfa;
    font-size: 100px;
    font-family: "Charmonman";
    cursor: default;
    -webkit-touch-callout: none;
    -webkit-user-select: none;
    -khtml-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
}
</style>


