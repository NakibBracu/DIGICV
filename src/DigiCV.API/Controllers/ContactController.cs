using Autofac;
using DigiCV.API.Models;
using DigiCV.Domain.Entities;
using DigiCV.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigiCV.API.Controllers
{
    [EnableCors("AllowWebApp")]
    [ApiController]
    [Route("api/v1/[controller]message")]
    public class ContactController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<ContactController> _logger;
        
        public ContactController(ILogger<ContactController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);

            var model = _scope.Resolve<ContactModel>();

            var data = await model.GetPagedContactsAsync(dataTablesModel);
            return data;
        }
        //[HttpGet]
        //[Authorize(Policy = "AdminManager")]
        //public IEnumerable<Contact> Get()
        //{
        //    try
        //    {
        //        var model = _scope.Resolve<ContactModel>();
        //        return model.GetContacts();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Couldn't get contacts");
        //        return null;
        //    }
        //}

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminManager")]
        public Contact Get(Guid id)
        {
            var model = _scope.Resolve<ContactModel>();
            return model.GetContact(id);
        }

        [HttpPost()]
        public IActionResult Post([FromBody] ContactModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                model.CreateContact();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't create contact messages");
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var model = _scope.Resolve<ContactModel>();
                model.DeleteContact(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't delete contact");
                return BadRequest();
            }
        }

    }
}
