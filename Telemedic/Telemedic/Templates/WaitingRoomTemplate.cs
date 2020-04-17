using System;
using System.Collections.Generic;
using System.Text;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using Telemedic.Templates.Enums;

namespace Telemedic.Templates
{
    

    static class WaitingRoomTemplate
    {

        public static Grid WaitingTemplate01(int ID,int AppointmentID,String PatientName, Status CurrentStatus,String BookingTime,String Description,ImageSource UserImageAddress)
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

            Grid ParentGrid = new Grid { HeightRequest = 75, HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(5, 0) };

            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(75) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.6, GridUnitType.Star) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.2, GridUnitType.Star) });

            TapGestureRecognizer ParentGridTapped = new TapGestureRecognizer();
            ParentGridTapped.Tapped += delegate 
            {
                App.Current.MainPage.Navigation.PushAsync(new DetailedWaitingPatient(ID, PatientName,BookingTime,Description, UserImageAddress));
            };

            ParentGrid.GestureRecognizers.Add(ParentGridTapped);

            CircleImage UserImage = new CircleImage
            {
                //ImageSource.FromUri(new Uri(ImageAddress))
                //Utilities.Source("doc_anim.jpg", typeof(WaitingRoomTemplate))
                Source = UserImageAddress,
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            Grid.SetColumn(UserImage,0);


            Label PatientNameLabel = new Label
            {
                Text = PatientName,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 18,
            };
            Grid.SetColumn(PatientNameLabel, 1);


            Label StatusLabel = new Label
            {
                Text = Enum.GetName(typeof(Status), CurrentStatus),
                TextColor = StatusTextColor,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 18,
            };
            Grid.SetColumn(StatusLabel, 2);


            BoxView BottomLine = new BoxView
            {
                BackgroundColor = (Color)App.Current.Resources["_MedAppAsh"],
                HeightRequest = 1,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End
                //Style = App.Current.Resources["_BoxViewBottomLine"] as Style
            };

            ParentGrid.Children.Add(UserImage);
            ParentGrid.Children.Add(PatientNameLabel);
            ParentGrid.Children.Add(StatusLabel);
            ParentGrid.Children.Add(BottomLine);

            return ParentGrid;
        }
        /**
         * 
         * **/
        public static StackLayout WaitingTemplate02(int ID, int AppointmentID, String PatientName, Status CurrentStatus, String BookingTime, String Description, ImageSource UserImageAddress,String PhoneNo)
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

            Grid ParentGrid = new Grid { HeightRequest = 75, HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(5,0) };

            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(75) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.5, GridUnitType.Star) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.3, GridUnitType.Star) });

            TapGestureRecognizer ParentGridTapped = new TapGestureRecognizer();
            ParentGridTapped.Tapped += delegate
            {
                App.Current.MainPage.Navigation.PushAsync(new DetailedWaitingPatient(ID, AppointmentID, PatientName, BookingTime, Description, UserImageAddress, CurrentStatus, PhoneNo));
            };

            ParentGrid.GestureRecognizers.Add(ParentGridTapped);

            CircleImage UserImage = new CircleImage
            {
                //ImageSource.FromUri(new Uri(ImageAddress))
                //Utilities.Source("doc_anim.jpg", typeof(WaitingRoomTemplate))
                Source = UserImageAddress,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            Grid.SetColumn(UserImage, 0);


            Label PatientNameLabel = new Label
            {
                Text = PatientName,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 16,
            };
            Grid.SetColumn(PatientNameLabel, 1);


            Label StatusLabel = new Label
            {
                Text = Enum.GetName(typeof(Status), CurrentStatus),
                TextColor = StatusTextColor,
                HorizontalTextAlignment = TextAlignment.End,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 14,
            };
            Grid.SetColumn(StatusLabel, 2);


            BoxView BottomLine = new BoxView
            {
                BackgroundColor = (Color.White),
                HeightRequest = 1,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End
                //Style = App.Current.Resources["_BoxViewBottomLine"] as Style
            };

            ParentGrid.Children.Add(UserImage);
            ParentGrid.Children.Add(PatientNameLabel);
            ParentGrid.Children.Add(StatusLabel);

            StackLayout AllStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0
            };

            AllStack.Children.Add(ParentGrid);
            AllStack.Children.Add(BottomLine);

            return AllStack;
        }
    }
}
