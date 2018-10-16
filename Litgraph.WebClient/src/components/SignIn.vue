<template>
    <div>
        <div id="banner-container">
            <img src="./../images/book.jpg" id="banner" alt="">
        </div>
        <div id="signin-container">
            <div id="signin-background"></div>
            <h1>Litgraph</h1>
            <el-card class="signin-card">
                <el-input placeholder="Username" class="signin-input" type="text" spellcheck="false"></el-input>
                <div style="margin-bottom:15px">
                    <el-input placeholder="Password" class="signin-input" style="margin: 15px 0px 7px 0px" type="password"></el-input>
                    <button id="password-reminder" v-on:click="$alert('Well thats too bad =( Shame on you!', { confirmButtonText: 'Ok' })">Forgot password?</button>
                </div>
                <el-button type="primary" style="font-size: 20px" round>Sign in</el-button>
            </el-card>
        </div>
    </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

@Component
export default class SignInComponent extends Vue {}
</script>

<style scoped>
#banner-container {
        float: left;
        position: absolute;
        width: 50%;
        height: -webkit-fill-available;
        border-right: 1px #2ECCFA solid;
        overflow: hidden
    }
    #banner {
        height: -webkit-fill-available;
        -webkit-animation: slide 80s linear infinite
    }
    @-webkit-keyframes slide {
        0% { transform: translate3d(0, 0, 0) }
        50% { transform: translate3d(-400px, 0, 0) }
        100% { transform: translate3d(0, 0, 0) }
    }

    #signin-container {
        float: left;
        position: relative;
        left: 50%;
        width: 50%;
        height: -webkit-fill-available;
        text-align: center
    }
    #signin-background {
        z-index: -1;
        position: absolute;
        width: 100%;
        opacity: 0.5;
        height: -webkit-fill-available;
        background: url("./../images/tic-tac-toe.png") repeat
    }
    #password-reminder {
        font-size: 10px;
        float: right;
        background:none;
        color:inherit;
        border:none; 
        padding:0;
        border-bottom:1px solid #444; 
        cursor: pointer;
    }

    .signin-card {
        margin: 10% 25% 20% 25%;
    }

    .signin-input {
        font-size: 20px
    }

    h1 {
        margin: 20% auto 0px auto;
        color: #2ECCFA;
        font-size: 100px;
        font-family: 'Charmonman'
    }
</style>


