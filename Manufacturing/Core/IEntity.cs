using System;

namespace Manufacturing.Core
{
    public interface IEntity
    {
        object Id { get; set; }
        int RowStatus { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        DateTime? Deleted { get; set; }
        string VersionNo { get; set; }
    }

    public interface IEntity<T> : IEntity
    {
        new T Id { get; set; }
    }
}
