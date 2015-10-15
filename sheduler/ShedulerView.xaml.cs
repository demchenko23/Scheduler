using LibraryForDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace Sheduler
{
    public partial class ShedulerView : Window
    {
        public ShedulerView()
        {
            InitializeComponent();
            var vm = new ShedulerViewModel();
            schedulerView.AppointmentsSource = vm.Load();
            schedulerView.AppointmentCreated += vm.schedulerView_AppointmentCreated;
            schedulerView.AppointmentDeleted += vm.schedulerView_AppointmentDeleted;
            schedulerView.AppointmentEdited += vm.schedulerView_AppointmentEdited;
        }
    }
}
