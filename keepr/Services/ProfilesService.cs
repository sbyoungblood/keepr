namespace keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _repo;

  public ProfilesService(ProfilesRepository repo)
  {
    _repo = repo;
  }

  internal List<Keep> GetUserKeeps(string id)
  {
    List<Keep> keeps = _repo.GetUserKeeps(id);
    return keeps;
  }

  internal Profile GetUserProfile(string id)
  {
    Profile userProfile = _repo.GetUserProfile(id);
    return userProfile;
  }
}
