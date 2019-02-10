import Vuex from "vuex"
import Vue from "vue"
import Axios from "axios"
import { vuexOidcCreateStoreModule } from "vuex-oidc"

import oidcConfig from "./oidc.json"

Vue.use(Vuex);

const errorModule = {
    namespaced: true,
    state: {
        isFatal: false
    },
    mutations: {
        reportError(state: any) {
            state.isFatal = true;
        }
    },
    getters: {
        isFatal: (state: any) => state.isFatal
    }
}

const store = new Vuex.Store({
    modules: {
        identity: vuexOidcCreateStoreModule(oidcConfig, { namespaced: true }),
        errors: errorModule
    }
})
export default store;