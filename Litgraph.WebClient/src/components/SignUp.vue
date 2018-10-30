<template>
    <div class="container">
        <div id="signup-bg"></div>
        <h1 class="litgraph-title signup-title">Litgraph</h1>
        <el-card class="signup-card">
            <el-form :model="signupModel" :rules="rules" ref="form">
                <el-form-item prop="username">
                    <el-input v-model="signupModel.username" placeholder="Username" type="text" spellcheck="false"></el-input>
                </el-form-item>
                <el-form-item prop="email">
                    <el-input v-model="signupModel.email" placeholder="Email" type="text" spellcheck="false"></el-input>
                </el-form-item>
                <el-form-item prop="password">
                    <el-input v-model="signupModel.password" placeholder="Password" type="password"></el-input>
                </el-form-item>
                <el-form-item prop="passwordRepeat">
                    <el-input v-model="signupModel.passwordRepeat" placeholder="Repeat password" type="password"></el-input>
                </el-form-item>
            </el-form>

            <el-button class="signup-button" type="primary" @click="signUp">Sign up</el-button>
        </el-card>
    </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

@Component
export default class SignUpComponent extends Vue {
  signupModel = {
    username: "",
    email: "",
    password: "",
    passwordRepeat: ""
  };

  rules = {
      username: [
          { required: true, message: 'Please input your Username', trigger: 'blur' },
          { min: 4, max: 14, message: 'Username should be 4 to 14 characters long', trigger: 'blur' }
      ],
      email: [
          { required: true, message: 'Please input your Email', trigger: 'blur' },
          { validator: this.validateEmail, trigger: 'blur' }
      ],
      password: [
          { required: true, message: 'Please input your password', trigger: 'blur' },
          { min: 5, message: 'Password should be at least 5 characters long', trigger: 'blur' }         
      ],
      passwordRepeat: [
          { required: true, message: 'Please repeat your password', trigger: 'blur' },
          { validator: this.validatePass, trigger: 'blur' }
      ]
  }

  validatePass(rule: any, value: any, callback: any) {
      if (this.signupModel.password != this.signupModel.passwordRepeat) {
          callback(new Error('Passwords do not match'));
      } else {
          callback();
      }
  }

  validateEmail(rule: any, value: any, callback: any) {
      if (!new RegExp(/^\S+@\S+[\.][0-9a-z]+$/).test(value.toLowerCase())) {
          callback(new Error('Email is not valid!'));
      } else {
          callback();
      }
  }

  async signUp() {
      (this.$refs['form'] as any).validate((valid: any) => {
          if (valid) {
              this.$message('Form is valid!');
          } else {
              this.$message('Form is invalid!');
          }
      })
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
  margin: 5% 37%;
}

.signup-button {
  margin-top: 30px;
  font-size: 20px;
}

.el-input {
    margin-bottom: 5px
}

.signup-title {
  margin-top: 5%;
}
</style>

