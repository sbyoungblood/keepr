import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class ProfilesService {

  SetActiveProfile(profile) {
    AppState.activeProfile = {}
    AppState.activeProfile = profile
    logger.log('[active profile set]')
  }

  async GetActiveProfile(profileId) {
    const res = await api.get(`api/profiles/${profileId}`)
    logger.log('[GOT ACTIVE PROFILE]')
    AppState.activeProfile = res.data
  }

  async GetProfileVaults(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log('[GOT PROFILE VAULTS]', res.data)
    AppState.profileVaults = res.data
  }

  async GetProfileKeeps(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    logger.log('[GOT PROFILE KEEPS]', res.data)
    AppState.profileKeeps = res.data
  }

}

export const profilesService = new ProfilesService();