


using AlytaloXplat.Data;
using AlytaloXplat.Views;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace AlytaloXplat
{
    public partial class App : Application
    {

        
        static HuoneDatabase hDatabase;
        static SaunaDatabase sDatabase;
        static TempDatabase tDatabase;
        public App()
        {

            var nav = new NavigationPage(new MainPageCS()) { BarBackgroundColor = Color.Brown };
            MainPage = nav;

            var buttonStyle = new Style(typeof(Button))
            {
                Setters =
                {             
                new Setter { Property = Button.BorderColorProperty,    Value = Color.Black},
                new Setter { Property = Button.BackgroundColorProperty, Value = Color.Yellow},
                new Setter {Property = Button.TextProperty, Value = FontAttributes.Bold}               
                }
            };

           
            Resources = new ResourceDictionary();
            Resources.Add("buttonStyle", buttonStyle);          

        }


        public static HuoneDatabase HDatabase
        {
            get
            {
                if (hDatabase == null)
                {
                    hDatabase = new HuoneDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AlyTaloDataBase.db"));
                }

                return hDatabase;
            }
        }

        public static SaunaDatabase SDatabase
        {
            get
            {
                if (sDatabase == null)
                {
                    sDatabase = new SaunaDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AlyTaloSaunaDataBase.db"));
                }
                return sDatabase;
            }
        }

        public static TempDatabase TDatabase
        {
            get
            {
                if (tDatabase == null)
                {
                    tDatabase = new TempDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AlyTaloTempDataBase.db"));
                }
                return tDatabase;
            }
        }

        public int ResumeAtHuoneId { get; set; }
        public int ResumeAtSaunaId { get; set; }
        public int ResumeAtTempId { get; set; }
           
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        
    }
}
