using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnsekWebApplication.Controllers
{

    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public AccountController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAccounts")]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _repository.Account.GetAllAccountsAsync(trackChanges: false);

            var accountsDto = _mapper.Map<IEnumerable<AccountDTO>>(accounts);

            return Ok(accountsDto);
        }

        [HttpGet("{id}", Name = "AccountById")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _repository.Account.GetAccountByIdAsync(id, trackChanges: false);

            if (account == null)
            {
                return NotFound();
            }
            else
            {
                var accountDto = _mapper.Map<AccountDTO>(account);
                return Ok(accountDto);
            }
        }


        [HttpPost(Name = "CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountForCreationDTO account)
        {
            var accountEntity = _mapper.Map<Account>(account);

            _repository.Account.CreateAccount(accountEntity);
            await _repository.SaveAsync();

            var accountToReturn = _mapper.Map<AccountDTO>(accountEntity);

            return CreatedAtRoute("CoffeeById", new { id = accountToReturn.Id }, accountToReturn);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                var account = await _repository.Account.GetAccountByIdAsync(id, trackChanges: false);
                if (account == null)
                {
                    return NotFound();
                }

                if (_repository.MeterReadings.GetMeterReadingByIdAsync(id, trackChanges: false).IsCompleted)
                {
                    return BadRequest("Cannot delete coffee. It has related comments. Delete those comments first");
                }

                _repository.Account.DeleteAccount(account);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountForUpdateDTO account)
        {
            try
            {
                if (account == null)
                {
                    return BadRequest("Coffee object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var accountEntity = await _repository.Account.GetAccountByIdAsync(id, trackChanges: false);
                if (accountEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(account, accountEntity);

                _repository.Account.UpdateAccount(accountEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/meterreadings")]
        public async Task<IActionResult> GetAccountsWithDetails(int id)
        {
            try
            {
                var account = await _repository.Account.GetAccountWithDetailsAsync(id, trackChanges: false);
                if (account == null)
                {
                    return NotFound();
                }
                else
                {

                    return Ok(account);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
