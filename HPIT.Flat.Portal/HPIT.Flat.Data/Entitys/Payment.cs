namespace HPIT.Flat.Data.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payment()
        {
            PayMentFileAttach = new HashSet<PayMentFileAttach>();
        }

        [Required]
        [StringLength(36)]
        public string PID { get; set; }

        [Key]
        [StringLength(36)]
        public string MID { get; set; }

        public decimal? PayMoney { get; set; }

        [StringLength(32)]
        public string StuName { get; set; }

        [StringLength(32)]
        public string StuNo { get; set; }

        public string Remark { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        //public virtual PayRequest PayRequest { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayMentFileAttach> PayMentFileAttach { get; set; }
    }
}
