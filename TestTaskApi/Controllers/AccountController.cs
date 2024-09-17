using Microsoft.AspNetCore.Mvc;
using Repository.Models.DataTransferObjects;
using Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace TestTaskApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDTO accountDto)
        {
            try
            {
                await _accountService.CreateAccount(accountDto);
                return Ok("Account created successfully");
            }

            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
