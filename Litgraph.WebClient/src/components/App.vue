<template>
    <div width=100%>
        <SignIn/>
    </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component"

import SignIn from "./SignIn.vue"

@Component({components: {SignIn}})
export default class AppComponent extends Vue {
    
}
</script>

<style scoped>
    
</style>

