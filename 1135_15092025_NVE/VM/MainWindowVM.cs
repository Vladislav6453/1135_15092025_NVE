using System.Collections.ObjectModel;
using System.Reflection;
using _1135_15092025_NVE.Model;
using _1135_15092025_NVE.View;
using _1135_15092025_NVE.VMTools;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace _1135_15092025_NVE.VM
{
    internal class MainWindowVM : BaseVM
    {
        private ObservableCollection<Workout> workouts = new();
        private ObservableCollection<WorkoutType> types = new();
        private List<Athlete> athletes;
        private List<AthleteWorkout> cross;
        private Workout selectedWorkout;
        private MainWindow mainWindow;

        public Workout SelectedWorkout
        {
            get => selectedWorkout;
            set
            {
                selectedWorkout = value;
                if (value != null )
                    selectedWorkout.Type = Types.FirstOrDefault(name => name == selectedWorkout.Type);
                Signal();
            }
        }

        public List<Athlete> Athletes
        {
            get => athletes;
            set
            {
                athletes = value;
                Signal();
            }
        }
        public List<AthleteWorkout> Cross
        {
            get => cross;
            set
            {
                cross = value;
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

        public ObservableCollection<WorkoutType> Types
        {
            get => types;
            set
            {
                types = value;
                Signal();
            }
        }

        public int Hours
        {
            get => SelectedWorkout.DateTime.Hour;
            set => SelectedWorkout.DateTime= SelectedWorkout.DateTime.AddHours(value);
        }
        public int Minutes
        {
            get => SelectedWorkout.DateTime.Minute;
            set => SelectedWorkout.DateTime=SelectedWorkout.DateTime.AddMinutes(value);
        }
        
     

        public CommandVM Delete { get; set; }
        public CommandVM Update { get; set; }
        public CommandVM Add { get; set; }
        public CommandVM Clear { get; set; }
        public CommandVM Registr { get; set; }
        public CommandVM Zapis { get; set; }
        public MainWindowVM() 
        {


            testST.getST.WorkoutsFROM_mainWINDOW = Workouts;

            var db = new SportWorkoutContext();
            Types = new ObservableCollection<WorkoutType>(db.WorkoutTypes.ToList());
            Workouts = new ObservableCollection<Workout>(db.Workouts.ToList());
            SelectedWorkout = new();

            Delete = new CommandVM(() =>
            {
                Workouts.Remove(SelectedWorkout);
                db.SaveChanges();
            }, () => SelectedWorkout != null && SelectedWorkout.Id != 0);

            Add = new CommandVM(() =>
            {
                if (!Workouts.Contains(SelectedWorkout))
                    Workouts.Add(SelectedWorkout);

                db.SaveChanges();
            }, () => !string.IsNullOrWhiteSpace(SelectedWorkout.Title) &&
            SelectedWorkout.Duration > 0 &&
            SelectedWorkout.DateTime.Year > 2000 &&
            SelectedWorkout.Type != null);

            Update = new CommandVM(() =>
            {
                if (!Workouts.Contains(SelectedWorkout))
                    db.SaveChanges();
            }, () => !string.IsNullOrWhiteSpace(SelectedWorkout.Title) &&
            SelectedWorkout.Duration > 0 &&
            SelectedWorkout.DateTime.Year > 2000 &&
            SelectedWorkout.Type != null);

            Registr = new CommandVM(() =>
            {
                Registration reg = new Registration();
                reg.ShowDialog(); 
                mainWindow.Close();
                

            }, () => true);

            Zapis = new CommandVM(() =>
            {
                ZapisAthlete reg = new ZapisAthlete();
                mainWindow.Close();
                reg.ShowDialog();


            }, () => true);

            Clear = new CommandVM(ClearSelected, () => true);
           
        }

        public void SetWindow(MainWindow window)
        {
            mainWindow = window;
        }

        private void ClearSelected()
        {
            SelectedWorkout = new();
        }
    }
}
