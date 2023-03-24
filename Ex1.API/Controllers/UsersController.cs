using Ex1.API.Services.Users;
using Ex1.Model;
using Ex1.Model.Model;
using Ex1.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace Ex1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> log;
        private readonly IUsersService _usrSvc;

        public UsersController(ILogger<UsersController> log, IUsersService usrSvc)
        {
            this.log = log;
            this._usrSvc = usrSvc;
        }

        /// <summary>
        /// Vrací seznam všech uživatelů
        /// </summary>
        /// <response code="200">Pole všech uživatelů</response>
        /// <response code="204">Pole obsahuje 0 položek</response>
        /// <response code="500">Chyba</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                var res = _usrSvc.Get();

                if (res.Count == 0)
                    return NoContent();

                return Ok(res.Select(x => new UserModel(x)).ToArray());
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                var res = _usrSvc.Get(id);

                if (res is null)
                    return NotFound();

                return Ok(new UserModel(res));
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //prověřit
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] UserModel user)
        {
            try
            {
                if (!user.IsEmailValid())
                    return BadRequest(user.Email);

                if (!user.IsFullNameValid())
                    return BadRequest(user.FullName);

                //email nemůže již existovat
                var obj = _usrSvc.GetByEmailAdress(user.Email);
                if (obj is not null)
                    return Conflict(nameof(user.Email));

                var dbUser = new User()
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    BornDate = user.BornDate,
                    Gender = user.Gender,
                    EducationMaxReached = user.EducationMaxReached,
                    Interests = user.Interests
                };

                _usrSvc.Create(dbUser, out long createdId);
                user.Id = createdId;

                return CreatedAtRoute(nameof(Get), new { user.Id }, user);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(int id, [FromBody] UserModel user)
        {
            try
            {
                if (!user.IsEmailValid())
                    return BadRequest(user.Email);

                if (!user.IsFullNameValid())
                    return BadRequest(user.FullName);

                var obj = _usrSvc.Get(id);
                if (obj is null)
                    return NotFound();

                obj.FullName = user.FullName;
                obj.Email = user.Email;
                obj.BornDate = user.BornDate;
                obj.Gender = user.Gender;
                obj.EducationMaxReached = user.EducationMaxReached;
                obj.Interests = user.Interests;

                _usrSvc.Update(obj);

                return Ok();
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                var obj = _usrSvc.Get(id);
                if (obj is null)
                    return NotFound();

                _usrSvc.Delete(obj);

                return Ok();
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}
