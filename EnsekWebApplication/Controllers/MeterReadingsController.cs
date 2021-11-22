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
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public MeterReadingsController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetMeterReading")]
        public async Task<IActionResult> GetMeterReading()
        {
            var meterReadings = await _repository.MeterReadings.GetAllMeterReadingsAsync(trackChanges: false);

            var meterReadingsDto = _mapper.Map<IEnumerable<MeterReadingDTO>>(meterReadings);

            return Ok(meterReadingsDto);
        }

        [HttpGet("{id}", Name = "MeterReadingById")]
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


        [HttpPost]
        public async Task<IActionResult> CreateMeterReading(DateTime meterReadingDateTime, [FromBody] MeterReadingsForCreationDTO comment)
        {
            var meterReading = await _repository.MeterReadings.GetMeterReadingByIdAsync(meterReadingDateTime, trackChanges: false);
            if (meterReading == null)
            {
                return NotFound();
            }

            var meterReadingEntity = _mapper.Map<MeterReading>(meterReading);

            _repository.MeterReadings.CreateMeterReading(meterReadingDateTime, meterReadingEntity);
            await _repository.SaveAsync();

            var meterReadingToReturn = _mapper.Map<MeterReadingDTO>(meterReadingEntity);

            return CreatedAtRoute("MeterReadingById", new { meterReadingDateTime, id = meterReadingToReturn.MeterReadingDateTime }, meterReadingToReturn);
        }

        [HttpDelete("{id}")]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeterReading(DateTime meterReadingDateTime, [FromBody] MeterReadingsForUpdateDTO meterReading)
        {
            try
            {
                if (meterReading == null)
                {
                    return BadRequest("Comment object is null");
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
