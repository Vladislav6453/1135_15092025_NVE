
using _1135_15092025_NVE.Model;
using _1135_15092025_NVE.View;
using _1135_15092025_NVE.VMTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1135_15092025_NVE.VM
{
    internal class RegistrationVM : BaseVM
    {

        private ObservableCollection<Athlete> athletes = new();
        private Athlete newAthlete;
        private Registration registration;

        public ObservableCollection<Athlete> Athletes
        {
            get => athletes;
            set
            {
                athletes = value;
                Signal();
            }
        }

        public Athlete NewAthlete
        {
            get => newAthlete;
            set
            {
                newAthlete = value;
                Signal();
            }
        }

        public CommandVM Registr { get; set; }
        public CommandVM Zapis { get; set; }
        public CommandVM Trenir { get; set; }

        public RegistrationVM()
        {
            var db = new SportWorkoutContext();
            Registr = new CommandVM(() =>
            {
                Athletes.Add(NewAthlete);
                NewAthlete.Name = string.Empty;
                NewAthlete.LastName = string.Empty;
                NewAthlete.Birthday = null;
                NewAthlete.Category.Title = string.Empty;
                NewAthlete.Level.Title = string.Empty;
            },() => !string.IsNullOrWhiteSpace(NewAthlete.Name) &&
            NewAthlete.Birthday != null &&
            NewAthlete.Level.Title != null &&
            NewAthlete.Category.Title != null);

            Zapis = new CommandVM(() =>
            {
                ZapisAthlete reg = new ZapisAthlete();
                registration.Close();
                reg.ShowDialog();


            }, () => true);

            Trenir = new CommandVM(() =>
            {
                MainWindow main = new MainWindow();
                registration.Close();
                main.ShowDialog();

 
            }, () => true);
        }

        

        public void SetWindow(Registration window)
        {
            registration = window;
        }
    }
}
