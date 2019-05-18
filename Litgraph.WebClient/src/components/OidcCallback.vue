<template>
  <styled-container>
    <loading-spinner class="spinner"></loading-spinner>
    <h2 class="styled-title loading-text">Loading</h2>
  </styled-container>
</template>

<script lang="ts">
import Vue from "vue";
import { mapActions, mapMutations } from "vuex";

import StyledContainer from "./controls/StyledContainer.vue";
import Spinner from "./controls/LoadingSpinner.vue"

export default Vue.extend({
  name: "OidcCallback",
  components: { 
    "styled-container": StyledContainer,
    "loading-spinner": Spinner
  },
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


