<template>
    <div class="error-container-outer">
        <div class="erro-container-middle">
            <div class="error-container-inner">
                <h1 id="header">:(</h1>
                <h3 id="message">Some error seemed to occure<br/>Try reloading the page</h3>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import Vue from 'vue'
export default Vue.extend({ name: "Error" })
</script>

<style scoped>
.error-container-outer {
  background: url("./../images/tic-tac-toe.png") repeat;
  width: 100%;
  height: -webkit-fill-available;
  display: table;
  position: absolute;
  top: 0;
  left: 0;
}
.error-container-middle {
  display: table-cell;
  vertical-align: middle;
}
.error-container-inner {
  margin-left: auto;
  margin-right: auto;
  width: fit-content;
  text-align: center;
}

#header {
    font-size: 150px;
    margin-top: 150px
}

#message {
    font-size: 60px;
}
</style>
