<template>
  <div class="d-flex justify-content-center">
    <div class="position-relative selectable">
      <img @click="SetActiveVault(vault)" class="rounded vault-img" :src="vault?.img" alt="">
      <div class="vault-name">
        {{ vault?.name }}
      </div>
      <div class="col-2 vault-lock">
        <i class="mdi mdi-lock"></i>
      </div>
    </div>
  </div>
</template>


<script>
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService";

export default {
  props: { vault: { type: Object, required: true } },

  setup() {
    return {

      async SetActiveVault(vault) {
        try {
          await vaultsService.SetActiveVault(vault)
        } catch (error) {
          logger.log(error)
          Pop.error(error, '[setting active vault]')
        }
      }

    }
  }
}
</script>


<style lang="scss" scoped>
.d-flex {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.vault-img {
  position: relative;
  max-width: 150px;
}

.vault-name {
  position: absolute;
  bottom: 0;
  left: 0;
  font-family: 'DM Serif Display', serif;
  color: white;
  font-size: larger;
  padding: 8px;
}

.vault-lock {
  position: absolute;
  top: 0;
  right: 0px;
}
</style>