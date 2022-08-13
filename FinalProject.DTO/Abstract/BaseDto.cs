using FinalProject.Base;
using FinalProject.Entities;

namespace FinalProject.DTO
{
    public abstract class BaseDto
    {
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int CreatedById { get; set; }

        public string CreatedBy { get; set; }

        public DataStatus Status { get; set; }
    }
}
