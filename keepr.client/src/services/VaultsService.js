import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {

  SetActiveVault(vault) {
    AppState.activeVault = {}
    AppState.activeVault = vault
  }

  async GetActiveVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    logger.log('[GET ACTIVE VAULT]')
    AppState.activeVault = res.data
  }

  async GetKeepsInVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log('[GOT KEEPS IN VAULT]', res.data)
    AppState.vaultKeeps = res.data
  }

}

export const vaultsService = new VaultsService()