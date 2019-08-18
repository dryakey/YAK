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
using System.Windows.Resources;
using System.Windows.Shapes;
using Autin.Model;

namespace AutinPower.WPF.Controls
{
    /// <summary>
    /// Interaction logic for UI_User.xaml
    /// </summary>
    public partial class UI_User : UserControl
    {
        public User User {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                this.DataContext = _user;
                SetProfileImage();
            }
        }
        public Action LogoutAction { get; set; }

        private User _user;

        public UI_User()
        {
            InitializeComponent();
        }

        public void SetProfileImage()
        {
            switch (_user.Role)
            {
                case "admin":
                    {
                        var resourceUri = new Uri("/AutinPower.WPF;component/Images/manager.png", UriKind.Relative);
                        StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                        BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                        ImageBrush brush = new ImageBrush();
                        brush.ImageSource = temp;
                        profileImage.Fill = brush;
                        break;
                    }
            }
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            LogoutAction?.Invoke();
        }
    }
}
