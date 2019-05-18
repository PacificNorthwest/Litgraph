<template>
  <div class="container-outer">
    <div class="container-middle">
      <div class="container-inner">
        <slot></slot>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
export default Vue.extend({ name: "styled-container" });
</script>

<style lang="scss" scoped>
@import "./../../styles/centered-container.scss";

.container-outer {
    background: url("./../../images/topography.png") repeat;
}
</style>


