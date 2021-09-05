using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cohesionChallenge.Data;
using cohesionChallenge.Models;
using Microsoft.EntityFrameworkCore;
using cohesionChallenge.Repositories;

namespace cohesionChallenge.Controllers
{
    [ApiController]
    [Route("api/servicerequest")]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestRepository _repository;
        public ServiceRequestController(IServiceRequestRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetServiceRequests()
        {
            List<ServiceRequest> requests = await _repository.GetAll();

            if (requests == null)
            {
                return NotFound();
            }

            return requests;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRequest>> GetServiceRequest(Guid id)
        {
            var request = await _repository.Get(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceRequest>> CreateServiceRequest(ServiceRequest request)
        {
            request.CurrentStatus = CurrentStatus.Created;
            request.CreatedDate = DateTime.UtcNow;
            request.LastModifiedDate = DateTime.UtcNow;
            request.LastModifiedBy = request.CreatedBy;

            await _repository.Add(request);

            return CreatedAtAction("GetServiceRequest", new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceRequest>> UpdateServiceRequest(Guid id, ServiceRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            ServiceRequest targetRequest = null;
            try
            {
                targetRequest = await _repository.Update(id, request);
            } catch (DbUpdateException)
            {
                return BadRequest();
            }

            if (targetRequest == null)
            {
                return NotFound();
            }
            return targetRequest;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRequest(Guid id)
        {
            try
            {
                await _repository.Delete(id);
            } catch (DbUpdateException e)
            {
                return NotFound();
            }

            return new StatusCodeResult(201);
        }
    }
}
