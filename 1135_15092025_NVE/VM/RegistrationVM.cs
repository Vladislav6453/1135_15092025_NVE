
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

        private List<Athlete> athletes;
        private Athlete newAthlete;
        private Registration registration;

        public List<Athlete> Athletes
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
        public DateTime Birthday 
        { 
            get 
            {
                if (NewAthlete != null && NewAthlete.Birthday.HasValue)
                    return NewAthlete.Birthday.Value.ToDateTime(new TimeOnly());
                return new DateTime();
            }
            set 
            {
                NewAthlete.Birthday = DateOnly.FromDateTime(value);
            }
        }

        public List<LevelOfTraining> Levels { get; set; }
        public List<AthletesCategory> Categories { get; set; }

        public CommandVM Registrat { get; set; }
        public CommandVM Zapis { get; set; }
        public CommandVM Trenir { get; set; }

        public RegistrationVM()
        {
            var db = new SportWorkoutContext();
            Athletes = db.Athletes.ToList();
            Levels = db.LevelOfTrainings.ToList();
            Categories = db.AthletesCategories.ToList();
            NewAthlete = new Athlete();
            Registrat = new CommandVM(() =>
            {

                RegistrateNewAthlete();
                db.Entry(NewAthlete).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                db.SaveChanges();
                NewAthlete = new Athlete();
            },() => !string.IsNullOrEmpty(NewAthlete.Name) &&
            !string.IsNullOrEmpty(NewAthlete.LastName) &&
            NewAthlete.Birthday != null &&
            NewAthlete.LevelOfTraining != null &&
            NewAthlete.Category != null);

            Zapis = new CommandVM(() =>
            {
                ZapisAthlete zap = new ZapisAthlete();
                registration.Close();
                zap.ShowDialog();


            }, () => true);

            Trenir = new CommandVM(() =>
            {
                MainWindow main = new MainWindow();
                registration.Close();
                main.ShowDialog();

 
            }, () => true);
        }


        public void RegistrateNewAthlete()
        {
            Athletes.Add(NewAthlete);
            

        }

        public void SetWindow(Registration window)
        {
            registration = window;
        }
    }
}
