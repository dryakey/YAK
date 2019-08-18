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

namespace AutinPower.WPF.Controls
{
    /// <summary>
    /// Interaction logic for UI_LabeledTextBox.xaml
    /// </summary>
    public partial class UI_LabeledTextBox : UserControl
    {
        public string Label { get => labelTitle.Content.ToString(); set => labelTitle.Content = value; }
        public string Value {
            get{
                return GetValue(TextValue).ToString();
               }
            set{
                SetValue(TextValue, value);
            } }

        public static readonly DependencyProperty TextValue
            = DependencyProperty.Register("Value", typeof(String), typeof(UI_LabeledTextBox), new FrameworkPropertyMetadata(string.Empty));

        public UI_LabeledTextBox()
        {
            InitializeComponent();
            textboxValue.DataContext = this;
        }
    }
}
