using cohesionChallenge.Data;
using cohesionChallenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cohesionChallenge.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly ServiceRequestContext _context;

        public ServiceRequestRepository(ServiceRequestContext context)
        {
            _context = context;
        }
        public async Task<ServiceRequest> Add(ServiceRequest request)
        {
            request.CurrentStatus = CurrentStatus.Created;
            request.CreatedDate = DateTime.UtcNow;
            request.LastModifiedDate = DateTime.UtcNow;
            request.LastModifiedBy = request.CreatedBy;

            _context.ServiceRequests.Add(request);

            await _context.SaveChangesAsync();
            return request;
        }

        public async Task Delete(Guid id)
        {
            ServiceRequest target = await _context.ServiceRequests.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            _context.ServiceRequests.Remove(target);

            await _context.SaveChangesAsync();
        }

        public async Task<ServiceRequest> Get(Guid id)
        {
            return await _context.ServiceRequests.FindAsync(id);
        }

        public async Task<List<ServiceRequest>> GetAll()
        {
            return await _context.ServiceRequests.ToListAsync();
        }

        public async Task<ServiceRequest> Update(Guid id, ServiceRequest serviceRequest)
        {
            ServiceRequest target = await _context.ServiceRequests.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (target == null)
            {
                return target;
            }
            target.BuildingCode = !string.IsNullOrWhiteSpace(serviceRequest.BuildingCode) ? serviceRequest.BuildingCode : target.BuildingCode;
            target.Description = !string.IsNullOrWhiteSpace(serviceRequest.Description) ? serviceRequest.Description : target.Description;
            target.CurrentStatus = serviceRequest.CurrentStatus;
            target.CreatedBy = !string.IsNullOrWhiteSpace(serviceRequest.CreatedBy) ? serviceRequest.CreatedBy : target.CreatedBy;
            target.CreatedDate = serviceRequest.CreatedDate != null ? serviceRequest.CreatedDate : target.CreatedDate;
            target.LastModifiedBy = serviceRequest.LastModifiedBy;
            target.LastModifiedDate = serviceRequest.LastModifiedDate != null ? serviceRequest.LastModifiedDate : DateTime.UtcNow;
            _context.Update(target);

            await _context.SaveChangesAsync();
            return target;
        }
    }
}
