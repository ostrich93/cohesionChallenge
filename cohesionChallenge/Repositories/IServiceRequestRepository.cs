using cohesionChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cohesionChallenge.Repositories
{
    public interface IServiceRequestRepository
    {
        Task<List<ServiceRequest>> GetAll();
        Task<ServiceRequest> Get(Guid id);
        Task<ServiceRequest> Add(ServiceRequest serviceRequest);
        Task<ServiceRequest> Update(Guid id, ServiceRequest serviceRequest);
        Task Delete(Guid id);
    }
}
