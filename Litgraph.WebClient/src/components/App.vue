<template>
    <div width=100%>
        <router-view></router-view>
    </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

@Component
export default class AppComponent extends Vue { }
</script>

<style>
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


