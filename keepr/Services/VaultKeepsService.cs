namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repo;
  private readonly VaultsService _vaultsService;

  private readonly KeepsService _keepsService;

  public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService, KeepsService keepsService)
  {
    _repo = repo;
    _vaultsService = vaultsService;
    _keepsService = keepsService;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
  {
    Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, userId);
    if (vault.CreatorId != userId) throw new Exception("Sorry, you can't put that there.");
    VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);
    _keepsService.increaseKept(vaultKeepData.KeepId);
    // TODO now go get the keep, similar to how you got the vault,
    // increase kept count
    // save it.
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
