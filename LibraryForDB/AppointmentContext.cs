namespace LibraryForDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AppointmentContext : DbContext
    {
        public AppointmentContext()
            : base("name=AppointmentContext")
        {
        }

        public virtual DbSet<AppointmentModel> AppointmentModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
