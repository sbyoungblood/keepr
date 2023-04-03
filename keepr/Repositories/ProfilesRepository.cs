namespace keepr.Repositories;

public class ProfilesRepository
{
  private readonly IDbConnection _db;

  public ProfilesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Profile GetUserProfile(string id)
  {
    string sql = @"
    SELECT
    prof.*
    FROM accounts prof
    WHERE prof.id = @id;
    ";
    Profile userProfile = _db.Query<Profile>(sql, new { id }).FirstOrDefault();
    return userProfile;
  }

  internal List<Keep> GetUserKeeps(string id)
  {
    string sql = @"
    SELECT
    kp.*,
    prof.*
    FROM keeps kp
    JOIN accounts prof ON kp.creatorId = prof.id
    WHERE prof.id = @id;
    ";
    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.CreatorId = profile.Id;
      return keep;
    }, new { id }).ToList();
    return keeps;
  }

  internal List<Vault> GetUserVaults(string id)
  {
    string sql = @"
    SELECT
    vt.*,
    prof.*
    FROM vaults vt
    JOIN accounts prof ON vt.creatorId = prof.id
    WHERE prof.id = @id AND vt.isPrivate = false;
    ";
    List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.CreatorId = profile.Id;
      return vault;
    }, new { id }).ToList();
    return vaults;
  }
}
