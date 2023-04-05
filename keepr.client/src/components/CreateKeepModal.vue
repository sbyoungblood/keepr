<template>
  <div class="modal" id="createKeepModal" tabindex="-1" role="dialog" aria-labelledby="modalTitleId" aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-xl" role="document">
      <div class="modal-content">
        <form @submit.prevent="CreateKeep()">
          <div class="modal-header">
            <h5 class="modal-title">Create Keep</h5>
            <button type="reset" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="form-floating mb-3">
              <input v-model="editable.name" type="text" class="form-control" id="floatingInput" placeholder="Cool Keep"
                required>
              <label for="floatingInput">Name</label>
            </div>
            <div class="form-floating mb-3">
              <input v-model="editable.img" type="url" class="form-control" id="floatingInput"
                placeholder="www.picture.come" required>
              <label for="floatingInput">Image</label>
            </div>
            <div class="form-floating">
              <textarea v-model="editable.description" type="text" class="form-control" id="floatingPassword"
                placeholder="Descripe this Keep" required></textarea>
              <label for="floatingPassword">Description</label>
            </div>
          </div>
          <div class="modal-footer">
            <button type="reset" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-primary" data-bs-dismiss="modal">Create Keep</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>


<script>
import { ref } from "vue";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService";

export default {
  setup() {
    const editable = ref({})
    return {
      editable,

      async CreateKeep() {
        try {
          const keepData = editable.value
          await keepsService.CreateKeep(keepData)
          editable.value = {}
        } catch (error) {
          logger.error(error)
          Pop.error(error, '[creating keep]')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped></style>