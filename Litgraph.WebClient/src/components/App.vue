<template>
    <el-button>Test</el-button>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component"

@Component
export default class AppComponent extends Vue {
    name: string = "";
}
</script>

<style scoped>
    .offset-element {
        margin-top: 100px;
    }
</style>

