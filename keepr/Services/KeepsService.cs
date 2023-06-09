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
    Keep keep = this.GetKeepById(id, userId);
    if (keep.CreatorId != userId) throw new Exception("That is not your Keep!");
    _repo.DeleteKeep(id);
    return $"{keep.Name} was deleted.";
  }

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repo.GetAllKeeps();
    return keeps;
  }

  internal Keep GetKeepById(int id, string userId, bool increaseViews = true)
  {
    Keep keep = _repo.GetKeepById(id);
    if (keep == null) throw new Exception($"There is no keep at id {id}");
    if (keep.CreatorId != userId && increaseViews)
      keep.Views++;
    _repo.UpdateData(keep);
    // TODO save this change using edit function
    return keep;
  }

  internal List<VaultedKeep> GetKeepsByVault(int vaultId, string userId)
  {
    Vault vault = _vaultsService.GetVaultById(vaultId, userId);
    if (vault.IsPrivate == true && vault.CreatorId != userId) throw new Exception($"Sorry, that vault is private.");
    List<VaultedKeep> vaultedKeeps = _repo.GetKeepsByVault(vaultId);
    return vaultedKeeps;
  }

  internal void increaseKept(int keepId)
  {
    Keep keep = _repo.GetKeepById(keepId);
    keep.Kept++;
    _repo.UpdateData(keep);
  }

  internal Keep UpdateKeep(Keep updateData, string userId)
  {
    Keep original = this.GetKeepById(updateData.Id, userId);
    if (original.CreatorId != userId) throw new Exception("Sorry, that aint your keep.");
    original.Name = updateData.Name == null ? original.Name : updateData.Name;
    original.Description = updateData.Description == null ? original.Description : updateData.Description;
    original.Views = updateData.Views == null ? original.Views : updateData.Views;
    original.Kept = updateData.Kept == null ? original.Kept : updateData.Kept;
    _repo.UpdateData(original);
    return original;
  }
}
