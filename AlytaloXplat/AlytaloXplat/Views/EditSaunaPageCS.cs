using AlytaloXplat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AlytaloXplat.Views
{
	public class EditSaunaPageCS : ContentPage
	{
        public int saunanlampotila = 20;     
        public int saunatick;
        public Label SaunaStatus;
        public Button deleteButton;
        public Button saveButton;
        public Switch saunaSwitch;
		public EditSaunaPageCS ()
		{
            BackgroundColor = Color.SaddleBrown;
            Title = "Hallitse saunaa";
            
            // Entry fields
            var nameEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, "SaunanNimi");

            // Save and Delete - Buttons defined here
            var saveSauna = new Button();
            saveSauna.Style = (Style)Application.Current.Resources["buttonStyle"];
            saveSauna.Text = "Tallenna uusi sauna";
            saveSauna.Clicked += async (sender, e) =>
            {
                var sauna = (SaunaDBModel)BindingContext;
                await App.SDatabase.SaveSaunaAsync(sauna);
            };

            deleteButton = new Button { Text = "Poista", Style = (Style)Application.Current.Resources["buttonStyle"] };
            deleteButton.IsVisible = true;
            deleteButton.Clicked += async (sender, e) =>
            {
                var sauna = (SaunaDBModel)BindingContext;
                await App.SDatabase.DeleteSaunaAsync(sauna);
                await Navigation.PopAsync();
            };


            var reqTemp = new Label();
            reqTemp.FontAttributes = FontAttributes.Bold;
            reqTemp.FontSize = 20;
            reqTemp.Text = "Haluttu lämpötila: ";
                                    
            // This slider defines the temperature of chosen sauna
            var saunaTemp = new Slider();           
            saunaTemp.Maximum = 120;
            saunaTemp.Minimum = 20;
            saunaTemp.SetBinding(Slider.ValueProperty, "ToivottuTemp");
            saunaTemp.ValueChanged += SaunaTemp_ValueChanged;
            void SaunaTemp_ValueChanged(object sender, ValueChangedEventArgs e)
            {
                reqTemp.Text = "Haluttu lämpötila: " + e.NewValue;
            }

            // This label shows the current temperature of started sauna 
            var SaunanTemp = new Label();
            SaunanTemp.FontAttributes = FontAttributes.Italic;
            SaunanTemp.FontSize = 25;
            SaunanTemp.Text = "Lämpötila: " + saunanlampotila;


            // Switch to turn the chosen sauna ON/OFF -- Includes a bunch of IF - statements to control the different actions and behaviours
            saunaSwitch = new Switch();
            saunaSwitch.SetBinding(Switch.IsToggledProperty, "SaunanTila");

            saunaSwitch.Toggled += SaunaSwitch_Toggled;
               async void SaunaSwitch_Toggled(object sender, ToggledEventArgs e)
               {
                  
                    var sauna = (SaunaDBModel)BindingContext;
                    await App.SDatabase.SaveSaunaAsync(sauna);
                    
                    if (saunaSwitch.IsToggled == false && saunanlampotila == 20)
                    {
                        SaunaStatus.Text = "Sauna on pois päältä.";
                        SaunaStatus.BackgroundColor = Color.Red;
                        deleteButton.IsVisible = true;
                        saveButton.IsVisible = false;                       
                    }
                    else if (saunaSwitch.IsToggled == true)
                    {
                        SaunaStatus.Text = "Sauna on päällä!";
                        SaunaStatus.BackgroundColor = Color.LightYellow;
                        saveButton.IsVisible = true;
                        deleteButton.IsVisible = false;
                    }

                    
                    else if (saunaSwitch.IsToggled != true && saunanlampotila != 20)
                    {
                        // Timer to lower the temperature of chosen sauna when turned off -- IF - statements to control different actions and behaviours
                        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                        {

                            if (saunanlampotila == 20 && saunaSwitch.IsToggled != true)
                            {
                                SaunaStatus.Text = "Sauna on pois päältä.";
                                SaunaStatus.BackgroundColor = Color.Red;
                                deleteButton.IsVisible = true;
                                return false;
                            }

                            else if (saunanlampotila == 20 && saunaSwitch.IsToggled != false )
                            {
                                SaunaStatus.Text = "Sauna on päällä!";
                                SaunaStatus.BackgroundColor = Color.LightYellow;
                                return false;
                            }

                            else if (saunaSwitch.IsToggled != false && saunanlampotila != 20 && saveButton.IsVisible != true)
                            {
                                return false;
                            }


                            SaunaStatus.Text = "Sauna on sammutettu ja lämpötila laskee...";
                            SaunaStatus.BackgroundColor = Color.OrangeRed;
                            saunatick = 0;
                            saunanlampotila -= 1;
                            SaunanTemp.Text = "Lämpötila: " + saunanlampotila;                          
                            return true;                           
                        });
                    }

                    
               }

            // Button to START the chosen sauna -- Button is only visible when sauna is switched on. (Controlled in IF - statements above) 
            saveButton = new Button { Text = "Käynnistä sauna" };
            saveButton.Style = (Style)Application.Current.Resources["buttonStyle"];
            saveButton.IsVisible = false;        
            saveButton.Clicked += async (sender, e) =>

            {

                SaunaStatus.Text = "Sauna lämpenee... ";
                SaunaStatus.BackgroundColor = Color.YellowGreen;
                saveButton.IsVisible = false;

                //Timer to increase the temperature of chosen sauna when turned ON 
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    // Do something                   
                    if (saunanlampotila >= saunaTemp.Value)
                    {
                        SaunaStatus.Text = "Sauna on lämmin!";
                        SaunaStatus.BackgroundColor = Color.Green;
                        return false;
                    }
                    else if (saunaSwitch.IsToggled != true)
                    {                       
                        return false;
                    }

                    saunatick += 1;
                    //saunanlampotila increment, because there is no true value available -- saunatick counts the time it takes for the sauna to be in the desired temperature
                    saunanlampotila += 1;
                    SaunanTemp.Text = "Lämpötila: " + saunanlampotila + ". Aikaa kulunut: " + saunatick;
                    return true;               
                });

            };
            
            // Label that shows the state of chosen sauna
            SaunaStatus = new Label();
            if (saunaSwitch.IsToggled == false)
            {
                SaunaStatus = new Label();
                SaunaStatus.HeightRequest = 100;
                SaunaStatus.WidthRequest = 150;
                SaunaStatus.Text = "Sauna on pois päältä.";
                SaunaStatus.HorizontalTextAlignment = TextAlignment.Center;
                SaunaStatus.VerticalTextAlignment = TextAlignment.Center;
                SaunaStatus.FontAttributes = FontAttributes.Bold;
                SaunaStatus.BackgroundColor = Color.Red;
            }
            else if (saunaSwitch.IsToggled == true)
            {
                SaunaStatus.Text = "Sauna on päällä!";
                SaunaStatus.BackgroundColor = Color.LightYellow;
            }
      
            // Content of the page created here
            Content = new StackLayout

            {

                Margin = new Thickness(20),

                VerticalOptions = LayoutOptions.StartAndExpand,

                Children =

                {

                    new Label { Text = "Nimi" },

                    nameEntry,

                    new Label { Text = "Sauna päälle / pois päältä" },

                    saunaSwitch,

                    new Label { Text = "Saunan lämpötila", },

                    saunaTemp,

                    saveButton,

                    saveSauna,

                    deleteButton,

                    SaunaStatus,

                    reqTemp,

                    SaunanTemp,
                  
                }

            };
        }
       
    }
}