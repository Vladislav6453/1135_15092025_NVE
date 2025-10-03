using _1135_15092025_NVE.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace _1135_15092025_NVE.VM
{
    public class testST
    {
        static private testST cur;

        private testST()
        { 
        }

        public static testST getST { get { if (cur == null) cur = new testST(); return cur; } }

        public ObservableCollection<Workout> WorkoutsFROM_mainWINDOW;




}
}
