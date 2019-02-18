<template>
  <div class="dashboard-container">
    <v-toolbar class="dashboard-header">
      <v-toolbar-title class="styled-title dashboard-title">Litgraph</v-toolbar-title>
      <v-breadcrumbs :items="breadcrumbItems" class="ml-5">
        <v-icon color="white" slot="divider">arrow_forward_ios</v-icon>
        <template slot="item" slot-scope="props">
          <a href class="depth-navigation-item">{{ props.item }}</a>
        </template>
      </v-breadcrumbs>
      <v-spacer></v-spacer>
      <v-toolbar-items>
        <transition name="slide-fade">
          <v-autocomplete
            class="search-input"
            placeholder="Start typing to search"
            color="white"
            dark
            hide-no-data
            hide-selected
            v-show="revealSearchField"
            @blur="revealSearchField = false"
            ref="searchField"
            append-icon="none"
          ></v-autocomplete>
        </transition>
        <v-btn fab class="icon-button" @click="toggleSearchField">
          <v-icon class="primary-colored" large>search</v-icon>
        </v-btn>

        <profile-card>
          <v-btn fab slot="activator" class="icon-button">
            <v-icon class="primary-colored" large>person</v-icon>
          </v-btn>
        </profile-card>
         
      </v-toolbar-items>
    </v-toolbar>

    <v-navigation-drawer class="sidebar" :mini-variant="!isSideMenuExpanded" app permanent clipped>
      <button
        class="hamburger hamburger-button hamburger--collapse"
        :class="{ 'is-active': isSideMenuExpanded }"
        style="color: white"
        @click="isSideMenuExpanded = !isSideMenuExpanded"
      >
        <span class="hamburger-box">
          <span class="hamburger-inner"></span>
        </span>
      </button>
    </v-navigation-drawer>

    <div class="dashboard-content">
      <router-view></router-view>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { mapGetters } from "vuex";

import profilecard from "./controls/ProfileCard.vue";

@Component({
  components: { 'profile-card': profilecard },
  computed: {
    ...mapGetters({
      user: "identity/oidcUser"
    })
  }
})
export default class DashboardComponent extends Vue {
  isSideMenuExpanded: boolean = false;
  revealSearchField: boolean = false;

  breadcrumbItems: string[] = ["Brave new world", "Characters", "John"];

  toggleSearchField() {
    this.revealSearchField = !this.revealSearchField;
    if (this.revealSearchField) {
      this.$nextTick((this.$refs.searchField as any).focus);
    }
  }
}
</script>

<style lang="scss" scoped>
@import "./../styles/hamburger.scss";
@import "./../styles/common.scss";

.dashboard-container {
  overflow: hidden;
  height: 100%;
}

.dashboard-header {
  background-color: $primary-dark-color;
  padding: 10px;
}

.depth-navigation-item {
  color: white;
  font-size: 22px;
  text-decoration: none;
}

.dashboard-title {
  padding: 5px;
  padding-right: 20px;
  font-size: 35px;
}

.search-input {
  background-color: transparent;
  color: white;
  margin: 5px auto 15px auto;
  width: 360px;
  font-size: 18px;
}

.slide-fade-enter-active,
.slide-fade-leave-active {
  transition: all 0.5s;
}
.slide-fade-enter,
.slide-fade-leave-to {
  opacity: 0;
  @include transform(translateX(50px))
}

.icon-button {
  @extend .circle;
  
  background-color: $primary-dark-color !important;
  border: hidden;
  margin: 10px;
  padding: 10px;
}
.icon-button:hover {
  background-color: $secondary-dark-color;
}
.icon-button:focus {
  background-color: $primary-dark-color;
  border: hidden;
}

.sidebar {
  background-color: $secondary-dark-color;
  position: relative;
  float: left;
}

.hamburger-button {
  margin: 0px 5px;
}
.hamburger-button:focus {
  outline: 0;
}

.hamburger-inner,
.hamburger-inner::before,
.hamburger-inner::after,
.hamburger.is-active .hamburger-inner,
.hamburger.is-active .hamburger-inner::before,
.hamburger.is-active .hamburger-inner::after {
  background-color: white;
}

.dashboard-content {
  display: flex;
}
</style>


