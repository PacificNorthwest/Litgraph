<template>
    <div>
        <div id="banner-container">
            <img src="./../images/book.jpg" id="banner" alt="">
        </div>
        <div id="signin-container">
            <div id="signin-background"></div>
            <h1 class="litgraph-title">Litgraph</h1>
            <div>
                <el-card class="signin-card">
                    <el-input placeholder="Username" class="signin-input" type="text" spellcheck="false" v-model="userName"></el-input>
                    <div style="margin-bottom:45px">
                        <el-input placeholder="Password" class="signin-input" style="margin: 15px 0px 7px 0px" type="password" v-model="password"></el-input>
                        <button id="password-reminder" v-on:click="$alert('Well thats too bad =( Shame on you!', { confirmButtonText: 'Ok' })">Forgot password?</button>
                    </div>
                    <el-button type="primary" style="font-size: 20px" :loading="loading" @click="signIn()">Sign in</el-button>
                </el-card>
                <router-link to="/signup">
                    <el-button class="signup">Create account</el-button>
                </router-link>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

import UserService from "./../services/userService";
import { RequestError } from "./../tools/httpclient";

@Component
export default class SignInComponent extends Vue {
  userName: string = "";
  password: string = "";

  loading: boolean = false;

  async signIn() {
    if (this.userName !== "" && this.password !== "") {
      try {
        this.loading = true;
        if (await UserService.signIn(this.userName, this.password)) {
          this.$router.push('/');
        }
      } catch (e) {
          this.$message.error(e.message);
      }
      this.loading = false;
    }
  }
}

</script>

<style scoped>
#banner-container {
  float: left;
  position: absolute;
  width: 50%;
  height: -webkit-fill-available;
  border-right: 1px #2eccfa solid;
  overflow: hidden;
}
#banner {
  height: -webkit-fill-available;
  -webkit-animation: slide 80s linear infinite;
}
@-webkit-keyframes slide {
  0% {
    transform: translate3d(0, 0, 0);
  }
  50% {
    transform: translate3d(-400px, 0, 0);
  }
  100% {
    transform: translate3d(0, 0, 0);
  }
}

#signin-container {
  float: left;
  position: relative;
  left: 50%;
  width: 50%;
  height: -webkit-fill-available;
  text-align: center;
}
#signin-background {
  z-index: -1;
  position: absolute;
  width: 100%;
  opacity: 0.5;
  height: -webkit-fill-available;
  background: url("./../images/tic-tac-toe.png") repeat;
}
#password-reminder {
  font-size: 10px;
  float: right;
  background: none;
  color: inherit;
  border: none;
  padding: 0;
  border-bottom: 1px solid #444;
  cursor: pointer;
}
#password-reminder:focus {
  outline: 0;
}

.signin-card {
  margin: 10% 25% 10px 25%;
  padding-bottom: 10px
}

.signin-input {
  font-size: 20px;
}

.signup {
  position: relative;
  float: right;
  margin-right: 25%;
  font-size: 16px;
}
</style>


