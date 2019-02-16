<template>
  <styled-container>
    <h1 id="header" class="styled-title">:(</h1>
    <h3 id="message" class="styled-title">Some error seemed to occure
      <br>Try reloading the page
    </h3>
  </styled-container>
</template>

<script lang="ts">
import Vue from "vue";
import StyledContainer from "./controls/StyledContainer.vue";
export default Vue.extend({
  name: "Error",
  components: { "styled-container": StyledContainer }
});
</script>

<style scoped>
#header {
  font-size: 150px;
  margin-bottom: 100px
}

#message {
  font-size: 60px;
}
</style>
