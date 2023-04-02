namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vaultKeepsService;
  private readonly Auth0Provider _auth;

  public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth)
  {
    _vaultKeepsService = vaultKeepsService;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
  {
    Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
    vaultKeepData.CreatorId = userInfo.Id;
    VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData);
    return Ok(vaultKeep);
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteVaultKeep(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _vaultKeepsService.DeleteVaultKeep(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public ActionResult<Vault> GetVaultKeepById(int id)
  {
    try
    {
      VaultKeep vaultKeep = _vaultKeepsService.GetVaultKeepById(id);
      return Ok(vaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
