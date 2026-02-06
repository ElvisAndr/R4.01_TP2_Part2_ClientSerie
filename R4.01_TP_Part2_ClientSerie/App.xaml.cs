using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using R4._01_TP_Part2_ClientSerie.ViewModels;
using R4._01_TP_Part2_ClientSerie.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace R4._01_TP_Part2_ClientSerie
{
    
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window? _window;
        public static FrameworkElement MainRoot { get; private set; }
        public ServiceProvider Services { get; private set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();

            /// <summary>
            /// Congigure the services for the application
            /// </summary>
            ServiceCollection services = new ServiceCollection();

            //ViewModels
            services.AddTransient<AjoutSerieVM>();

            Services = services.BuildServiceProvider();
        }

        /// <summary>
        /// Gets the current app instance in use
        /// </summary>
        public static new App Current => (App)Application.Current;

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWidow();
            Frame rootFrame = new Frame();
            this.m_window.Content = rootFrame;
            m_window.Activate();
            rootFrame.Navigate(typeof(AjoutSerieView));

            MainRoot = m_window.Content as FrameworkElement;
        }
        private Window m_window;
    }
}
