using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryForDB;
using Telerik.Windows.Controls;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls.ScheduleView;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace Sheduler
{
    public class ShedulerViewModel
    {
        public ObservableCollection<Appointment> LoadAppointment { get; set; }
        public DelegateCommand<AppointmentCreatedEventArgs> SaveAppointment { get; set; }
        public DelegateCommand<AppointmentDeletedEventArgs> DeleteAppointment { get; set; }
        public DelegateCommand<AppointmentEditedEventArgs> EditAppointment { get; set; }
        public ShedulerViewModel()
        {
            this.LoadAppointment = Load();
            this.SaveAppointment = new DelegateCommand<AppointmentCreatedEventArgs>(Save);
            this.DeleteAppointment = new DelegateCommand<AppointmentDeletedEventArgs>(Delete);
            this.EditAppointment = new DelegateCommand<AppointmentEditedEventArgs>(Edit);
        }
        private ObservableCollection<Appointment> Load()
        {
            using (var db = new AppointmentContext())
            {
                var collection = new ObservableCollection<Appointment>();
                try
                {
                var findApps = db.AppointmentModels.Select(app => app);
                if (findApps.Count() > 0)
                {
                    foreach (var app in findApps)
                    {
                        var appoint = new Appointment();
                        appoint.UniqueId = app.Id;
                        appoint.Start = app.StartTime;
                        appoint.End = app.EndTime;
                        appoint.Subject = app.Subject;
                        appoint.Body = app.Body;
                        collection.Add(appoint);
                    }
                }
                }
                catch
                { }                
                return collection;
            }
        }
        public void Save(AppointmentCreatedEventArgs e)
        {
            using (var db = new AppointmentContext())
            {
                var appointment = (Appointment)(e.CreatedAppointment);
                var newAppointment = new AppointmentModel()
                {
                    Id = appointment.UniqueId,
                    StartTime = appointment.Start,
                    EndTime = appointment.End,
                    Subject = appointment.Subject,
                    Body = appointment.Body
                };
                db.AppointmentModels.Add(newAppointment);
                db.SaveChanges();
            }        
        }
        public void Delete(AppointmentDeletedEventArgs e)
        {
            var appointment = (Appointment)(e.Appointment);
            using (var db = new AppointmentContext())
            {

                var findAppInDB = db.AppointmentModels.FirstOrDefault(applic => applic.Id == appointment.UniqueId);
                db.AppointmentModels.Remove(findAppInDB);
                db.SaveChanges();
            }
        }
        public void Edit(AppointmentEditedEventArgs e)
        {
            var appointment = (Appointment)(e.Appointment);
            using (var db = new AppointmentContext())
            {
                var findAppInDB = db.AppointmentModels.FirstOrDefault(applic => applic.Id == appointment.UniqueId);
                findAppInDB.StartTime = appointment.Start;
                findAppInDB.EndTime = appointment.End;
                findAppInDB.Subject = appointment.Subject;
                findAppInDB.Body = appointment.Body;
                db.SaveChanges();
            }       
        }
    }
}
