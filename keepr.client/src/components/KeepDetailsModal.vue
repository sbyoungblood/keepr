<template>
  <div class="modal fade" id="keepDetailsModal" tabindex="-1" role="dialog" aria-labelledby="modalTitleId"
    aria-hidden="true">
    <div class="modal-dialog  modal-dialog-centered modal-xl" role="document">
      <div class="modal-content">
        <div class="modal-body p-0">

          <div class="row">
            <div class="col-12 col-md-6 p-0">
              <img class="kd-img" :src="keep?.img" alt="">
            </div>

            <div class="col-12 col-md-6 p-0 d-flex justify-content-center">
              <div class="row">
                <div class="col-12 d-flex flex-column justify-content-between">
                  <div class="row justify-content-center pt-4">
                    <div class="col-3 d-flex justify-content-center">
                      <i class="mdi mdi-eye-outline kd-icon">{{ keep?.views }}</i>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                      <i class="mdi mdi-bookmark-outline kd-icon">{{ keep?.kept }}</i>
                    </div>
                  </div>
                  <div class="row justify-content-center">
                    <div class="col-8">
                      <h1 class="kp-name d-flex justify-content-center">
                        {{ keep?.name }}
                      </h1>
                      <p>{{ keep?.description }}</p>
                    </div>
                  </div>
                  <div class="row">
                    <div v-if="profile.id != null" class="col-9">
                      <form @submit.prevent="KeepKeep()">
                        <div class="input-group">
                          <select class="custom-select" id="inputGroupSelect04" v-model="editable.id">
                            <option selected>Choose...</option>
                            <option v-for="m in myVaults" :value="m.id">{{ m.name }}</option>
                          </select>
                          <div class="input-group-append">
                            <button class="btn btn-outline-secondary" type="submit">Save</button>
                          </div>
                        </div>
                      </form>
                    </div>
                    <div v-if="keep?.creatorId" class="col-3" @click="SetActiveProfile(profile)">
                      <router-link :to="{ name: 'Profile', params: { profileId: keep?.creatorId } }">
                        <img class="kd-user-img rounded-circle" title="Go to user's profile page"
                          :src="keep?.creator?.picture" alt="">
                        <span>{{ keep?.creator?.name }}</span>
                      </router-link>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted, ref } from "vue";
import { profilesService } from "../services/ProfilesService.js"
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { vaultsService } from "../services/VaultsService";
import { vaultKeepsService } from "../services/VaultKeepsService"

export default {
  props: { profile: { type: Object, required: true } },
  setup() {
    const editable = ref({})
    onMounted(() => {

      // GetKeepById()
    })
    // async function GetKeepById() {
    //   try {
    //     await keepsService.GetKeepById(keep.id)
    //   } catch (error) {
    //     logger.log(error)
    //     Pop.error(error, '[getting keep by id]')
    //   }
    // }

    return {
      editable,
      keep: computed(() => AppState.activeKeep),
      profile: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),

      async KeepKeep() {
        try {
          const vaultId = editable.value.id
          const keepId = this.keep.id
          await vaultKeepsService.KeepKeep(vaultId, keepId)
          this.keep.kept++;
          Pop.success('[Keep has been kept]')
        } catch (error) {
          logger.log(error)
          Pop.error(error, '[keeping this keep]')
        }
      },

      async SetActiveProfile(profile) {
        try {
          await profilesService.SetActiveProfile(profile);
          logger.log('[hello]')
        } catch (error) {
          logger.log(error);
          Pop.error(error, '[setting active profile]');
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.kd-img {
  width: 100%;
  height: auto;
  object-fit: cover;
  object-position: center;
}

.kd-user-img {
  max-width: 60px;
  max-height: 60px;
}

.kd-icon {
  font-size: x-large;
}

.kp-name {
  font-family: 'DM Serif Display', serif;
}

.details {
  max-height: 100%;
}
</style>