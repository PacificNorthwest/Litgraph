<template>
  <styled-container>
    <h2 class="styled-title mb-5" style="font-size: 50px">Welcome to</h2>
    <h1 class="styled-title" style="font-size: 100px; margin-bottom: 150px">Litgraph</h1>
    <v-btn depressed large class="mt-5 primary-colored" to="/dashboard">Dashboard</v-btn>
  </styled-container>
</template>

<script lang="ts">
import Vue from "vue";
import StyledContainer from "./controls/StyledContainer.vue";

export default Vue.extend({
  components: { "styled-container": StyledContainer }
});
</script>

<style lang="scss" scoped>
@import "./../styles/common.scss";
</style>




