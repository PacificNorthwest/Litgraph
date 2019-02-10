<template>
  <div class="loading-container-outer">
    <div class="loading-container-middle">
      <div class="loading-container-inner">
        <div class="spinner">
          <div class="double-bounce1"></div>
          <div class="double-bounce2"></div>
        </div>
        <h2 class="loading-text">Loading</h2>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { mapActions, mapMutations } from "vuex";

export default Vue.extend({
  name: "OidcCallback",
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

<style scoped>
@import "./../css/dots-spinner-animation.css";

.loading-container-outer {
  background: url("./../images/tic-tac-toe.png") repeat;
  width: 100%;
  height: -webkit-fill-available;
  display: table;
  position: absolute;
  top: 0;
  left: 0;
}
.loading-container-middle {
  display: table-cell;
  vertical-align: middle;
}
.loading-container-inner {
  margin-left: auto;
  margin-right: auto;
  width: fit-content;
  text-align: center;
}

.spinner {
  margin-bottom: 100px;
}
.loading-text {
  text-align: center;
  width: 100%;
  /* margin-top: 200px; */
  font-size: 55px;
}
</style>


