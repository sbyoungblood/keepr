<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer>
    You're a keepr.
  </footer>

  <CreateKeepModal />
  <CreateVaultModal />
  <KeepDetailsModal />
  <VaultKeepModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import CreateKeepModal from "./components/CreateKeepModal.vue"
import KeepDetailsModal from "./components/KeepDetailsModal.vue"
import CreateVaultModal from "./components/CreateVaultModal.vue"
import VaultKeepModal from "./components/VaultKeepModal.vue"

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, CreateKeepModal, KeepDetailsModal, CreateVaultModal, VaultKeepModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
  margin-top: 25px;
  background-color: whitesmoke;
  color: black;
}
</style>
