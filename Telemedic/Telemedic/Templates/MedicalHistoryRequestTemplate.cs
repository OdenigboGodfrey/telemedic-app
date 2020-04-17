using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Telemedic.Templates
{
    static class MedicalHistoryRequestTemplate
    {
        public static Frame RequestTemplate01(int ID,String Name)
        {
            Frame ParentFrame = new Frame
            {
                Style = (Style)App.Current.Resources["_FrameContainer"],
                Margin = new Thickness(5),
                Padding = 5,
                CornerRadius = 0
            };

            Grid ParentGrid = new Grid
            {
                Padding = 5
            };

            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.5,GridUnitType.Star) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.5, GridUnitType.Star) });

            Label MedPractName = new Label
            {
                Text = Name,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            };
            Grid.SetColumn(MedPractName,0);

            Button Accept = new Button
            {
                Text = "Accept",
                TextColor = Color.White,
                BackgroundColor = (Color)App.Current.Resources["colorGreen"],
                HorizontalOptions = LayoutOptions.End,
                CornerRadius = 5,
                WidthRequest = 60
            };
            Grid.SetColumn(Accept, 1);

            Button Decline = new Button
            {
                Text = "Decline",
                TextColor = Color.White,
                BackgroundColor = (Color)App.Current.Resources["colorRed"],
                HorizontalOptions = LayoutOptions.End,
                CornerRadius = 5,
                WidthRequest = 60
            };
            Grid.SetColumn(Decline, 2);

            ParentGrid.Children.Add(MedPractName);
            ParentGrid.Children.Add(Accept);
            ParentGrid.Children.Add(Decline);

            ParentFrame.Content = ParentGrid;

            return ParentFrame;
        }

        public static StackLayout RequestTemplate02(int ID, String Name)
        {
            Grid ParentGrid = new Grid
            {
                Padding = 5
            };

            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.5, GridUnitType.Star) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.5, GridUnitType.Star) });

            Label MedPractName = new Label
            {
                Text = Name,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            };
            Grid.SetColumn(MedPractName, 0);

            Button Accept = new Button
            {
                Text = "Accept",
                TextColor = Color.White,
                BackgroundColor = (Color)App.Current.Resources["colorGreen"],
                HorizontalOptions = LayoutOptions.End,
                CornerRadius = 5,
                WidthRequest = 120
            };
            Grid.SetColumn(Accept, 1);

            Button Decline = new Button
            {
                Text = "Decline",
                TextColor = Color.White,
                BackgroundColor = (Color)App.Current.Resources["colorRed"],
                HorizontalOptions = LayoutOptions.End,
                CornerRadius = 5,
                WidthRequest = 120
            };
            Grid.SetColumn(Decline, 2);

            ParentGrid.Children.Add(MedPractName);
            ParentGrid.Children.Add(Accept);
            ParentGrid.Children.Add(Decline);


            StackLayout AllStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0,
                Margin = new Thickness(5),
                Padding = 5,
            };

            AllStack.Children.Add(ParentGrid);
            AllStack.Children.Add(new BoxView { Style = App.Current.Resources["_BoxViewBottomLine"] as Style, BackgroundColor = (Color.White) });

            return AllStack;
        }
    }
}
