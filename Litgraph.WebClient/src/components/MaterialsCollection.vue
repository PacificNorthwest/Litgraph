<template>
  <v-flex pa-3 xs12>
    <div v-if="$apollo.queries.materials.loading" class="container-outer">
      <div class="container-middle">
        <div class="container-inner">
          <loading-spinner></loading-spinner>
        </div>
      </div>
    </div>
    <div v-else>
      <v-layout row wrap fluid>
        <v-flex pa-2 xs12 sm6 md4>
          <v-dialog v-model="dialog" max-width="900px" @input="v => v || newMaterial.clear()">
            <template v-slot:activator="{ on }">
              <button class="new-material-item-button" v-on="on">
                <v-icon size="50">add</v-icon>
                <div>Add material</div>
              </button>
            </template>
            <v-card style="position: relative">
              <v-card-title class="headline gray primary-colored dark justify-center" primary-title>Create new material</v-card-title>
              <v-card-text>
                <v-container>
                  <v-layout column>
                    <v-text-field label="Material title" v-model="newMaterial.base.title"></v-text-field>
                    <v-textarea label="Brief" v-model="newMaterial.base.brief"></v-textarea>
                    <v-dropzone id="images-dropzone" v-bind:options="fileUploadOptions"></v-dropzone>
                    <v-flex text-xs-center mt-5>
                      <v-btn class="create-material-button" v-on:click="createMaterial" :disabled="newMaterial.base.title.length == 0">Create</v-btn>
                    </v-flex>
                  </v-layout>
                </v-container>
              </v-card-text>
              <div class="create-material-busy-indicator" v-show="newMaterial.pendingState">
                <loading-spinner class="centered"></loading-spinner>
              </div>
            </v-card>
          </v-dialog>
        </v-flex>
        <v-flex v-for="material in materials" :key="material.id" xs12 sm6 md4 grow="1" style="max-width: 100%">
          <material-card class="material-card ma-2" :material="material"></material-card>
        </v-flex>
      </v-layout>
    </div>
  </v-flex>
</template>

<script lang="ts">
import Vue from "vue";
import gql from "graphql-tag";
import Component from "vue-class-component";
import { mapGetters, mapActions } from "vuex";
import { Material, IMaterial } from "./../models/material";

import loadMaterialsQuery from "./../graphql/LoadMaterials.gql"
import createMaterialQuery from "./../graphql/CreateMaterial.gql"

import materialcard from "./controls/MaterialCard.vue";
import spinner from "./controls/LoadingSpinner.vue";
import vueDropzone from "vue2-dropzone"

@Component({
  components: {
    "material-card": materialcard,
    "loading-spinner": spinner,
    "v-dropzone": vueDropzone
  },
  computed: {
    ...mapGetters({
      user: "identity/oidcUser"
    })
  },
  apollo: {
    materials: {
      query: loadMaterialsQuery,
      variables() {
        return {
          userEmail: this.user.email
        }
      }
    }
  }
})
export default class MaterialsCollectionComponent extends Vue {
  materials: Material[] = [];
  dialog: boolean = false;

  user: any;

  fileUploadOptions: any = {
    url: 'https://httpbin.org/post',
    addRemoveLinks: true
  }

  newMaterial = {
    base: <IMaterial> {
      title: '',
      brief: '',
    },
    
    pendingState: false,
    clear: (): void => {
      this.newMaterial.base.title = '';
      this.newMaterial.base.brief = '';
    }
  }

  async createMaterial() {
    this.newMaterial.pendingState = true;
    await this.$apollo.mutate({
      mutation: createMaterialQuery,
      variables: {
          email: this.user.email,
          title: this.newMaterial.base.title,
          brief: this.newMaterial.base.brief
      },
      update: (store, { data: { createMaterial } }) => {
        if (createMaterial.successful) {
          this.materials.unshift(createMaterial.material);
          // const cache = store.readQuery({ query: loadMaterialsQuery, variables: { userEmail: this.user.email } }) as any;
          // cache.materials.unshift(createMaterial.material);
          // store.writeQuery({ query: loadMaterialsQuery, data: cache });

          this.dialog = false;
          this.newMaterial.pendingState = false;
        }
      }
    })
  }
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
    outline: none;
  }
}

.create-material-button {
  color: $main-color;
  background-color: $primary-dark-color !important;
}

.centered {
  position: absolute;
  left: 50%;
  top: 50%;
  @include transform(translate3d(-50%, -50%, 0))
}

.create-material-busy-indicator {
  background: rgba($color: #000000, $alpha: 0.3);
  position: absolute;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
}
</style>


