namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth;
  private readonly KeepsService _keepsService;

  public VaultsController(VaultsService vaultsService, Auth0Provider auth, KeepsService keepsService)
  {
    _vaultsService = vaultsService;
    _auth = auth;
    _keepsService = keepsService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      Vault vault = _vaultsService.CreateVault(vaultData);
      vaultData.Creator = userInfo;
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public ActionResult<Vault> GetVaultById(int id)
  {
    try
    {
      Vault vault = _vaultsService.GetVaultById(id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault updateData, int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      updateData.CreatorId = userInfo.Id;
      updateData.Id = id;
      Vault vault = _vaultsService.UpdateVault(updateData, userInfo.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteVault(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _vaultsService.DeleteVault(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}/keeps")]
  public async Task<ActionResult<List<VaultedKeep>>> GetKeepsByVault(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      List<VaultedKeep> vaultedKeeps = _keepsService.GetKeepsByVault(id);
      return Ok(vaultedKeeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
