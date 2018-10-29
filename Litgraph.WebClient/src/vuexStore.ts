import Vuex from "vuex"
import Vue from "vue"

Vue.use(Vuex);

const accountModule = {
    namespaced: true,
    state: {
        token: localStorage.getItem('token') || '',
        userName: '',
        role: '',
    },
    mutations: {
        enrollToken(state: any, token: string) {
            localStorage.setItem('token', token);
            state.token = token;
        },
        dropToken(state: any) {
            localStorage.removeItem('token');
            state.token = '';
        }
    },
    getters: {
        isLoggedIn: (state: any) => typeof state.token != 'undefined' && state.token != ''
    }
}

const store = new Vuex.Store({
    modules: {
        identity: accountModule
    }
})
export default store;