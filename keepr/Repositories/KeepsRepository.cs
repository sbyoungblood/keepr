namespace keepr.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    string sql = @"
    INSERT INTO keeps
    (creatorId, name, description, img)
    VALUES
    (@creatorId, @name, @description, @img);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, keepData);
    keepData.Id = id;
    return keepData;
  }

  internal int DeleteKeep(int id)
  {
    string sql = @"
    DELETE FROM keeps
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, new { id });
    return rows;
  }

  internal List<Keep> GetAllKeeps()
  {
    string sql = @"
    SELECT
    kp.*,
    creator.*
    FROM keeps kp
    JOIN accounts creator ON kp.creatorId = creator.id;
    ";
    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }).ToList();
    return keeps;
  }

  internal Keep GetKeepById(int id)
  {
    string sql = @"
    SELECT
    kp.*,
    creator.*
    FROM keeps kp
    JOIN accounts creator ON kp.creatorId = creator.id
    WHERE kp.id = @id;
    ";
    Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { id }).FirstOrDefault();
    return keep;
  }

  internal int UpdateData(Keep updateData)
  {
    string sql = @"
    UPDATE keeps
    SET
    name = @name,
    description = @description
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, updateData);
    return rows;
  }
}
