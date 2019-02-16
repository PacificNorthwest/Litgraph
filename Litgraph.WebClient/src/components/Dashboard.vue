<template>
  <div class="dashboard-container">
    <v-toolbar class="dashboard-header">
      <v-toolbar-title class="styled-title dashboard-title">Litgraph</v-toolbar-title>
      <v-breadcrumbs :items="breadcrumbItems" class="depth-navigation-menu">
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
        <v-btn fab class="icon-button circle" @click="toggleSearchField">
          <v-icon color="#2eccfa" large>search</v-icon>
        </v-btn>

        <profile-card>
          <v-btn fab slot="activator" class="icon-button circle">
            <v-icon color="#2eccfa" large>person</v-icon>
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

<style scoped>
@import "./../css/hamburger.css";

.dashboard-container {
  overflow: hidden;
  height: 100%;
}

.sidebar {
  background-color: #43516e;
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

.dashboard-header {
  background-color: #3a4660;
  padding: 10px;
}

.depth-navigation-menu {
  margin-left: 60px;
}

.depth-navigation-item {
  color: white;
  font-size: 22px;
  text-decoration: none;
}

.dashboard-title {
  padding: 5px 20px 5px 5px;
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
  transform: translateX(50px);
}

.icon-button {
  background-color: #3a4660 !important;
  border: hidden;
  margin: 10px;
  padding: 10px;
}
.icon-button:hover {
  background-color: #43516e;
}
.icon-button:focus {
  background-color: #3a4660;
  border: hidden;
}

.dashboard-content {
  display: flex;
}
</style>


