using System.Collections.ObjectModel;
using _1135_15092025_NVE.Model;
using _1135_15092025_NVE.VMTools;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace _1135_15092025_NVE.VM
{
    internal class MainWindowVM : BaseVM
    {
        private ObservableCollection<Workout> workouts = new();
        private List<Athlete> athletes;
        private List<AthleteWorkout> cross;
        private Workout selectedWorkout;
        public Workout SelectedWorkout
        {
            get => selectedWorkout;
            set
            {
                selectedWorkout = value;
                if (value != null && Types.Contains(selectedWorkout.Type))
                    selectedWorkout.Type = Types.First(name => name == selectedWorkout.Type);
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

        //public TimeOnly SelectedWorkoutTime
        //{
        //    get => SelectedWorkout != null ? new TimeOnly(SelectedWorkout.DateTime.Hour, SelectedWorkout.DateTime.Second) : new();
        //    set
        //    {
        //        if (SelectedWorkout != null)
        //        {
        //            SelectedWorkout.DateTime = new DateTime(SelectedWorkout.DateTime.Year, SelectedWorkout.DateTime.Month,
        //                SelectedWorkout.DateTime.Day, value.Hour, value.Minute, value.Second);
        //        }
        //    }
        //}
        public int Hours
        {
            get => SelectedWorkout != null ? SelectedWorkout.DateTime.Hour : 0;
            set => SelectedWorkout.DateTime.AddHours(value - SelectedWorkout.DateTime.Hour);
        }
        public int Minutes
        {
            get => SelectedWorkout != null ? SelectedWorkout.DateTime.Minute : 0;
            set => SelectedWorkout.DateTime.AddMinutes(value - SelectedWorkout.DateTime.Minute);
        }

        public string[] Types { get; set; } = ["Кардио", "Силовая", "Техническая"];

        public CommandVM Delete { get; set; }
        public CommandVM Add { get; set; }
        public CommandVM Clear { get; set; }
        public MainWindowVM()
        {
            var db = new SportWorkoutContext();
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
            !string.IsNullOrEmpty(SelectedWorkout.Type));

            Clear = new CommandVM(ClearSelected, () => true);
        }

        private void ClearSelected()
        {
            SelectedWorkout = new();
        }
    }
}
