using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Telemedic.Templates
{
    static class RecordTemplate
    {
        public static StackLayout RecordTemplate01(int ID, String Title, String Content, bool FolderIconVisible)
        {
            StackLayout ParentStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Margin = 10,
                Padding = 5,
                HeightRequest = 75,
                BackgroundColor = (Color)App.Current.Resources["_MedAppChatBackground"]
            };

            TapGestureRecognizer ParentStackTapped = new TapGestureRecognizer();
            ParentStackTapped.Tapped += delegate
            {
                App.Current.MainPage.Navigation.PushAsync(new ReadRecordPage(0, Content, Title));
            };

            ParentStack.GestureRecognizers.Add(ParentStackTapped);

            Label RecordTitle = new Label
            {
                Text = Title,
                FontSize = 24,
                TextColor = (Color)App.Current.Resources["_MedAppLightBlue"]
            };

            Label RecordContent = new Label
            {
                Text = (Content.Length > 30) ? Content.Substring(0, 30) + "..." : Content
            };

            ImageButton FolderImage = new ImageButton
            {
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 30,
                HeightRequest = 30
            };


            ParentStack.Children.Add(RecordTitle);
            ParentStack.Children.Add(RecordContent);
            if (FolderIconVisible) ParentStack.Children.Add(FolderImage);

            ParentStack.Children.Add(new BoxView { Style = App.Current.Resources["_BoxViewBottomLine"] as Style, BackgroundColor = (Color.White) });

            return ParentStack;
        }

        
    }
}
