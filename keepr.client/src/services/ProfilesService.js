import { AppState } from "../AppState";
import { logger } from "../utils/Logger";

class ProfilesService {

  SetActiveProfile(profile) {
    AppState.activeProfile = {}
    AppState.activeProfile = profile
    logger.log('[active profile set]')
  }

}

export const profilesService = new ProfilesService();