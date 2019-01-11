using AlytaloXplat.Models;
using AlytaloXplat.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlytaloXplat
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            //InitializeComponent();
            
        }
        protected override async void OnAppearing()

        {
            base.OnAppearing();

            // Reset the 'resume' id, since we just want to re-start here

            ((App)App.Current).ResumeAtHuoneId = -1;

            viewList.ItemsSource = await App.HDatabase.GetRoomsWithLightsOn();

        }

        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewRoomsListCS
            {
                BindingContext = new HuoneDBModel()
            });
        }

        async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewSaunaPageCS { BindingContext = new SaunaDBModel() });
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ((App)App.Current).ResumeAtHuoneId = (args.SelectedItem as HuoneDBModel).huoneId;

            if (args.SelectedItem != null)
            {
                await Navigation.PushAsync(new EditRoomsPageCS

                {
                    BindingContext = args.SelectedItem as HuoneDBModel
                });
            }
        }

    }
    	
}
