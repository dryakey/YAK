using System.Collections.Generic;
using System.Windows;
using Autin.Model;
using CommonServiceLocator;
using Prism.Ioc;
using Prism.Regions;

namespace AutinPowerMain.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IContainerExtension _container;
        IRegionManager _regionManager;

        public MainWindow(IContainerExtension container, IRegionManager regionManager)
        {
            InitializeComponent();

            _container = container;
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            _regionManager.Regions.Remove("ContentRegion");
            RegionManager.SetRegionManager(this.MainRegion, _regionManager);

            InitialToLoadLoginUI();
            //PostRequestTestAsync();
        }

        public void InitialToLoadLoginUI()
        {
            var view = _container.Resolve<UI_Login>();
            IRegion region = _regionManager.Regions["ContentRegion"];
            region.Add(view);
        }
    }
}
