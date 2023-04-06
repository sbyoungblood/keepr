import { AppState } from "../AppState"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {

  SetActiveVault(vault) {
    AppState.activeVault = {}
    AppState.activeVault = vault
  }

  async GetActiveVault(vaultId) {
    AppState.activeVault = {}
    const res = await api.get(`api/vaults/${vaultId}`)
    logger.log('[GET ACTIVE VAULT]')
    AppState.activeVault = res.data
  }

  async GetKeepsInVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log('[GOT KEEPS IN VAULT]', res.data)
    AppState.vaultKeeps = res.data
  }

  async CreateVault(vaultData) {
    const res = await api.post('api/vaults', vaultData)
    AppState.myVaults.push(new Vault(res.data))
  }

  // FIXME vaultkeeps??

  async DeleteVault(vaultId) {
    const res = await api.delete('api/vaults/' + vaultId)
    const index = AppState.profileVaults.findIndex(v => v.id == vaultId)
    AppState.profileVaults.splice(index, 1)
  }

  async GetMyVaults() {
    const res = await api.get('account/vaults')
    logger.log('[GOT MY VAULTS]')
    AppState.myVaults = res.data
  }

}

export const vaultsService = new VaultsService()