using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Forgot.Views;

namespace Forgot
{
    public partial class App : Application
    {
        public static String DbName;
        public static String DbPath;

        public App()
        {
            InitializeComponent();

            MainPage = new PagePrincipal();
        }

        // Como é uma linguagem procedural, caso não haja parâmetros, ou seja, não há banco, o método executado será o de cima, caso contrário será o de baixo
        public App(string dbName, string dbPath)
        {
            InitializeComponent();

            App.DbName = dbName;
            App.DbPath = dbPath;

            MainPage = new PagePrincipal();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
