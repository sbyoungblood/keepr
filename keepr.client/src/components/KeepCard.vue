<template>
  <div class="component  rounded elevation-5 selectable" data-bs-toggle="modal" data-bs-target="#keepDetailsModal">
    <img @click="SetActiveKeep(keep)" class="kc-img img-fluid rounded" :src="keep.img" alt="">
    <h5 class="kc-name ps-2">{{ keep.name }}</h5>
    <img class="kc-user-img p-1 rounded-circle" :src="keep.creator.picture" alt="">
  </div>

  <KeepDetailsModal />
</template>







<script>
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService";
import KeepDetailsModal from "./KeepDetailsModal.vue";

export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {
      async SetActiveKeep(keep) {
        try {
          await keepsService.SetActiveKeep(keep);
        }
        catch (error) {
          logger.log(error);
          Pop.error(error, "[setting active recipe]");
        }
      }
    };
  },
  components: { KeepDetailsModal, KeepDetailsModal }
}
</script>


<style lang="scss" scoped>
.kc-user-img {
  position: relative;
}

.kc-name {
  position: absolute;
  bottom: 0;
  color: white;
}

.kc-user-img {
  position: absolute;
  bottom: 0;
  right: 10px;
  max-height: 50px;
  max-width: 50px;
}

.kd-image {
  object-position: center;
  object-fit: cover;
}

.modal {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
</style>