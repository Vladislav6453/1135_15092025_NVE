
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
        private List<LevelOfTrainig> levels = new();
        private List<AthletesCategory> types = new();
        private ObservableCollection<Athlete> athletes = new();
        private Athlete newAthlete;
        private Registration registration;
        public List<AthletesCategory> Types
        {
            get => types;
            set
            {
                types = value;
                Signal();
            }
        }
        
        public List<LevelOfTrainig> Levels
        {
            get => levels;
            set
            {
                levels = value;
                Signal();
            }
        }

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

        public RegistrationVM()
        {
            var db = new SportWorkoutContext();
            Registr = new CommandVM(() =>
            {
                Athletes.Add(NewAthlete);
                NewAthlete.Name = string.Empty;
                NewAthlete.LastName = string.Empty;
                NewAthlete.Birthday = DateOnly.;
                NewAthlete.Category = null;
                NewAthlete.Level = null;
            },());
        }
    }
}
