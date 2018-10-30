import Vuex from "vuex"
import Vue from "vue"
import Axios from "axios"

Vue.use(Vuex);

const accountModule = {
    namespaced: true,
    state: {
        token: localStorage.getItem('token') || ''
    },
    mutations: {
        enrollToken(state: any, token: string) {
            localStorage.setItem('token', token);
            Axios.defaults.headers.common['Authorization'] = 'Bearer ' + token;
            state.token = token;
        },
        dropToken(state: any) {
            localStorage.removeItem('token');
            Axios.defaults.headers.common['Authorization'] = '';
            state.token = '';
        }
    },
    getters: {
        isLoggedIn: (state: any) => state.token != '',
        token: (state: any) => state.token
    }
}

const store = new Vuex.Store({
    modules: {
        identity: accountModule
    }
})
export default store;