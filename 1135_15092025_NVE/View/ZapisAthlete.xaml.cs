using _1135_15092025_NVE.VM;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace _1135_15092025_NVE.View
{
    /// <summary>
    /// Логика взаимодействия для ZapisAthlete.xaml
    /// </summary>
    public partial class ZapisAthlete : Window
    {
        public ZapisAthlete()
        {
            InitializeComponent();
            ((ZapisAthleteVM)DataContext).SetWindow(this);
            
        }
    }
}
