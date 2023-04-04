<template>
  <div class="modal fade" id="keepDetailsModal" tabindex="-1" role="dialog" aria-labelledby="modalTitleId"
    aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-lg" role="document">
      <div class="modal-content">
        <div class="modal-body">
          <div class="container-fluid">
            <div class="row">
              <div class="col-6">
                <img :src="this.keep?.img" alt="">
              </div>
              <div class="col-6 d-flex justify-content-center">
                <div class="row">
                  <div class="col-12 d-flex justify-content-center">
                    {{ keep?.name }}
                  </div>
                  <div class="col-12">
                    {{ keep?.description }}
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
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { AppState } from "../AppState";

export default {

  setup() {

    return {
      keep: computed(() => AppState.activeKeep)
    }
  }
}
</script>


<style lang="scss" scoped></style>