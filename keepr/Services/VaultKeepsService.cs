namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repo;
  private readonly VaultsService _vaultsService;

  public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
  {
    _repo = repo;
    _vaultsService = vaultsService;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
  {
    Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId);
    if (vault.CreatorId != userId) throw new Exception("Sorry, you can't put that there.");
    VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);
    return vaultKeep;
  }

  internal string DeleteVaultKeep(int id, string userId)
  {
    VaultKeep vaultKeep = this.GetVaultKeepById(id);
    if (vaultKeep.CreatorId != userId) throw new Exception("That is not your vaultKeep!");
    _repo.DeleteVaultKeep(id);
    return $"The vaultKeep at id {id} was deleted.";
  }

  internal VaultKeep GetVaultKeepById(int id)
  {
    VaultKeep vaultKeep = _repo.GetVaultKeepById(id);
    if (vaultKeep == null) throw new Exception($"Sorry, couldn't find a vaultKeep at id {id}");
    return vaultKeep;
  }
}
