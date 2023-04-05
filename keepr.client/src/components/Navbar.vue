<template>
  <nav class="navbar elevation-5 px-3">
    <!-- <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button> -->
    <div class="" id="navbarText">
      <span>
        <div class="navbar-nav d-flex">
          <h6 class="selectable" data-bs-toggle="modal" data-bs-target="#createKeepModal">+Keep</h6>
        </div>
        <div class="navbar-nav d-flex">
          <h6>+Vault</h6>
        </div>
      </span>
      <!-- LOGIN COMPONENT HERE -->
    </div>
    <div>
      <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
        <div class="d-flex flex-column align-items-center">
          <h1>keepr</h1>
        </div>
      </router-link>
    </div>
    <Login />
  </nav>
</template>

<script>
import Login from './Login.vue'
export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 75px;
  }
}

nav {
  background-color: whitesmoke;
}

h1 {
  font-family: 'DM Serif Display', serif;
  color: black;
}
</style>
