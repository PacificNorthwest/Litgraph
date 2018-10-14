<template>
    <div>
        <h1>Welcome to Ligraph!</h1>
        <span class="offset-element">Please fill in your name:</span>
        <input type="text" v-model="name"/>
        <h1 class="offset-element">Your name: {{ name }} </h1>
    </div>
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

