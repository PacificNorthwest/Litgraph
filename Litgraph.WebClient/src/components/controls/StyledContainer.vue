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
import Vue from 'vue'
export default Vue.extend({
    name: "styled-container"
})
</script>

<style scoped>
.container-outer {
 background: url("./../../images/topography.png") repeat;
  width: 100%;
  height: -webkit-fill-available;
  display: table;
  position: absolute;
  top: 0;
  left: 0;
}
.container-middle {
    display: table-cell;
  vertical-align: middle;
}
.container-inner {
margin-left: auto;
  margin-right: auto;
  width: fit-content;
  text-align: center;
}
</style>


