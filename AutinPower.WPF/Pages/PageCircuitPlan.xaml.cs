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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutinPower.WPF.Pages
{
    /// <summary>
    /// Interaction logic for PageCircuitPlan.xaml
    /// </summary>
    public partial class PageCircuitPlan : Page
    {
        public PageCircuitPlan()
        {
            InitializeComponent();
            
            busED.LineMovingToNomal();

            busAD.LineMovingToHighLoad();
            busADv.LineMovingToHighLoad();

            busAE.LineMovingToNomal();
            busAEv.LineMovingToNomal();

            busBC.LineMovingToBlock();

            busDC0.LineMovingToNomal();
            busDC1.LineMovingToNomal();
            busDCv.LineMovingToNomal();

            busAB.LineMovingToLoose();
            busABv.LineMovingToLoose();

            loadB.LineMovingToNomal();
            loadC.LineMovingToHighLoad();
            loadD.LineMovingToBlock();
        }
        
    }
}
