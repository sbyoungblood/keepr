namespace keepr.Repositories;

public class VaultsRepository
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    string sql = @"
    INSERT INTO vaults
    (creatorId, name, description, img, isPrivate)
    VALUES
    (@creatorId, @name, @description, @img, @isPrivate);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, vaultData);
    vaultData.Id = id;
    return vaultData;
  }

  internal int DeleteVault(int id)
  {
    string sql = @"
    DELETE FROM vaults
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, new { id });
    return rows;
  }

  internal Vault GetVaultById(int id)
  {
    string sql = @"
    SELECT
    vt.*,
    creator.*
    FROM vaults vt
    JOIN accounts creator ON vt.creatorId = creator.id
    WHERE vt.id = @id;
    ";
    Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { id }).FirstOrDefault();
    return vault;
  }

  internal int UpdateVault(Vault updateData)
  {
    string sql = @"
    UPDATE vaults
    SET
    name = @name,
    isPrivate = @isPrivate
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, updateData);
    return rows;
  }
}
