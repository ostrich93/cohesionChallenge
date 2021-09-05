using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace cohesionChallenge.Models
{
    public class ServiceRequest
    {
        [Key]
        public Guid Id { get; set; }
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
