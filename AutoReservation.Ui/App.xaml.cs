using System.Windows;
using AutoReservation.Ui.ViewModels;
using Ninject;

namespace AutoReservation.Ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel kernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            kernel = LoadNinject();

            var vm = kernel.Get<MainWindowViewModel>();
            var view = new MainWindow();
            view.DataContext = vm;
            vm.Init();
            this.MainWindow = view;
            this.MainWindow.Show();
        }

        private IKernel LoadNinject()
        {
            var kernel = new StandardKernel(new AutoReservationModule());
            kernel.Load("Dependencies.Ninject.xml");
            return kernel;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            kernel.Dispose();
            base.OnExit(e);
        }
    }
}
