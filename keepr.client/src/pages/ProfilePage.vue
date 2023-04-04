<template>
  <div class="container">
    <div class="row">
      <div class="col-6">
        <img :src="profile?.coverImg" alt="">
        {{ profile.name }}
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState";

export default {
  setup() {
    return {
      profile: computed(() => AppState.activeProfile)
    }
  }
}
</script>


<style lang="scss" scoped></style>