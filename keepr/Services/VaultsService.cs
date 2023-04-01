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
    Vault vault = this.GetVaultById(id);
    if (vault.CreatorId != userId) throw new Exception("That is not your Vault!");
    _repo.DeleteVault(id);
    return $"{vault.Name} was deleted.";
  }

  internal Vault GetVaultById(int id)
  {
    Vault vault = _repo.GetVaultById(id);
    if (vault == null) throw new Exception($"Sorry, couldn't find a vault at id {id}");
    return vault;
  }

  internal Vault UpdateVault(Vault updateData)
  {
    Vault original = this.GetVaultById(updateData.Id);
    original.Name = updateData.Name == null ? original.Name : updateData.Name;
    original.IsPrivate = updateData.IsPrivate;
    _repo.UpdateVault(original);
    return original;
  }
}
