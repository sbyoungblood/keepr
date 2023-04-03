namespace keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _repo;
  private readonly VaultsService _vaultsService;

  public KeepsService(KeepsRepository repo, VaultsService vaultsService)
  {
    _repo = repo;
    _vaultsService = vaultsService;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    Keep keep = _repo.CreateKeep(keepData);
    return keep;
  }

  internal string DeleteKeep(int id, string userId)
  {
    Keep keep = this.GetKeepById(id);
    if (keep.CreatorId != userId) throw new Exception("That is not your Keep!");
    _repo.DeleteKeep(id);
    return $"{keep.Name} was deleted.";
  }

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repo.GetAllKeeps();
    return keeps;
  }

  internal Keep GetKeepById(int id)
  {
    Keep keep = _repo.GetKeepById(id);
    if (keep == null) throw new Exception($"There is no keep at id {id}");
    return keep;
  }

  internal List<VaultedKeep> GetKeepsByVault(int vaultId)
  {
    Vault vault = _vaultsService.GetVaultById(vaultId);
    if (vault.IsPrivate == true) throw new Exception($"Sorry, that vault is private.");
    List<VaultedKeep> vaultedKeeps = _repo.GetKeepsByVault(vaultId);
    return vaultedKeeps;
  }

  internal Keep UpdateKeep(Keep updateData, string userId)
  {
    Keep original = this.GetKeepById(updateData.Id);
    if (original.CreatorId != userId) throw new Exception("Sorry, that aint your keep.");
    original.Name = updateData.Name == null ? original.Name : updateData.Name;
    original.Description = updateData.Description == null ? original.Description : updateData.Description;
    _repo.UpdateData(original);
    return original;
  }
}
