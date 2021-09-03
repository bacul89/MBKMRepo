using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MBKM.Entities.Basentities
{
    public abstract class BaseEntity
    {
        #region Properties

        public virtual Int64 ID { get; set; }

        [StringLength(100)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        [Display(Name = "Update By")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Update Date")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }
        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }
        #endregion
    }
}
