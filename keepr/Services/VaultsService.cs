namespace keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _repo;

  public VaultsService(VaultsRepository repo)
  {
    _repo = repo;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault vault = _repo.CreateVault(vaultData);
    return vault;
  }

  internal string DeleteVault(int id, string userId)
  {
    Vault vault = this.GetVaultById(id, userId);
    if (vault.CreatorId != userId) throw new Exception("That is not your Vault!");
    _repo.DeleteVault(id);
    return $"{vault.Name} was deleted.";
  }

  internal List<Vault> GetMyVaults(string accountId)
  {
    List<Vault> vaults = _repo.GetMyVaults(accountId);
    return vaults;
  }

  internal Vault GetVaultById(int id, string userId)
  {
    Vault vault = _repo.GetVaultById(id, userId);
    if (vault == null) throw new Exception($"Sorry, couldn't find a vault at id {id}");
    if (vault.IsPrivate == true && vault.CreatorId != userId) throw new Exception($"Sorry, vault {id} is a private vault.");
    return vault;
  }

  internal Vault UpdateVault(Vault updateData, string userId)
  {
    Vault original = this.GetVaultById(updateData.Id, userId);
    if (original.CreatorId != userId) throw new Exception("Sorry, that aint your vault.");
    original.Name = updateData.Name == null ? original.Name : updateData.Name;
    original.IsPrivate = updateData.IsPrivate;
    _repo.UpdateVault(original);
    return original;
  }
}
