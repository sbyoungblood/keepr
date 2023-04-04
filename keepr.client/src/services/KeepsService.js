import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {


  async GetKeeps() {
    const res = await api.get('api/keeps')
    logger.log('[GOT KEEPS]', res.data)
    AppState.keeps = res.data
  }

  SetActiveKeep(keep) {
    AppState.activeKeep = {}
    AppState.activeKeep = keep
  }

  async DeleteKeep(keep) {
    const res = await api.delete('api/keeps/' + keep.id)
    const index = AppState.keeps.findIndex(k => k.id == keep.id)
    AppState.keeps.splice(index, 1)
  }

}
export const keepsService = new KeepsService()