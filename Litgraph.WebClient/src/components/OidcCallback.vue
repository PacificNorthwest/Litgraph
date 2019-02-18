<template>
  <styled-container>
    <div class="spinner">
      <div class="double-bounce1"></div>
      <div class="double-bounce2"></div>
    </div>
    <h2 class="styled-title loading-text">Loading</h2>
  </styled-container>
</template>

<script lang="ts">
import Vue from "vue";
import { mapActions, mapMutations } from "vuex";

import StyledContainer from "./controls/StyledContainer.vue";

export default Vue.extend({
  name: "OidcCallback",
  components: { "styled-container": StyledContainer },
  methods: {
    ...mapActions({
      oidcSignInCallback: "identity/oidcSignInCallback"
    })
  },
  mounted() {
    this.oidcSignInCallback()
      .then(redirectPath => {
        this.$router.push(redirectPath);
      })
      .catch(err => {
        console.error(err);
        this.$store.commit("errors/reportError");
      });
  }
});
</script>

<style lang="scss" scoped>
@import "./../styles/dots-spinner-animation.scss";

.spinner {
  margin-bottom: 100px;
}

.loading-text {
  text-align: center;
  width: 100%;
  font-size: 65px;
}
</style>


