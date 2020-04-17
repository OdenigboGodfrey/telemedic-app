using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Telemedic.Templates;
using Telemedic.Templates.Enums;

namespace Telemedic.Templates
{
    static class AppointmentHistoryTemplate
    {

        /**
         * summary TemplateForUser01 creates a grid template for users with the 'More' button at the right and the info at the left
         * **/
        public static Grid TemplateForUser01(int ID,HistoryType Type,String MedPractName,String Address)
        {
            String HistoryTypeText = "";
            switch (Type)
            {
                case HistoryType.Doctor:
                    HistoryTypeText = "Doctor";
                    break;
                case HistoryType.Hospital:
                    HistoryTypeText = "Hospital";
                    break;
                case HistoryType.Laboratory:
                    HistoryTypeText = "Laboratory";
                    break;
                case HistoryType.Pharmacy:
                    HistoryTypeText = "Pharmacy";
                    break;
            };
            Grid ParentGrid = new Grid { Padding = 5 };

            ParentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(.2, GridUnitType.Star) });
            ParentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(.8, GridUnitType.Star) });

            Grid Grid0 = new Grid();
            Grid.SetRow(Grid0, 0);

            Label HistoryTypeLabel = new Label
            {
                Text = HistoryTypeText,
                TextColor = (Color)App.Current.Resources["_MedAppLightBlue"],
                FontAttributes = FontAttributes.Bold,
                FontSize = 16,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            BoxView Line = new BoxView
            {
                BackgroundColor = (Color)App.Current.Resources["_MedAppAsh"],
                WidthRequest = 1,
                HeightRequest = 1,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End
            };

            Grid0.Children.Add(HistoryTypeLabel);
            Grid0.Children.Add(Line);

            Grid Grid1 = new Grid();
            Grid.SetRow(Grid1,1);

            Grid1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.7, GridUnitType.Star) });
            Grid1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.3, GridUnitType.Star) });

            Label AppointmentInfo = new Label
            {
                Text = MedPractName + "\n" + Address,
                HorizontalTextAlignment = TextAlignment.Start
            };
            Grid.SetColumn(AppointmentInfo,0);

            Button More = new Button
            {
                Text = "more",
                BackgroundColor = (Color)App.Current.Resources["_MedAppLightBlue"],
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                WidthRequest = 80
            };
            Grid.SetColumn(More,1);

            Grid1.Children.Add(AppointmentInfo);
            Grid1.Children.Add(More);

            ParentGrid.Children.Add(Grid0);
            ParentGrid.Children.Add(Grid1);

            return ParentGrid;
        }

        public static Frame TemplateForDoctor01(int ID, int AppointmentID, String UserName, String Address,String AppointmentTime, Status CurrentStatus,String Description,String PhoneNo)
        {
            Color StatusTextColor = (Color)App.Current.Resources["colorGreen"];
            switch (CurrentStatus)
            {
                case Status.Declined:
                    StatusTextColor = (Color)App.Current.Resources["colorRed"];
                    break;
                case Status.Waiting:
                    StatusTextColor = (Color)App.Current.Resources["colorGold"];
                    break;
                default:
                    StatusTextColor = (Color)App.Current.Resources["colorGreen"];
                    break;
            }

            Frame ParentFrame = new Frame
            {
                HasShadow = true,
                BorderColor = (Color)App.Current.Resources["_MedAppAsh"],
                CornerRadius = 0,
                Padding = 0,
                Margin = 5,
                HeightRequest = 50
            };


            TapGestureRecognizer ParentFrameTapped = new TapGestureRecognizer();
            ParentFrameTapped.Tapped += delegate 
            {
                App.Current.MainPage.Navigation.PushAsync(new DetailedWaitingPatient(ID, AppointmentID ,UserName,AppointmentTime,Description,Utilities.Source("IMG_5204.JPG",typeof(AppointmentHistoryTemplate)),CurrentStatus,PhoneNo));
            };

            if (CurrentStatus == Status.Waiting) ParentFrame.GestureRecognizers.Add(ParentFrameTapped);
            

            Grid ParentGrid = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Label NameLabel = new Label
            {
                Text = UserName,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            Grid.SetColumn(NameLabel,0);


            Label TimeLabel = new Label
            {
                Text = AppointmentTime,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            Grid.SetColumn(TimeLabel, 1);

            Label StatusLabel = new Label
            {
                Text = Enum.GetName(typeof(Status),CurrentStatus),
                TextColor = StatusTextColor,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            Grid.SetColumn(StatusLabel, 2);

            ParentGrid.Children.Add(NameLabel);
            ParentGrid.Children.Add(TimeLabel);
            ParentGrid.Children.Add(StatusLabel);

            ParentFrame.Content = ParentGrid;

            return ParentFrame;
        }

        public static StackLayout TemplateForDoctor02(int ID, int AppointmentID , String UserName, String Address, String AppointmentTime, Status CurrentStatus, String Description,String PhoneNo)
        {
            Color StatusTextColor = (Color)App.Current.Resources["colorGreen"];
            switch (CurrentStatus)
            {
                case Status.Declined:
                    StatusTextColor = (Color)App.Current.Resources["colorRed"];
                    break;
                case Status.Waiting:
                    StatusTextColor = (Color)App.Current.Resources["colorGold"];
                    break;
                default:
                    StatusTextColor = (Color)App.Current.Resources["colorGreen"];
                    break;
            }
            

            Grid ParentGrid = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 50
            };

            TapGestureRecognizer ParentGridTapped = new TapGestureRecognizer();
            ParentGridTapped.Tapped += delegate
            {
                App.Current.MainPage.Navigation.PushAsync(new DetailedWaitingPatient(ID, AppointmentID , UserName, AppointmentTime, Description, Utilities.Source("IMG_5204.JPG", typeof(AppointmentHistoryTemplate)), CurrentStatus,PhoneNo));
            };

            if (CurrentStatus == Status.Waiting) ParentGrid.GestureRecognizers.Add(ParentGridTapped);

            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Label NameLabel = new Label
            {
                Text = UserName,
                FontSize = 18,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            Grid.SetColumn(NameLabel, 0);


            Label TimeLabel = new Label
            {
                Text = AppointmentTime,
                FontSize = 16,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            Grid.SetColumn(TimeLabel, 1);

            Label StatusLabel = new Label
            {
                Text = Enum.GetName(typeof(Status), CurrentStatus),
                TextColor = StatusTextColor,
                FontSize = 16,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            Grid.SetColumn(StatusLabel, 2);

            ParentGrid.Children.Add(NameLabel);
            ParentGrid.Children.Add(TimeLabel);
            ParentGrid.Children.Add(StatusLabel);

            StackLayout AllStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0
            };

            AllStack.Children.Add(ParentGrid);
            AllStack.Children.Add(new BoxView { BackgroundColor = (Color.White), Style = App.Current.Resources["_BoxViewBottomLine"] as Style });

            return AllStack;
        }
    }
}
