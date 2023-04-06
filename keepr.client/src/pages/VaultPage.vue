<template>
  <div class="container">
    <div class="row justify-content-between my-4">
      <div class="col-2">
        <button v-if="vault.creatorId == account.id && vault.isPrivate == false" @click="SwitchPrivate()"
          class="btn btn-outline-dark">Lock
          Vault</button>
        <button v-if="vault.creatorId == account.id && vault.isPrivate == true" @click="SwitchPrivate()"
          class="btn btn-outline-dark">Unlock
          Vault</button>
      </div>
      <div class="col-6 d-flex justify-content-center">
        <img class="vault-coverImg rounded elevation-3" :src="vault?.img" alt="">
      </div>
      <div class="col-2">
        <button v-if="vault.creatorId == account.id" @click="DeleteVault()" class="btn btn-outline-danger">Delete
          Vault</button>
      </div>
    </div>
    <div class="row justify-content-center mt-2 mb-5">
      <div class="col-8 text-center">
        <h1>{{ vault.name }}</h1>
        <h5>Keeps {{ keeps.length }}</h5>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-8">
        <section class="bricks">
          <div v-for="k in keeps">
            <VaultKeepCard :keep="k" />
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
import VaultKeepCard from "../components/VaultKeepCard.vue";
import { router } from "../router";

export default {
  setup() {
    const route = useRoute();
    onMounted(() => {
      GetActiveVault();
      GetKeepsInVault();
    });
    async function GetActiveVault() {
      try {
        await vaultsService.GetActiveVault(route.params.vaultId);
      }
      catch (error) {
        logger.log(error);
        Pop.error(error, "[getting active vault]");
        router.push({ path: '/' })
      }
    }
    async function GetKeepsInVault() {
      try {
        await vaultsService.GetKeepsInVault(route.params.vaultId);
      }
      catch (error) {
        logger.log(error);
        Pop.error(error, "[getting keeps in vault]");
      }
    }
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.vaultKeeps),
      account: computed(() => AppState.account),


      async DeleteVault() {
        try {
          if (await Pop.confirm("Are you sure you want to delete this Vault?")) {
            await vaultsService.DeleteVault(route.params.vaultId);
            Pop.success(`You deleted the vault.`);
          }
        }
        catch (error) {
          logger.log(error);
          Pop.error(error, "[deleting vault]");
        }
      },

      async SwitchPrivate() {
        try {
          AppState.activeVault.isPrivate = !AppState.activeVault.isPrivate
          await vaultsService.SwitchPrivate(AppState.activeVault);
        } catch (error) {
          logger.log(error);
          Pop.error(error, '[switching the lock]')
        }
      }
    };
  },
  components: { VaultKeepCard }
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