import Vuex from "vuex"
import Vue from "vue"

Vue.use(Vuex);

const accountModule = {
    state: {
        token: '',
        userName: '',
        role: '',
    }
}

const storage = new Vuex.Store({
    modules: {
        accountModule: accountModule
    }
})

export default storage;