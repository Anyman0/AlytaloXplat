using AlytaloXplat.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;


namespace AlytaloXplat.Views
{
	public class MainPageCS : ContentPage
	{
        public ListView viewList;
        public ListView offviewList;
        public ListView saunaList;
        public Label roomName;
        public Label offroomName;
        public Label saunaInfo;
        public Label mainInfo;
        
        public MainPageCS()
        {
            
            // List that shows the rooms with lights ON --- Itemsource defined in OnAppearing - Method
            viewList = new ListView()

            {

                Margin = new Thickness(20),
                
                ItemTemplate = new DataTemplate(() =>
                
                {
                    
                    roomName = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    };

                    roomName.SetBinding(Label.TextProperty, "Nimi");
                    roomName.FontAttributes = FontAttributes.Bold;
                    roomName.WidthRequest = 400;
                    
                    // Layout for list items
                    var stackLayout = new StackLayout

                    {
                       
                        Margin = new Thickness(0, 0, 0, 0),
                        BackgroundColor = Color.GreenYellow,
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.StartAndExpand,                       

                        Children = { roomName, }

                    };
                    
                    return new ViewCell { View = stackLayout };
                }),
            };

            // Moves the user to control the lights of the chosen room
            viewList.ItemSelected += async (sender, e) =>
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


            // List that shows the rooms with lights OFF --- Itemsource defined in OnAppearing - Method
            offviewList = new ListView()

            {

                Margin = new Thickness(20),
                ItemTemplate = new DataTemplate(() =>

                {

                    roomName = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    };

                    roomName.SetBinding(Label.TextProperty, "Nimi");
                    roomName.FontAttributes = FontAttributes.Bold;
                    roomName.WidthRequest = 400;

                    // Layout for list items
                    var stackLayout = new StackLayout

                    {

                        Margin = new Thickness(0, 0, 0, 0),
                        BackgroundColor = Color.Purple,
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.StartAndExpand,

                        Children = { roomName, }

                    };

                    return new ViewCell { View = stackLayout };
                }),
            };

            // Moves the user to control the lights of the chosen room
            offviewList.ItemSelected += async (sender, e) =>
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

            // list that shows the saunas where bool SaunanTila is true
            saunaList = new ListView
            {

                Margin = new Thickness(20),

                ItemTemplate = new DataTemplate(() =>

                {

                    var label = new Label

                    {

                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand

                    };

                    label.SetBinding(Label.TextProperty, "SaunanNimi");

                    var haluttutemp = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    };

                    haluttutemp.Text = "Haluttu lämpötila:  ";

                    var saunaTemperature = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    };

                    saunaTemperature.SetBinding(Label.TextProperty, "ToivottuTemp");



                    var stackLayout = new StackLayout

                    {

                        Margin = new Thickness(0, 0, 0, 0),
                        BackgroundColor = Color.GreenYellow,
                        Orientation = StackOrientation.Horizontal,

                        HorizontalOptions = LayoutOptions.FillAndExpand,

                        Children = { label, haluttutemp, saunaTemperature }

                    };


                    return new ViewCell { View = stackLayout };

                })
            };

            // Moves the uses to control the chosen sauna
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


            // all the toolbarItems defined here
            var saunatoolbarItem = new ToolbarItem
            {
                Text = "Sauna", 
            };

            saunatoolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new ViewSaunaPageCS { BindingContext = new SaunaDBModel() });
            };
            ToolbarItems.Add(saunatoolbarItem);

            var huonetoolbarItem = new ToolbarItem
            {
                Text = "Huoneet"
            };

            huonetoolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new ViewRoomsListCS { BindingContext = new HuoneDBModel() });
            };

            ToolbarItems.Add(huonetoolbarItem);


            var temptoolbarItem = new ToolbarItem
            {
                Text = "Lämpötilat"
            };

            temptoolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new TempPageCS {BindingContext = new TempDBModel()});
            };

            ToolbarItems.Add(temptoolbarItem);


            // All the informative Labels defined here
            mainInfo = new Label();
            mainInfo.Text = "Alla näet kaikki huoneesi, jossa on valaisimia päällä. Klikkaamalla huonetta, pääset hallitsemaan huoneen valaistusta.";
            mainInfo.FontSize = 20;          
            mainInfo.FontAttributes = FontAttributes.Bold;
            mainInfo.HorizontalTextAlignment = TextAlignment.Center;
            mainInfo.VerticalTextAlignment = TextAlignment.Center;

            offroomName = new Label();
            offroomName.Text = "Alla näet kaikki huoneesi, joissa EI ole valoja päällä.";
            offroomName.FontSize = 20;
            offroomName.FontAttributes = FontAttributes.Bold;            
            offroomName.HorizontalTextAlignment = TextAlignment.Center;
            offroomName.VerticalTextAlignment = TextAlignment.Center;

            saunaInfo = new Label();
            saunaInfo.Text = "Alla näet kaikki saunasi, jotka ovat päällä.";
            saunaInfo.FontSize = 20;           
            saunaInfo.FontAttributes = FontAttributes.Bold;
            saunaInfo.HorizontalTextAlignment = TextAlignment.Center;
            saunaInfo.VerticalTextAlignment = TextAlignment.Center;

            
            // Content of the page defined here           
            var kasa = new StackLayout();                                 
            kasa.BackgroundColor = Color.SaddleBrown;           
            kasa.Children.Add(mainInfo);
            kasa.Children.Add(viewList);
            kasa.Children.Add(offroomName);
            kasa.Children.Add(offviewList);
            kasa.Children.Add(saunaInfo);
            kasa.Children.Add(saunaList);                      
            Content = kasa;
        }

        // This method sets the itemsources used on the above lists 
        protected override async void OnAppearing()

        {
            base.OnAppearing();
                                 
            ((App)App.Current).ResumeAtHuoneId = -1;
            
            viewList.ItemsSource = await App.HDatabase.GetRoomsWithLightsOn();
            offviewList.ItemsSource = await App.HDatabase.GetRoomsWithLightsOff();
            saunaList.ItemsSource = await App.SDatabase.GetSaunaOn();

        }
       

    }
}