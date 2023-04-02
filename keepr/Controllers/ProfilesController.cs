namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _profilesService;
  private readonly Auth0Provider _auth;

  public ProfilesController(ProfilesService profilesService, Auth0Provider auth)
  {
    _profilesService = profilesService;
    _auth = auth;
  }

  [HttpGet("{id}")]
  [Authorize]
  public async Task<ActionResult<Profile>> GetUserProfile(string id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Profile userProfile = _profilesService.GetUserProfile(id);
      return Ok(userProfile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}/keeps")]
  public ActionResult<List<Keep>> GetUserKeeps(string id)
  {
    try
    {
      List<Keep> keeps = _profilesService.GetUserKeeps(id);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
