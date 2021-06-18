<template>
  <q-layout view="lHh Lpr lFf">
    <login-dialog :visible="showLoginDialog" />
<v-menu offset-y full-width>
    <q-header elevated>
      <q-toolbar style="background-color:#FFFFFF; height:20px;display:flex; flex-direction:row; flex grow:1; align-content:center  "
        ><div class="buttonDetails" @click="handleClick">
         <img src="../../public/quhinja.png">
        </div>
        <div class="q-px-xl">
        </div>
        <q-tabs indicator-color="brown">
          <q-route-tab active name="Menu" label="Menu" to="/menu" />
          <q-route-tab name="Jela" label="Jela" to="/dishes" />
          <q-route-tab name="Zaposleni" label="Zaposleni" to="/employees" />
        </q-tabs>
        <div v-if="isAuthenticated" class="fixed-top-right on-left q-mt-sm">
          <user-avatar-and-menu />
        </div>
        <div v-else>
          <q-btn @click="handleLoginClick" class="fixed-top-right on-left q-mt-sm" dense>
            <q-icon name="login" color="white"> </q-icon>
          </q-btn>
        </div>
      </q-toolbar>
    </q-header>
</v-menu>
    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import UserAvatarAndMenu from "../components/UserAvatarAndMenu";

import LoginDialog from "../components/LoginDialog";

export default {
  name: "MainLayout",
  components: {
    LoginDialog,
    UserAvatarAndMenu,
  },
  data() {
    return {};
  },
  methods: {
    handleClick() {
      this.$router.push(`/`);
    },
    handleLoginClick() {
      this.$store.commit("auth/showLoginDialog");
    },
  },
  computed: {
    isAuthenticated() {
      return this.$store.getters["auth/isAuthenticated"];
    },
    showLoginDialog() {
      return this.$store.getters["auth/toShowLogin"];
      // return this.$store.state.auth.showLoginDialog
    },
  },
};
</script>

<style scoped>
* {
  color: #000;
}
.buttonDetails:hover {
  cursor: pointer;
  color: blue;
}
</style>
