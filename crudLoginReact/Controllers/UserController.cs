using crudLoginReact.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace crudLoginReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext userContext;

        public UserController(UserContext userContext)
        {
            this.userContext = userContext;
        }

        [HttpGet]
        [Route("GetUsuarios")]
        public ActionResult<List<Usuario>> GetUsuarios()
        {
            try
            {
                return Ok(userContext.Usuario.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetUsuario")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            try
            {
                var usuario = userContext.Usuario.Where(x => x.id_user == id).FirstOrDefault();
                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound("Usuario no encontrado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("AddUsuario")]
        public ActionResult<string> AddUsuario(Usuario usuario)
        {
            try
            {
                userContext.Usuario.Add(usuario);
                userContext.SaveChanges();
                return Ok("Usuario agregado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("UpdateUsuario")]
        public ActionResult<string> UpdateUsuario(Usuario usuario)
        {
            try
            {
                userContext.Entry(usuario).State = EntityState.Modified;
                userContext.SaveChanges();
                return Ok("Usuario modificado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public ActionResult<string> DeleteUsuario(int id)
        {
            try
            {
                var usuario = userContext.Usuario.Where(x => x.id_user == id).FirstOrDefault();
                if (usuario != null)
                {
                    userContext.Usuario.Remove(usuario);
                    userContext.SaveChanges();
                    return Ok("Usuario eliminado");
                }
                else
                {
                    return NotFound("Usuario no encontrado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
