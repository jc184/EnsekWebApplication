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
    /// MeterReading Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IDbInitializer _dbInitializer;

        /// <summary>
        /// Constructor for MeterReading Controller
        /// </summary>
        public MeterReadingsController(IRepositoryManager repository, IMapper mapper, IDbInitializer dbInitializer)
        {
            _repository = repository;
            _mapper = mapper;
            _dbInitializer = dbInitializer;
        }


        /// <summary>
        /// Uploads Meter Readings file and adds data to database
        /// </summary>
        /// <response code="200">File added</response>
        [Route("meter-reading-uploads")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadDocument([FromHeader] String documentType, [FromForm] IFormFile file)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                file = formCollection.Files.First();
                var folderName = Path.Combine("Resources", "Uploads");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    int count = _dbInitializer.AddMeterReadings();
                    int rowcount = _dbInitializer.GetMeterReadingsCount();
                    return Ok(new { Message = "No of successful meter readings:" + " " + count + ", No of unsuccessful meter readings:" + " " + (rowcount - count) + ""});
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        /// <summary>
        /// Retrieves all MeterReading
        /// </summary>
        /// <response code="200">MeterReading retrieved</response>
        [HttpGet(Name = "GetMeterReading")]
        public async Task<IActionResult> GetMeterReading()
        {
            var meterReadings = await _repository.MeterReadings.GetAllMeterReadingsAsync(trackChanges: false);

            var meterReadingsDto = _mapper.Map<IEnumerable<MeterReadingDTO>>(meterReadings);

            return Ok(meterReadingsDto);
        }

        /// <summary>
        /// Retrieves a specific MeterReading by MeterReadingDateTine
        /// </summary>
        /// <response code="200">MeterReading retrieved</response>
        /// <response code="404">MeterReading not found</response>
        [HttpGet("{meterReadingDateTime}",Name = "MeterReadingById")]
        public async Task<IActionResult> GetMeterReading(DateTime meterReadingDateTime)
        {
            var meterReading = await _repository.MeterReadings.GetMeterReadingByIdAsync(meterReadingDateTime, trackChanges: false);

            if (meterReading == null)
            {
                return NotFound();
            }
            else
            {
                var meterReadingDto = _mapper.Map<MeterReadingDTO>(meterReading);
                return Ok(meterReadingDto);
            }
        }

        /// <summary>
        /// Creates a new MeterReading
        /// </summary>
        /// <response code="201">MeterReading added</response>
        /// <response code="400">Bad Request</response>
        [HttpPost(Name = "CreateMeterReading")]
        public async Task<IActionResult> CreateMeterReading([FromBody] MeterReadingsForCreationDTO meterReading)
        {

            var meterReadingEntity = _mapper.Map<MeterReading>(meterReading);

            _repository.MeterReadings.CreateMeterReading(meterReadingEntity);
            await _repository.SaveAsync();

            var meterReadingToReturn = _mapper.Map<MeterReadingDTO>(meterReadingEntity);

            return CreatedAtRoute(new { id = meterReadingToReturn.MeterReadingDateTime }, meterReadingToReturn);
        }


        /// <summary>
        /// Deletes a MeterReading
        /// </summary>
        /// <response code="204">MeterReading deleted</response>
        [HttpDelete("{meterReadingDateTime}")]
        public async Task<IActionResult> DeleteMeterReading(DateTime meterReadingDateTime)
        {
            try
            {
                var meterReading = await _repository.MeterReadings.GetMeterReadingByIdAsync(meterReadingDateTime, trackChanges: false);
                if (meterReading == null)
                {
                    return NotFound();
                }

                _repository.MeterReadings.DeleteMeterReading(meterReading);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Updates a MeterReading
        /// </summary>
        /// <response code="200">MeterReading updated</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("{meterReadingDateTime}")]
        public async Task<IActionResult> UpdateMeterReading(DateTime meterReadingDateTime, [FromBody] MeterReadingsForUpdateDTO meterReading)
        {
            try
            {
                if (meterReading == null)
                {
                    return BadRequest("MeterReading object is null");
                }

                if (!ModelState.IsValid)
                { 
                    return BadRequest("Invalid model object");
                }

                var meterReadingEntity = await _repository.MeterReadings.GetMeterReadingByIdAsync(meterReadingDateTime, trackChanges: false);
                if (meterReadingEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(meterReading, meterReadingEntity);

                _repository.MeterReadings.UpdateMeterReading(meterReadingEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
