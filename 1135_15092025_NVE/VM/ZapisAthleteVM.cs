using _1135_15092025_NVE.Model;
using _1135_15092025_NVE.View;
using _1135_15092025_NVE.VMTools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1135_15092025_NVE.VM
{
    internal class ZapisAthleteVM : BaseVM
    {
        private ObservableCollection<Workout> workouts = new();
        private ObservableCollection<Athlete> athletes = new();
        private ObservableCollection<AthleteWorkout> athleteWorkouts = new();
        private Workout selectedWorkout;
        private Athlete selectedAthlete;
        private AthleteWorkout selectedAthleteWorkout;
        private ZapisAthlete zapisAthlete;

        public ObservableCollection<Athlete> Athletes
        {
            get => athletes;
            set
            {
                athletes = value;
                Signal();
            }
        }

        public ObservableCollection<Workout> Workouts
        {
            get => workouts;
            set
            {
                workouts = value;
                Signal();
            }
        }

        public ObservableCollection<AthleteWorkout> AthleteWorkouts
        {
            get => athleteWorkouts;
            set
            {
                athleteWorkouts = value;
                Signal();
            }
        }

        public Workout SelectedWorkout
        {
            get => selectedWorkout;
            set
            {
                selectedWorkout = value;
                Signal();
            }
        }

        public Athlete SelectedAthlete
        {
            get => selectedAthlete;
            set
            {
                selectedAthlete = value;
                Signal();
            }
        }

        public AthleteWorkout SelectedAthleteWorkout
        {
            get => selectedAthleteWorkout;
            set
            {
                selectedAthleteWorkout = value;
                Signal();
            }
        }

        public CommandVM Connecting {  get; set; }
        public CommandVM Zareg { get; set; }
        public CommandVM Trenir { get; set; }

        public ZapisAthleteVM()
        {
            var db = new SportWorkoutContext();
            
            Workouts = new ObservableCollection<Workout>(db.Workouts.ToList());
            //Workouts = testST.getST.WorkoutsFROM_mainWINDOW;
            Athletes = new ObservableCollection<Athlete>(db.Athletes.ToList());
            AthleteWorkouts = new ObservableCollection<AthleteWorkout>(db.AthleteWorkouts.Include("Athlete").ToList());
            SelectedAthleteWorkout = new();

            Connecting = new CommandVM(() =>
            {
                SelectedAthleteWorkout.AthleteId = SelectedAthlete.Id;
                SelectedAthleteWorkout.WorkoutId = SelectedWorkout.Id;
                if (!AthleteWorkouts.Contains(SelectedAthleteWorkout))
                {
                    AthleteWorkouts.Add(SelectedAthleteWorkout);
                    db.Entry(SelectedAthleteWorkout).State = EntityState.Added;
                }
                else
                    db.Entry(SelectedAthleteWorkout).State = EntityState.Modified;
                    

                db.SaveChanges();
                //AthleteWorkouts = new ObservableCollection<AthleteWorkout>(db.AthleteWorkouts.Include("Athlete").ToList());
            }, () => SelectedWorkout != null && 
            SelectedAthlete != null);

            Trenir = new CommandVM(() =>
            {
                MainWindow main = new MainWindow();
                main.ShowDialog();
                zapisAthlete.Close();
            }, () => true);

            Zareg = new CommandVM(() =>
            {
                Registration reg = new Registration();
                reg.ShowDialog();
                zapisAthlete.Close();
            }, () => true);
        }

        internal void SetWindow(ZapisAthlete zapisAthlete)
        {
            this.zapisAthlete = zapisAthlete;
        }
    }
}
