namespace keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _repo;

  public KeepsService(KeepsRepository repo)
  {
    _repo = repo;
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

  internal Keep UpdateKeep(Keep updateData)
  {
    Keep original = this.GetKeepById(updateData.Id);
    original.Name = updateData.Name == null ? original.Name : updateData.Name;
    original.Description = updateData.Description == null ? original.Description : updateData.Description;
    _repo.UpdateData(original);
    return original;
  }
}
