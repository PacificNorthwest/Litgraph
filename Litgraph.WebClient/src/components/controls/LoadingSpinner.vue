<template>
    <div class="spinner">
      <div class="double-bounce1"></div>
      <div class="double-bounce2"></div>
    </div>
</template>

<script lang="ts">
import Vue from 'vue'
export default Vue.extend({ name: 'loading-spinner' });
</script>

<style lang="scss" scoped>
@import "./../../styles/dots-spinner-animation.scss";
</style>