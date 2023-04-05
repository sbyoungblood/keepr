<template>
  <div class="rounded elevation-5 selectable" data-bs-toggle="modal" data-bs-target="#keepDetailsModal">
    <img @click="SetActiveKeep(keep)" class="kc-img img-fluid rounded" :src="keep?.img" alt="">
    <div class="container">
      <div class="row kc-top-row">
        <i v-if="keep.creatorId == account.id" @click="DeleteKeep(keep)"
          class="col-12 mdi mdi-close-circle kc-delete d-flex justify-content-end pt-1" title="delete keep"></i>
      </div>
      <div class="row kc-btm-row pb-2">
        <div class="col-8 d-flex flex-column justify-content-center">
          <div class="kc-name">{{ keep?.name }}</div>
        </div>
        <div class="col-4 d-flex justify-content-end">
          <img class="kc-user-img rounded-circle" :src="keep?.creator?.picture" alt="">
        </div>
      </div>
    </div>
  </div>
</template>







<script>
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService";
import KeepDetailsModal from "./KeepDetailsModal.vue";
import { computed, popScopeId } from "vue";
import { AppState } from "../AppState";

export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {

      account: computed(() => AppState.account),

      async SetActiveKeep(keep) {
        try {
          await keepsService.SetActiveKeep(keep);
        }
        catch (error) {
          logger.log(error);
          Pop.error(error, "[setting active keep]");
        }
      },

      async DeleteKeep(keep) {
        try {
          if (await Pop.confirm('Are you sure you want to delete this Keep?')) {
            await keepsService.DeleteKeep(keep)
            Pop.success(`You deleted the keep "${keep.name}"`)
          }
        } catch (error) {
          logger.log(error)
          Pop.error(error, "[deleting keep]")
        }
      }

    };
  },
  components: { KeepDetailsModal, KeepDetailsModal }
}
</script>


<style lang="scss" scoped>
.kc-img {
  position: relative;
}

.kc-top-row {
  position: absolute;
  top: 0;
  right: 0;
  left: .75em;
  max-width: 100%;
  color: rgb(209, 209, 209);
}

.kc-btm-row {
  position: absolute;
  bottom: 0;
  right: 0;
  left: .50em;
  max-width: 100%;
  color: white;
  font-family: 'DM Serif Display', serif;
  font-size: x-large;
}

.kc-user-img {
  // position: relative;
  // bottom: 0;
  // right: 0;
  max-height: 35px;
  max-width: 35px;
}
</style>