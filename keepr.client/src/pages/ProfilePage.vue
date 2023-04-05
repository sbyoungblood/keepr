<template>
  <div class="container">
    <div class="row justify-content-center mb-4">
      <div class="col-6">
        <img class="profile-coverImg rounded elevation-3" :src="profile?.coverImg" alt="">
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-4 d-flex justify-content-center">
        <img class="profile-picture rounded-circle elevation-5" :src="profile?.picture" alt="">
      </div>
    </div>
    <div class="row mt-5 mb-3 justify-content-center">
      <div class="col-4 d-flex justify-content-center">
        <h1>Vaults</h1>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-8">
        <div class="row">
          <div v-for="v in vaults" class="col-3 mb-3">
            <router-link :to="{ name: 'Vault', params: { vaultId: v?.id } }">
              <VaultCard :vault="v" />
            </router-link>
          </div>
        </div>
      </div>
    </div>
    <div class="row justify-content-center mt-5">
      <div class="col-4 justify-content-center d-flex">
        <h1>Keeps</h1>
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
import { computed, onMounted } from "vue";
import { AppState } from "../AppState";
import { useRoute } from "vue-router";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { profilesService } from "../services/ProfilesService";

export default {
  props: { keep: { type: Object, required: true } },
  setup() {

    const route = useRoute()
    onMounted(() => {
      GetActiveProfile()
      GetProfileVaults()
      GetProfileKeeps()
    })

    async function GetActiveProfile() {
      try {
        await profilesService.GetActiveProfile(route.params.profileId)
      } catch (error) {
        logger.log(error)
        Pop.error(error, '[getting active profile]')
      }
    }

    async function GetProfileVaults() {
      try {
        await profilesService.GetProfileVaults(route.params.profileId)
      } catch (error) {
        logger.log(error)
        Pop.error(error, '[getting profile vaults]')
      }
    }

    async function GetProfileKeeps() {
      try {
        await profilesService.GetProfileKeeps(route.params.profileId)
      } catch (error) {
        logger.error(error)
        Pop.error(error, '[getting profile keeps]')
      }
    }

    return {
      profile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.profileVaults),
      keeps: computed(() => AppState.profileKeeps)
    }
  }
}
</script>


<style lang="scss" scoped>
.profile-coverImg {
  margin-top: 5%;
  object-fit: cover;
  max-width: 100%;
  max-height: 100%;
  position: relative;
}

.profile-picture {
  max-width: 120px;
  max-height: 120px;
  border: 3px, red;
  position: absolute;
  top: 50%;
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

h1 {
  font-family: 'DM Serif Display', serif;
}
</style>