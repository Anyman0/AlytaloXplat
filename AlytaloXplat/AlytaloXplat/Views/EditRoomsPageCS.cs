using AlytaloXplat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AlytaloXplat.Views
{
	public class EditRoomsPageCS : ContentPage
	{
        public Slider himmennin;
        public Switch valoSwitch;
		public EditRoomsPageCS ()
		{
            BackgroundColor = Color.SaddleBrown;
            

            // Entry fields
            var nameEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, "Nimi");

            var valoEntry = new Entry();
            valoEntry.Keyboard = Keyboard.Numeric;            
            valoEntry.SetBinding(Entry.TextProperty, "Valot");


            // Switch to turn light ON/OFF -- When toggled, sets the default value of dimmer (1-10) to 5
            valoSwitch = new Switch();
            valoSwitch.SetBinding(Switch.IsToggledProperty, "valonTila");
            valoSwitch.Toggled += ValoSwitch_Toggled;
            void ValoSwitch_Toggled(object sender, ToggledEventArgs e)
            {
                himmennin.Value = 5;
            }

            // Sets the dimmer for the chosen light
            himmennin = new Slider();          
            himmennin.Maximum = 10;          
            himmennin.SetBinding(Slider.ValueProperty, "Himmennin");
                                        

            // All the buttons defined here 
            var saveButton = new Button { Text = "Tallenna", Style = (Style)Application.Current.Resources["buttonStyle"] };
            saveButton.Clicked += async (sender, e) =>

            {
                var huone = (HuoneDBModel)BindingContext;
                await App.HDatabase.SaveItemAsync(huone);
                await Navigation.PopAsync();
            };


            var deleteButton = new Button { Text = "Poista", Style = (Style)Application.Current.Resources["buttonStyle"] };
            deleteButton.Clicked += async (sender, e) =>

            {
                var huone = (HuoneDBModel)BindingContext;
                await App.HDatabase.DeleteItemAsync(huone);
                await Navigation.PopAsync();
            };


            var cancelButton = new Button { Text = "Poistu tallentamatta", Style = (Style)Application.Current.Resources["buttonStyle"] };
            cancelButton.Clicked += async (sender, e) =>

            {
                await Navigation.PopAsync();
            };


            // Content of the page created here
            Content = new StackLayout

            {

                Margin = new Thickness(20),

                VerticalOptions = LayoutOptions.StartAndExpand,

                Children =

                {

                    new Label { Text = "Nimi" },

                    nameEntry,

                    new Label { Text = "Valot" },

                    valoEntry,

                    new Label { Text = "Valot päällä / pois" },

                    valoSwitch,

                    new Label { Text = "Himmennin", },

                    himmennin,
                                     
                    saveButton,

                    deleteButton,

                    cancelButton,
                   
                }

            };
        }
        
    }
}