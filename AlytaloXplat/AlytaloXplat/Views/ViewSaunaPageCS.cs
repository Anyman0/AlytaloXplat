using AlytaloXplat.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AlytaloXplat.Views
{
    public class ViewSaunaPageCS : ContentPage
    {

        public ListView saunaList;
        public ViewSaunaPageCS()
        {
            BackgroundColor = Color.SaddleBrown;

            // toolbaritems defined here
            var saunatoolbarItem = new ToolbarItem
            {
                Text = "Muokkaa/Lisää Sauna"
            };

            saunatoolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new EditSaunaPageCS { BindingContext = new SaunaDBModel() });
            };

            ToolbarItems.Add(saunatoolbarItem);

            // list that shows all the saunas added by the user
            saunaList = new ListView
            {

                Margin = new Thickness(20),

                ItemTemplate = new DataTemplate(() =>

                {

                    var label = new Label

                    {

                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontAttributes = FontAttributes.Italic,

                    };

                    label.SetBinding(Label.TextProperty, "SaunanNimi");


                    // Layout for list items
                    var stackLayout = new StackLayout

                    {

                        Margin = new Thickness(0, 0, 0, 0),

                        Orientation = StackOrientation.Vertical,

                        BackgroundColor = Color.Khaki,

                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        
                        Children = { label,}

                    };


                    return new ViewCell { View = stackLayout };

                })
            };

            // Moves the user to modify the sauna
            saunaList.ItemSelected += async (sender, e) =>
            {

                ((App)App.Current).ResumeAtSaunaId = (e.SelectedItem as SaunaDBModel).SaunaId;
                Debug.WriteLine("setting ResumeAtSaunaId = " + (e.SelectedItem as SaunaDBModel).SaunaId);

                if (e.SelectedItem != null)

                {
                    await Navigation.PushAsync(new EditSaunaPageCS
                    {
                        BindingContext = e.SelectedItem as SaunaDBModel
                    });
                }

            };

            // Content of the page defined here
            Content = saunaList;
        }

        // itemsource for the list above defined here
        protected override async void OnAppearing()
        {
            base.OnAppearing();           
            ((App)App.Current).ResumeAtSaunaId = -1;
            saunaList.ItemsSource = await App.SDatabase.GetSaunaAsync();
        }
    }
}



            


   