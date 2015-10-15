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

namespace Sheduler
{
    public class ShedulerViewModel : INotifyPropertyChanging
    {
        public ObservableCollection<Appointment> Load()
        {
            using (var db = new ModelAppointment())
            {
                var collection = new ObservableCollection<Appointment>();               
                var findApps = db.AllAppointments.Select(app => app);
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
                
                return collection;
            }
        }
        public void schedulerView_AppointmentCreated(object sender, AppointmentCreatedEventArgs e)
        {
            using (var db = new ModelAppointment())
            {
                var appAdd = (Appointment)(e.CreatedAppointment);
                var newApp = new AllAppointment()
                {
                    Id = appAdd.UniqueId,
                    StartTime = appAdd.Start,
                    EndTime = appAdd.End,
                    Subject = appAdd.Subject,
                    Body = appAdd.Body
                };
                db.AllAppointments.Add(newApp);
                db.SaveChanges();
            }        
        }
        public void schedulerView_AppointmentDeleted(object sender, AppointmentDeletedEventArgs e)
        {
            using (var db = new ModelAppointment())
            {
                var appDelet = (Appointment)(e.Appointment);
                var findAppInDB = db.AllAppointments.FirstOrDefault(applic => applic.Id == appDelet.UniqueId);
                db.AllAppointments.Remove(findAppInDB);
                db.SaveChanges();
            }
            
        }
        public void schedulerView_AppointmentEdited(object sender, AppointmentEditedEventArgs e)
        {
            using (var db = new ModelAppointment())
            {
                var appModific = (Appointment)(e.Appointment);
                var findAppInDB = db.AllAppointments.FirstOrDefault(applic => applic.Id == appModific.UniqueId);
                findAppInDB.StartTime = appModific.Start;
                findAppInDB.EndTime = appModific.End;
                findAppInDB.Subject = appModific.Subject;
                findAppInDB.Body = appModific.Body;
                db.SaveChanges();
            }       
        }
        public event PropertyChangingEventHandler PropertyChanging;
    }
}
