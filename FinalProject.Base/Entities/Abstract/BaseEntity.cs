﻿namespace FinalProject.Base
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
            Status = DataStatus.Inserted;
        }

        public int ID { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public DataStatus Status { get; set; }

    }
}
