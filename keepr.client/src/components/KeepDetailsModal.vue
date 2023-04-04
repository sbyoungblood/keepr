<template>
  <div class="modal fade" id="keepDetailsModal" tabindex="-1" role="dialog" aria-labelledby="modalTitleId"
    aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-xl" role="document">
      <div class="modal-content">
        <div class="">
          <div class="row">
            <div class="col-6 p-0">
              <img class="kd-img" :src="keep?.img" alt="">
            </div>
            <div class="col-6 d-flex justify-content-center">
              <div class="row">
                <div class="col-12 d-flex flex-column justify-content-between">
                  <div class="row justify-content-center pt-4">
                    <div class="col-3 d-flex justify-content-center">
                      <i class="mdi mdi-eye-outline kd-icon"> 5</i>
                    </div>
                    <div class="col-3 d-flex justify-content-center">
                      <i class="mdi mdi-bookmark-outline kd-icon">3</i>
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
                    <div class="col-9">
                      <div>vault</div>
                    </div>
                    <div v-if="keep != null" class="col-3" @click="SetActiveProfile(profile)">
                      <router-link :to="{ name: 'Profile', params: { profileId: keep?.creatorId } }">
                        <img class="kd-user-img rounded-circle" :src="keep?.creator?.picture" alt="">
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
import { computed } from "vue";
import { profilesService } from "../services/ProfilesService.js"
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { AppState } from "../AppState";

export default {
  props: { profile: { type: Object, required: true } },
  setup() {

    return {
      keep: computed(() => AppState.activeKeep),
      profile: computed(() => AppState.account),

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
  height: 100%;
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
</style>