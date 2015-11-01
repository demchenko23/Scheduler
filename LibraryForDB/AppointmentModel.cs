namespace LibraryForDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Appointment")]
    public partial class AppointmentModel
    {
        [StringLength(50)]
        public string Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [StringLength(100)]
        public string Subject { get; set; }

        [StringLength(500)]
        public string Body { get; set; }
    }
}
