using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EnsekWebApplication.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IDbInitializer _dbInitializer;

        /// <summary>
        /// Constructor for Account Controller
        /// </summary>
        public AccountController(IRepositoryManager repository, IMapper mapper, IDbInitializer dbInitializer)
        {
            _repository = repository;
            _mapper = mapper;
            _dbInitializer = dbInitializer; 
        }

        /// <summary>
        /// Retrieves all accounts
        /// </summary>
        /// <response code="200">Accounts retrieved</response>
        [HttpGet(Name = "GetAccounts")]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _repository.Account.GetAllAccountsAsync(trackChanges: false);

            var accountsDto = _mapper.Map<IEnumerable<AccountDTO>>(accounts);

            return Ok(accountsDto);
        }

        /// <summary>
        /// Retrieves a specific account by id
        /// </summary>
        /// <response code="200">Account retrieved</response>
        /// <response code="404">Account not found</response>
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

        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <response code="201">Account added</response>
        /// <response code="400">Bad Request</response>
        [HttpPost(Name = "CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountForCreationDTO account)
        {
            var accountEntity = _mapper.Map<Account>(account);

            _repository.Account.CreateAccount(accountEntity);
            await _repository.SaveAsync();

            var accountToReturn = _mapper.Map<AccountDTO>(accountEntity);

            return CreatedAtRoute(new { id = accountToReturn.AccountId }, accountToReturn);
        }


        /// <summary>
        /// Deletes an account
        /// </summary>
        /// <response code="204">Account deleted</response>
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

                //if (_repository.MeterReadings.GetMeterReadingByIdAsync(meterReadingDateTime, trackChanges: false).IsCompleted)
                //{
                //    return BadRequest("Cannot delete Account. It has related meterreadings. Delete those first");
                //}

                _repository.Account.DeleteAccount(account);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Updates an account
        /// </summary>
        /// <response code="200">Account updated</response>
        /// <response code="400">Bad Request</response>
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

        /// <summary>
        /// Retrieves all accounts with details
        /// </summary>
        /// <response code="200">Accounts retrieved</response>
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
