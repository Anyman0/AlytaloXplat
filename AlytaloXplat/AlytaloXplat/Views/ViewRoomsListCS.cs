using AlytaloXplat.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AlytaloXplat.Views
{
	public class ViewRoomsListCS : ContentPage
	{
        public ListView listView;
            
		public ViewRoomsListCS ()
		{
            
            BackgroundColor = Color.SaddleBrown;
            
            // toolbarItems defined here
            var toolbarItem = new ToolbarItem
           
            {
                Text = "Uusi huone",
                
            };

            toolbarItem.Clicked += async (sender, e) =>

            {
                await Navigation.PushAsync(new EditRoomsPageCS
                {
                    BindingContext = new HuoneDBModel()
                });
            };

            ToolbarItems.Add(toolbarItem);

         
            // list that shows all the rooms added by the user
            listView = new ListView

            {

                Margin = new Thickness(20),
                
                ItemTemplate = new DataTemplate(() =>

                {

                    var roomName = new Label {VerticalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.StartAndExpand, }; roomName.Text = "Huoneen nimi:    ";
                    var label = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,                       
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    };

                    label.SetBinding(Label.TextProperty, "Nimi");
                    label.FontAttributes = FontAttributes.Bold;


                    var lightName = new Label { VerticalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.StartAndExpand }; lightName.Text = "       Valaisimia:    ";
                    var valolabel = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.EndAndExpand
                        
                    };
                    
                    valolabel.SetBinding(Label.TextProperty, "Valot");
                    valolabel.FontAttributes = FontAttributes.Bold;


                    // Layout for list items
                    var stackLayout = new StackLayout

                    {

                        Margin = new Thickness(0, 0, 0, 0),

                        Orientation = StackOrientation.Horizontal,

                        BackgroundColor = Color.Khaki,

                        HorizontalOptions = LayoutOptions.FillAndExpand,                       
                         
                        Children = {roomName, label, lightName, valolabel,  }

                    };

                    
                    return new ViewCell { View = stackLayout};

                })

                
            };

            // Moves the user to modify the room
            listView.ItemSelected += async (sender, e) =>
            {
                ((App)App.Current).ResumeAtHuoneId = (e.SelectedItem as HuoneDBModel).huoneId;
                Debug.WriteLine("setting ResumeAtHuoneId = " + (e.SelectedItem as HuoneDBModel).huoneId);

                if (e.SelectedItem != null)
                {
                    await Navigation.PushAsync(new EditRoomsPageCS
                    {
                        BindingContext = e.SelectedItem as HuoneDBModel
                    });
                }
            };

            // Content of the page defined here
            Content = listView;

        }

        // itemsource for the list above defined here
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ((App)App.Current).ResumeAtHuoneId = -1;
            listView.ItemsSource = await App.HDatabase.GetRooms();
        }
    }
}