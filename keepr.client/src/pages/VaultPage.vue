<template>
  <div class="container">
    <div class="row justify-content-center mb-4">
      <div class="col-6 d-flex justify-content-center">
        <img class="vault-coverImg rounded elevation-3" :src="vault?.img" alt="">
      </div>
    </div>
    <div class="row justify-content-center mt-2 mb-5">
      <div class="col-8 text-center">
        <h1>{{ vault.name }}</h1>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-8">
        <section class="bricks">
          <div v-for="k in keeps">
            <KeepCard :keep="k" />
          </div>
        </section>
      </div>
    </div>
  </div>
</template>


<script>
import { useRoute } from "vue-router";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService";
import { computed, onMounted } from "vue";
import { AppState } from "../AppState";

export default {
  setup() {

    const route = useRoute()
    onMounted(() => {
      GetActiveVault()
      GetKeepsInVault()
    })

    async function GetActiveVault() {
      try {
        await vaultsService.GetActiveVault(route.params.vaultId)
      } catch (error) {
        logger.log(error)
        Pop.error(error, '[getting active vault]')
      }
    }

    async function GetKeepsInVault() {
      try {
        await vaultsService.GetKeepsInVault(route.params.vaultId)
      } catch (error) {
        logger.log(error)
        Pop.error(error, '[getting keeps in vault]')
      }
    }

    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.vaultKeeps)
    }
  }
}
</script>


<style lang="scss" scoped>
.vault-coverImg {
  margin-top: 5%;
  object-fit: cover;
  max-width: 100%;
  max-height: 100%;
  position: relative;
}

h1 {
  font-family: 'DM Serif Display', serif;
}

$gap: 1.5em;

.bricks {
  columns: 250px;
  column-gap: $gap;

  &>div {
    margin-top: $gap;
    display: inline-block;
  }
}
</style>