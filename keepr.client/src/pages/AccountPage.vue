<template>
  <div class="containter-fluid">
    <div class="row">
      <div class="about text-center">
        <h1>Welcome {{ account?.name }}</h1>
        <img class="rounded" :src="account.picture" alt="" />
        <p>{{ account?.email }}</p>
      </div>

    </div>
    <div class="row justify-content-center pb-3">
      <div class="col-md-7 col-10">

        <div>
          <form @submit.prevent="handleSubmit">
            <!-- NOTE name -->
            <div class="mb-3">
              <label for="name" class="form-label">name</label>
              <input required type="text" v-model="editable.name" class="form-control" id="name"
                :placeholder="account.name" name="name">
            </div>
            <!-- NOTE image -->
            <div class="mb-3">
              <label for="picture" class="form-label">picture</label>
              <input required type="text" v-model="editable.picture" class="form-control" id="picture"
                :placeholder="account.picture" name="picture">
            </div>
            <!-- NOTE coverimage -->
            <div class="mb-3">
              <label for="coverImg" class="form-label">coverImg</label>
              <input type="text" v-model="editable.coverImg" class="form-control" id="coverImg"
                :placeholder="account.coverImg" name="coverImg">
            </div>

            <div>
              <button type="submit" class="btn btn-primary">
                {{ editable.profile }}
                Save changes
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, ref } from 'vue'
import { AppState } from '../AppState'
import { accountService } from "../services/AccountService.js"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
export default {
  setup() {
    const editable = ref({ ...AppState.account })
    return {
      editable,
      account: computed(() => AppState.account),

      async handleSubmit() {
        try {
          logger.log(editable.value, 'form data')
          await accountService.EditAccount(editable.value)
          editable.value = {}
        } catch (error) {
          Pop.error(error, 'Editing Profile')
        }

      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
