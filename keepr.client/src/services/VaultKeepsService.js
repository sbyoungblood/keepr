import { AppState } from "../AppState"
import { VaultKeep } from "../models/VaultKeep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {

  async KeepKeep(vaultId, keepId) {
    const res = await api.post('api/vaultkeeps', {vaultId, keepId})
    logger.log('[KEEPING THIS KEEP]', res.data)
    AppState.vaultKeeps.push(new VaultKeep(res.data))
  }

  // async UnkeepKeep(vkId) {
  //   const res =
  // }
}

export const vaultKeepsService = new VaultKeepsService()