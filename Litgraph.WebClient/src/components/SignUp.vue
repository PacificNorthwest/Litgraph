<template>
    <div class="container">
        <div id="signup-bg"></div>
        <h1 class="litgraph-title signup-title">Litgraph</h1>
        <div style="margin: 4% auto 1% auto">
            <span class="subtitle" style="margin-bottom: 5px">New to Litgraph?</span>
            <span class="subtitle">Create a free accout!</span>
        </div>
        <el-card class="signup-card">
            <el-form :model="signupModel" :rules="rules" ref="form">
                <el-form-item prop="username">
                    <el-input v-model="signupModel.username" placeholder="Choose a Username" type="text" spellcheck="false"></el-input>
                </el-form-item>
                <el-form-item prop="email">
                    <el-input v-model="signupModel.email" placeholder="Email address" type="text" spellcheck="false"></el-input>
                </el-form-item>
                <el-form-item prop="password">
                    <el-input v-model="signupModel.password" placeholder="Choose password" type="password"></el-input>
                </el-form-item>
                <el-form-item prop="passwordRepeat">
                    <el-input v-model="signupModel.passwordRepeat" placeholder="Repeat password" type="password"></el-input>
                </el-form-item>
            </el-form>

            <el-button class="signup-button" type="primary" @click="signUp" v-loading.fullscreen.lock="loading">Sign up</el-button>
        </el-card>
        <router-link to="/signin">
            <el-button class="signin">Sign in</el-button>
        </router-link>
    </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

import UserService from "./../services/userService";

@Component
export default class SignUpComponent extends Vue {
  loading: boolean = false;

  signupModel = {
    username: "",
    email: "",
    password: "",
    passwordRepeat: ""
  };

  rules = {
    username: [
      {
        required: true,
        message: "Please input your Username",
        trigger: "blur"
      },
      {
        min: 4,
        max: 18,
        message: "Username should be 4 to 18 characters long",
        trigger: "blur"
      }
    ],
    email: [
      { required: true, message: "Please input your Email", trigger: "blur" },
      { validator: this.validateEmail, trigger: "blur" }
    ],
    password: [
      {
        required: true,
        message: "Please input your password",
        trigger: "blur"
      },
      {
        min: 5,
        message: "Password should be at least 5 characters long",
        trigger: "blur"
      }
    ],
    passwordRepeat: [
      {
        required: true,
        message: "Please repeat your password",
        trigger: "blur"
      },
      { validator: this.validatePass, trigger: "blur" }
    ]
  };

  validatePass(rule: any, value: any, callback: any) {
    if (this.signupModel.password != this.signupModel.passwordRepeat) {
      callback(new Error("Passwords do not match"));
    } else {
      callback();
    }
  }

  validateEmail(rule: any, value: any, callback: any) {
    if (!new RegExp(/^\S+@\S+[\.][0-9a-z]+$/).test(value.toLowerCase())) {
      callback(new Error("Email is not valid!"));
    } else {
      callback();
    }
  }

  async signUp() {
    (this.$refs["form"] as any).validate(async (valid: any) => {
      if (valid) {
        try {
            this.loading = true;
          if (
            await UserService.signUp(
              this.signupModel.username,
              this.signupModel.password,
              this.signupModel.email
            )
          )
            this.$router.push("/");
        } catch (e) {
          this.$message.error(e.message);
        }

        this.loading = false;
      }
    });
  }
}
</script>

<style scoped>
.container {
  text-align: center;
  width: 100%;
  height: -webkit-fill-available;
  overflow: hidden;
}

#signup-bg {
  z-index: -1;
  position: absolute;
  width: 100%;
  opacity: 0.5;
  height: -webkit-fill-available;
  background: url("./../images/tic-tac-toe.png") repeat;
}

.signup-card {
  margin: 0% 37%;
}

.signup-button {
  margin-top: 15px;
  font-size: 20px;
}

.el-input {
  margin-bottom: 5px;
  font-size: 16px
}

.signup-title {
  margin-top: 3%;
}

.subtitle {
  font-size: 25px;
  text-align: left;
  margin: 0% 37% 0% 37%;
  display: inline-block;
  width: 100%;
  color: gray;
}

.signin {
  position: relative;
  float: right;
  margin-right: 37%;
  margin-top: 5px;
  font-size: 16px;
}
</style>