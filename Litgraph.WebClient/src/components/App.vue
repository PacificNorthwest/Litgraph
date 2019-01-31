<template>
    <div width=100% style="overflow: hidden">
        <span class="dashboard-header" v-show="$store.getters['identity/isLoggedIn']">
            <h1 class="litgraph-title title">Litgraph</h1>

            <span class="breadcrumb">
                <span>Brave new world</span>
                <span>Characters</span>
                <span>John</span>

            </span>

            <el-button class="icon-button" circle><img src="./../icons/search.svg" class="icon" alt=""/></el-button>
        </span>
        <div class="sidebar" v-show="$store.getters['identity/isLoggedIn']" 
             :style="{ width: isSideMenuExpanded ? '300px' : '70px'}">
            <button class="hamburger hamburger-button hamburger--collapse" :class="{ 'is-active': isSideMenuExpanded }" style="color: white"
                    @click="isSideMenuExpanded = !isSideMenuExpanded">
                    <span class="hamburger-box">
                        <span class="hamburger-inner"></span>
                    </span>
            </button>
        </div>

        <transition mode="out-in" 
                    name="router-anim" 
                    enter-active-class="animated faster fadeIn" 
                    leave-active-class="animated faster fadeOut">
            <router-view></router-view>
        </transition>
        
    </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

@Component
export default class AppComponent extends Vue {
  isSideMenuExpanded: boolean = false;
}
</script>

<style>
@import "./../css/animate.css";
@import "./../css/hamburger.css";


.litgraph-title {
  margin: 20% auto 0px auto;
  color: #2eccfa;
  font-size: 100px;
  font-family: "Charmonman";
  cursor: default;
  -webkit-touch-callout: none;
  -webkit-user-select: none;
  -khtml-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
}

.sidebar {
  float: left;
  position: relative;
  margin-right: 15px;
  z-index: 1;
  height: -webkit-fill-available;
  width: 50px;
  border-right: 1px solid #2eccfa;
  /* background-color: #484848; */
  background-color: #43516e;

  -webkit-transition: width 0.6s;
  transition: width 0.6s;
  color: white
}

.hamburger-button:focus {
    outline: 0
}

.hamburger-inner, 
.hamburger-inner::before, 
.hamburger-inner::after, 
.hamburger.is-active .hamburger-inner, 
.hamburger.is-active .hamburger-inner::before, 
.hamburger.is-active .hamburger-inner::after {
    background-color: white
}

.dashboard-header {
    display: -webkit-box;
    width: 100%;
    background-color: #3a4660;
    height: fit-content;
    text-align: left
}

.dashboard-header .title {
    margin: 5px 25px;
    font-size: 35px;
}

.icon-button {
    position: absolute;
    right: 30px;
    background-color: #3a4660;
    border: hidden;
    margin: 5px auto
}
.icon-button:hover {
    background-color: #43516e;
    border: 1px solid #43516e;
}
.icon-button:focus {
    background-color: #3a4660;
    border: hidden;
}

.icon {
    width: 35px
}

.breadcrumb {
    color: white;
    font-size: 22px;
    margin: 5px 55px;
    height: 60px;
    display: -webkit-box;
    line-height: 60px; 
}
.breadcrumb span::after {
    background-image: url(./../icons/arrow-right.svg);
    background-repeat: no-repeat;
    background-size: 15px 15px;
    content: " ";
    width: 15px;
    height: 15px;
    display: -webkit-inline-box;
    margin: auto 3px auto 10px
}
.breadcrumb span:last-child::after {
    content: none;
    display: none
}
</style>