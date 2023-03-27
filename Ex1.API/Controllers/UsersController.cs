using Ex1.API.Services.Users;
using Ex1.Model;
using Ex1.Model.Model;
using Ex1.Model.Request;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ex1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _log;
        private readonly IUsersService _usrSvc;
        private readonly IUserValidationService _usrValSvc;

        public UsersController(
            ILogger<UsersController> log,
            IUsersService usrSvc,
            IUserValidationService usrValSvc)
        {
            this._log = log;
            this._usrSvc = usrSvc;
            this._usrValSvc = usrValSvc;
        }

        /// <summary>
        /// Vrací seznam všech uživatelů
        /// </summary>
        /// <returns>pole všech uživatelů</returns>
        /// <response code="200">Pole všech uživatelů</response>
        /// <response code="204">Pole obsahuje 0 položek</response>
        /// <response code="500">Chyba</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int offset, int limit)
        {
            try
            {
                var res = _usrSvc.Get(offset, limit);

                if (res.Count == 0)
                    return NoContent();

                return Ok(res.Select(x => new UserModel(x)).ToArray());
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Podle id vrátí uživatele
        /// </summary>
        /// <returns>uživatele</returns>
        /// <param name="id">id uživatele</param>
        /// <response code="200">Vrací uživatele</response>
        /// <response code="404">Uživatel nenalezen</response>
        /// <response code="500">Chyba</response>
        [HttpGet("{id}", Name = nameof(Get))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get([Required] long id)
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
                _log.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Vytvoří uživatele
        /// </summary>
        /// <param name="user">uživatel, kterého chceme vytvořit</param>
        /// <returns>Cestu na které byl uživatel vytvořen</returns>
        /// <response code="200">Uživatel vytvořen</response>
        /// <response code="400">Invalidní hodnota parametru</response>
        /// <response code="409">Uživatel již existuje</response>
        /// <response code="500">Chyba</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] UserModel user)
        {
            try
            {
                if (!_usrValSvc.IsEmailValid(user))
                    return BadRequest(user.Email);

                if (!_usrValSvc.IsFullNameValid(user))
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
                    Gender = (int)user.Gender,
                    EducationMaxReached = (int)user.EducationMaxReached,
                    Interests = user.Interests
                };

                _usrSvc.Create(dbUser, out long createdId);
                user.Id = createdId;

                return CreatedAtRoute(nameof(Get), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Updatuje uživatele
        /// </summary>
        /// <param name="id">ID uživatele, kterého chceme updatovat</param>
        /// <param name="user">Hodnoty uživatele, které chceme updatovat (mimo ID)</param>
        /// <response code="200">Uživatel updatován</response>
        /// <response code="404">Uživatel nenalezen</response>
        /// <response code="500">Chyba</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put([Required] long id, [FromBody] UserModel user)
        {
            try
            {
                if (!_usrValSvc.IsEmailValid(user))
                    return BadRequest(user.Email);

                if (!_usrValSvc.IsFullNameValid(user))
                    return BadRequest(user.FullName);

                var obj = _usrSvc.Get(id);
                if (obj is null)
                    return NotFound();

                obj.FullName = user.FullName;
                obj.Email = user.Email;
                obj.BornDate = user.BornDate;
                obj.Gender = (int)user.Gender;
                obj.EducationMaxReached = (int)user.EducationMaxReached;
                obj.Interests = user.Interests;

                _usrSvc.Update(obj);

                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Smaže uživatele
        /// </summary>
        /// <param name="id">id uživatele, kterého chceme smazat</param>
        /// <response code="200">Uživatel smazán</response>
        /// <response code="404">Uživatel neexistuje</response>
        /// <response code="500">Chyba</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(long id)
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
                _log.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}
