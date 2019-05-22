<template>
  <div class="pa-3 xs12">
    <div v-if="$apollo.queries.materials.loading" class="container-outer">
      <div class="container-middle">
        <div class="container-inner">
          <loading-spinner></loading-spinner>
        </div>
      </div>
    </div>
    <div v-else>
      <v-layout row wrap fluid>
        <v-flex pa-2 xs12 sm6 md3>
          <button class="new-material-item-button">
            <v-icon size="50">add</v-icon>
            <div>Add material</div>
          </button>
        </v-flex>
        <v-flex v-for="material in materials" :key="material.id" xs12 sm6 md4 grow="1" style="max-width: 100%">
          <material-card class="material-card ma-2" :material="material"></material-card>
        </v-flex>
      </v-layout>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import gql from "graphql-tag";
import Component from "vue-class-component";
import { mapGetters, mapActions } from "vuex";
import { Material } from "./../models/material";

import materialcard from "./controls/MaterialCard.vue";
import spinner from "./controls/LoadingSpinner.vue";

@Component({
  components: {
    "material-card": materialcard,
    "loading-spinner": spinner
  },
  computed: {
    ...mapGetters({
      user: "identity/oidcUser"
    })
  },
  apollo: {
    materials: {
      query: gql`
        query LoadMaterials($userEmail: String!) {
          materials(userEmail: $userEmail) {
            title
            brief
          }
        }
      `,
      variables() {
        return {
          userEmail: this.user.email
        };
      }
    }
  }
})
export default class MaterialsCollectionComponent extends Vue {
  materials: Material[] = [];
}
</script>

<style lang="scss" scoped>
@import "./../styles/common.scss";
@import "./../styles/centered-container.scss";

.new-material-item-button {
  width: 100%;
  height: 100%;
  min-height: 250px;
  border-radius: 10px;
  border: dashed 4px darkgray;
  background: transparent;
  font-size: 30px;
  opacity: 0.6;

  &:hover {
      cursor: pointer;
      border-color: gray;
  }

  &:focus {
      outline: none
  }
}
</style>


