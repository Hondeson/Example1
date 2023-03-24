using Ex1.Model;
using Ex1.Model.Model;
using Ex1.Model.Request;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Ex1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> log;
        private readonly Ex1Context db;

        public UsersController(ILogger<UsersController> log, Ex1Context db)
        {
            this.log = log;
            this.db = db;
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
                var res = db.Users.ToList();

                if (res.Count == 0)
                    return NoContent();

                return Ok(res);
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
                var res = db.Users.FirstOrDefault(x => x.Id == id);

                if (res is null)
                    return NotFound();

                return Ok(res);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] UserModel user)
        {
            try
            {
                //email nemůže již existovat
                var obj = db.Users.FirstOrDefault(x => x.Email == user.Email);
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

                db.Users.Add(dbUser);

                return CreatedAtRoute(nameof(Get), new { dbUser.Id }, user);
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
                var obj = db.Users.FirstOrDefault(x => x.Id == id);
                if (obj is null)
                    return NotFound();

                obj.FullName = user.FullName;
                obj.Email = user.Email;
                obj.BornDate = user.BornDate;
                obj.Gender = user.Gender;
                obj.EducationMaxReached = user.EducationMaxReached;
                obj.Interests = user.Interests;

                db.SaveChanges();

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
                var obj = db.Users.FirstOrDefault(x => x.Id == id);
                if (obj is null)
                    return NotFound();

                db.Users.Remove(obj);
                db.SaveChanges();

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
