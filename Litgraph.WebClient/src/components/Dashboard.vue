<template>
    <div class="dashboard-container">
        <span class="dashboard-header">
            <h1 class="litgraph-title title">Litgraph</h1>

            <span class="breadcrumb">
                <span>Brave new world</span>
                <span>Characters</span>
                <span>John</span>
            </span>

            <span class="header-menu">
                <input placeholder="Search" class="search-input"/>
                <el-button class="icon-button" circle><img src="./../icons/search.svg" class="icon" alt=""/></el-button>

                <el-popover placement="bottom" trigger="click" width="200" :title="user.name" content="Profile information">
                    <el-button class="icon-button" slot="reference" circle><img src="./../icons/user.svg" class="icon" alt=""/></el-button>
                </el-popover>
            </span>
            
        </span>
        <div class="sidebar" :style="{ width: isSideMenuExpanded ? '300px' : '70px'}">
            <button class="hamburger hamburger-button hamburger--collapse" :class="{ 'is-active': isSideMenuExpanded }" style="color: white"
                    @click="isSideMenuExpanded = !isSideMenuExpanded">
                    <span class="hamburger-box">
                        <span class="hamburger-inner"></span>
                    </span>
            </button>
        </div>

        <div>
            <!-- Dashboard content here -->
        </div>
    </div>
</template>

<script lang="ts">
import Vue from 'vue'
import Component from "vue-class-component"
import { mapGetters } from 'vuex';

@Component({
    computed: {
        ...mapGetters({
            'user': 'identity/oidcUser'
        })
    }
})
export default class DashboardComponent extends Vue {
  isSideMenuExpanded: boolean = false;
}
</script>

<style scoped>
@import "./../css/hamburger.css";

.dashboard-container {
    overflow: hidden;
}

.sidebar {
  float: left;
  position: relative;
  margin-right: 15px;
  z-index: 1;
  height: -webkit-fill-available;
  width: 50px;
  border-right: 1px solid #2eccfa;
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

.header-menu {
    display: inline-flex;
    position: absolute;
    right: 30px;
    height: 67px;
    line-height: 67px;
}

.search-input {
    background-color: transparent;
    color:white;
    margin: 5px -30px 5px auto;
    padding-top: 25px;
    width: 360px;
    border: 0px;
    border-radius: 0px;
    border-bottom: 1px solid #2eccfa;
    font-size: 18px;
}
.search-input:focus {
    outline: none
}
.search-input::placeholder {
    color: white;
    opacity: 0.4;
}

.icon-button {
    background-color: #3a4660;
    border: hidden;
    margin: 5px auto
}
.icon-button:hover {
    background-color: #43516e;
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
    height: 67px;
    display: -webkit-box;
    line-height: 67px; 
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


