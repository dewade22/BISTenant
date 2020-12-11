using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manufacturing.Domain
{
    public class BaseEntity
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public T Id { get; set; }

        //object IEntity.Id
        //{
        //    get => this.Id;
        //    set => this.Id = (T)Convert.ChangeType(value, typeof(T));
        //}

        //public int RowStatus { get; set; }

        //private DateTime? createdDate;
        //[DataType(DataType.DateTime)]
        //public DateTime CreatedDate
        //{
        //    get { return createdDate ?? DateTime.UtcNow; }
        //    set { createdDate = value; }
        //}

        //[DataType(DataType.DateTime)]
        //public DateTime? ModifiedDate { get; set; }

        //public string CreatedBy { get; set; }

        //public string ModifiedBy { get; set; }

        //[DataType(DataType.DateTime)]
        //public DateTime? Deleted { get; set; }

        //public string VersionNo { get; set; }
        //}
        protected short _rowStatus = 0;
        protected string _createdBy = string.Empty;
        protected DateTime _createdTime = DateTime.Now;
        protected string? _lastModifiedBy = string.Empty;
        protected DateTime? _lastModifiedTime = DateTime.Now;

        public short RowStatus
        {
            get
            {
                return _rowStatus;
            }
            set
            {
                _rowStatus = value;
            }
        }

        public string CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                _createdBy = value;
            }
        }

        public DateTime CreatedTime
        {
            get
            {
                return _createdTime;
            }
            set
            {
                _createdTime = value;
            }
        }

        public string LastModifiedBy
        {
            get
            {
                return _lastModifiedBy;
            }
            set
            {
                _lastModifiedBy = value;
            }
        }

        public DateTime? LastModifiedTime
        {
            get
            {
                return _lastModifiedTime;
            }
            set
            {
                _lastModifiedTime = value;
            }
        }
    }
}
