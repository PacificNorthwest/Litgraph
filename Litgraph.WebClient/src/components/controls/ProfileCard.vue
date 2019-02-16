<template>
  <span
    ref="container"
    class="popover-container"
    @click="revealCard = !revealCard"
    @blur="revealCard = false"
  >
    <slot name="activator"></slot>
    <v-slide-y-transition>
      <v-card class="profile-card dynamic-arrow elevation-15" v-show="revealCard">
        <v-img id="profile-picture" contain :src="defaultProfileImage"></v-img>
        <v-card-text primary-title fluid grid-list-1 class="pa-5">
          <h2 class="title mb-0">Username:</h2>
          <h2 class="headline mb-3">{{ user.name }}</h2>
          <h2 class="title mb-0">Email:</h2>
          <h2 class="headline mb-0">{{ user.email }}</h2>
          <div class="logout-container">
            <v-btn flat large color="#2eccfa" class="logout-button mt-5" @click.prevent="signout">Sign out</v-btn>
          </div>
        </v-card-text>
      </v-card>
    </v-slide-y-transition>
  </span>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { mapGetters, mapActions } from "vuex";
import CssUtils from "./../../services/utils/css";

import * as DefaultProfileImage from "./../../images/profile.svg";

@Component({
  name: "profile-card",
  computed: {
    ...mapGetters({
      user: "identity/oidcUser"
    })
  },
  methods: {
      ...mapActions({
          signout: "identity/signOutOidc"
      })
  }
})
export default class ProfileCardComponent extends Vue {
  defaultProfileImage: any = DefaultProfileImage;
  revealCard: boolean = false;

  mounted() {
    let anchorWidth = (this.$refs.container as any).clientWidth;
    CssUtils.createCssSelector(
      "dynamic-arrow::before",
      `{ right: ${anchorWidth / 2 - 15}px }`
    );
  }
}
</script>

<style scoped>
.profile-card {
  width: 500px;
  height: fit-content;
  position: absolute;
  direction: ltr;
  top: 115%;
}
.profile-card::before {
  bottom: 100%;
  border: solid transparent;
  content: " ";
  height: 0;
  width: 0;
  position: absolute;
  pointer-events: none;
  border-bottom-color: white;
  border-width: 15px;
}

.popover-container {
  direction: rtl;
  display: flex;
}

#profile-picture {
  max-height: 150px;
  margin: 40px auto;
}

.logout-button {
  font-size: 25px;
  padding: 10px 25px;
}

.logout-container {
  text-align: center;
  margin: 30px auto;
}
</style>
