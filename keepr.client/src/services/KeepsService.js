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

}
export const keepsService = new KeepsService()