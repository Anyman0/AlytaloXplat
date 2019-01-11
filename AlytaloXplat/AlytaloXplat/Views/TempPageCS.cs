using AlytaloXplat.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AlytaloXplat.Views
{
	public class TempPageCS : ContentPage
	{
        public ListView tempList;
        public Label tempInfo;
		public TempPageCS ()
		{

            // list of the saved temperature / humidity values
            tempList = new ListView
            {

                Margin = new Thickness(5),
                RowHeight = 120,
                ItemTemplate = new DataTemplate(() =>

                {

                    var housetemp = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    };

                    housetemp.Text = "Talon lämpötila--C";

                    var label = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    };

                    label.SetBinding(Label.TextProperty, "TalonLämpötila");

                    var kosteuslabel = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    };

                    kosteuslabel.Text = "Kosteus %--";


                    var kosteusproslabel = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    };

                    kosteusproslabel.SetBinding(Label.TextProperty, "Kosteusprosentti");

                    var dateLabel = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        FontAttributes = FontAttributes.Bold,
                        
                    };

                    dateLabel.SetBinding(Label.TextProperty, "Päivämäärä");

                    // Layout for list items
                    var stackLayout = new StackLayout

                    {

                        Margin = new Thickness(0, 0, 0, 0),
                        BackgroundColor = Color.GreenYellow,
                        Orientation = StackOrientation.Vertical,                      
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        
                        Children = { housetemp, label, kosteuslabel, kosteusproslabel, dateLabel }
                        
                    };


                    return new ViewCell { View = stackLayout };

                })
            };

            // header label
            tempInfo = new Label();
            tempInfo.Text = "Tässä näet lämpötilasi seurannan";
            tempInfo.FontSize = 20;
            tempInfo.FontAttributes = FontAttributes.Italic;
            tempInfo.FontAttributes = FontAttributes.Bold;
            tempInfo.HorizontalTextAlignment = TextAlignment.Center;


            var savedate = new DateTime();
            savedate = DateTime.Now;

            var datenow = new Label();
            datenow.FontSize = 20;
            datenow.FontAttributes = FontAttributes.Italic;           
            datenow.Text = savedate.ToString();

           
            var temperature = new Label();
            temperature.Text = "Talon lämpötila: ";
            temperature.FontSize = 20;
            temperature.FontAttributes = FontAttributes.Bold;

            var kosteusprosenttilabel = new Label();
            kosteusprosenttilabel.Text = "Kosteusprosentti: ";
            kosteusprosenttilabel.FontSize = 20;
            kosteusprosenttilabel.FontAttributes = FontAttributes.Bold;
                             
            // sliders for choosing the temperature / humidity values, as real values not available
            var tempSlider = new Slider();
            tempSlider.Maximum = 30;
            tempSlider.SetBinding(Slider.ValueProperty, "TalonLämpötila");
            tempSlider.ValueChanged += TempSlider_ValueChanged;
            void TempSlider_ValueChanged(object sender, ValueChangedEventArgs e)
            {
                temperature.Text = "Talon lämpötila: " + e.NewValue;
            }

            var kosteusSlider = new Slider();
            kosteusSlider.Maximum = 80;
            kosteusSlider.Minimum = 15;
            kosteusSlider.SetBinding(Slider.ValueProperty, "Kosteusprosentti");
            kosteusSlider.ValueChanged += SaunatempSlider_ValueChanged;
            void SaunatempSlider_ValueChanged(object sender, ValueChangedEventArgs e)
            {
                kosteusprosenttilabel.Text = "Kosteusprosentti: " + e.NewValue;
            }

            // Buttons for saving the values / resetting the list defined here
            var saveTemp = new Button();
            saveTemp.Style = (Style)Application.Current.Resources["buttonStyle"];
            saveTemp.Text = "Tallenna arvot seurantaan";
            saveTemp.Clicked += async (sender, e) =>
            {                
                var temp = (TempDBModel)BindingContext;               
                temp.Päivämäärä = DateTime.Now;
                await App.TDatabase.SaveTempAsync(temp);
                await Navigation.PopAsync();
            };

            var resetButton = new Button { Text = "Resetoi lista", Style = (Style)Application.Current.Resources["buttonStyle"] };
        
            resetButton.Clicked += async (sender, e) =>
            {
                var resetTemp = (TempDBModel)BindingContext;
                await App.TDatabase.DeleteTempAsync(resetTemp);
                await Navigation.PopAsync();
            };

            // Content of the page defined here
            ScrollView kasa = new ScrollView();
            StackLayout stack = new StackLayout();
            stack.BackgroundColor = Color.SaddleBrown;
            kasa.RaiseChild(kasa);
            stack.Children.Add(tempInfo);
            stack.Children.Add(tempList);
            stack.Children.Add(datenow);
            stack.Children.Add(kosteusprosenttilabel);
            stack.Children.Add(kosteusSlider);
            stack.Children.Add(temperature);
            stack.Children.Add(tempSlider);
            stack.Children.Add(saveTemp);
            stack.Children.Add(resetButton);
            Content = stack;
        }
       
        // itemsource for the list above defined here
        protected override async void OnAppearing()
        {
            base.OnAppearing();
                  
            ((App)App.Current).ResumeAtTempId = -1;
            tempList.ItemsSource = await App.TDatabase.GetTempAsync();           
        }
               
    }
}