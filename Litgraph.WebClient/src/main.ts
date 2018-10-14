import Vue from "vue";
import AppComponent from "./components/App.vue"

let v = new Vue({
    el: "#vue-app",
    render: createEle => createEle(AppComponent)
})