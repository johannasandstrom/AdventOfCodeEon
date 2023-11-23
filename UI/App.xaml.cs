using System;
using System.Windows;

namespace AdventOfCodeEon.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            App app = new();
            app.InitializeComponent();
            app.Run();
        }
    }
}