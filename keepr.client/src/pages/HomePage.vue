<template>
  <div class="container-fluid">
    <section class="bricks">
      <div v-for="k in keeps">
        <KeepCard :keep="k" />
      </div>
    </section>
  </div>
</template>

<script>
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService.js"
import { computed, onMounted } from "vue";
import { AppState } from '../AppState.js';

export default {
  setup() {

    onMounted(() => {
      GetKeeps()
    })

    async function GetKeeps() {
      try {
        await keepsService.GetKeeps()
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
// .home {
//   display: grid;
//   height: 80vh;
//   place-content: center;
//   text-align: center;
//   user-select: none;

//   .home-card {
//     width: 50vw;

//     >img {
//       height: 200px;
//       max-width: 200px;
//       width: 100%;
//       object-fit: contain;
//       object-position: center;
//     }
//   }
// }

$gap: 1.5em;

.bricks {
  columns: 250px;
  column-gap: $gap;

  &>div {
    margin-top: $gap;
    display: inline-block;
  }
}
</style>
