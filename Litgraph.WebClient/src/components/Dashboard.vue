<template>
    <div>
        <h1 v-if="$store.getters['identity/isLoggedIn']">Your token: {{ $store.state.identity.token }}</h1>
        <h1 v-else>Token is empty!</h1>
        <h1>Is logged in: {{ $store.getters['identity/isLoggedIn'] }}</h1>
    </div>
</template>

<script lang="ts">
import Vue from 'vue'
import Component from "vue-class-component"

@Component
export default class DashboardComponent extends Vue {
    
}
</script>

