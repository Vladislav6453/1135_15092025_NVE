
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
        private ObservableCollection<LevelOfTrainig> levels = new();
        private ObservableCollection<AthletesCategory> types = new();
        private Registration registration;
        public ObservableCollection<AthletesCategory> Types
        {
            get => types;
            set
            {
                types = value;
                Signal();
            }
        }

        public ObservableCollection<LevelOfTrainig> Levels
        {
            get => levels;
            set
            {
                levels = value;
                Signal();
            }
        }


    }
}
