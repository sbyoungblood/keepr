<template>
  <div class="modal" id="createVaultModal" tabindex="-1" role="dialog" aria-labelledby="modalTitleId" aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-xl" role="document">
      <div class="modal-content">
        <form @submit.prevent="CreateVault()">
          <div class="modal-header">
            <h5 class="modal-title">Create Vault</h5>
            <button type="reset" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="form-floating mb-3">
              <input v-model="editable.name" type="text" class="form-control" id="floatingInput" placeholder="Cool Vault"
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
                placeholder="Describe this Vault" required></textarea>
              <label for="floatingPassword">Description</label>
            </div>
          </div>
          <div class="modal-footer">
            <button type="reset" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-primary" data-bs-dismiss="modal">Create Vault</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>


<script>
import { ref } from "vue"
import { vaultsService } from "../services/VaultsService"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"

export default {
  setup() {
    const editable = ref({})
    return {
      editable,

      async CreateVault() {
        try {
          const vaultData = editable.value
          await vaultsService.CreateVault(vaultData)
          editable.value = {}
        } catch (error) {
          logger.error(error)
          Pop.error(error, '[creating vault]')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped></style>