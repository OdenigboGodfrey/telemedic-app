using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Telemedic.Templates.Enums;

namespace Telemedic.Templates
{

    class NotificationTemplate
    {
        
        private static ImageCircle.Forms.Plugin.Abstractions.CircleImage UserImage;
        /**
        * summary CreateNewGridType01 creates the Grid display
        * param name="ReminderType" is the type of reminder 
        * param name="ImageAddress" is the address of the image file
        * param name="Content" is the message content
        * param name="Time" is the time the message was delivered
        * returns return the new Grid
        * **/
        public static StackLayout Notification01(String ImageAddress, String Content, String Time,NotificationType UserNotificationType,int ID)
        {
            String ReminderType = Enum.GetName(typeof(NotificationType),UserNotificationType) ;
            
            Grid ParentGrid = new Grid
            {
                HeightRequest = 75
            };

            /** grid1 should be tappable and should send some info to the chatpage**/
            TapGestureRecognizer ParentGridTapped = new TapGestureRecognizer();
            ParentGridTapped.Tapped +=
                delegate
                {
                    switch (UserNotificationType)
                    {
                        case NotificationType.Chat:
                            App.Current.MainPage.Navigation.PushAsync(new ChatPage(ID,Utilities.IsMedic));
                            break;
                        case NotificationType.Reminder:
                            //send to reminder here
                            break;
                        case NotificationType.Appointment:
                            App.Current.MainPage.Navigation.PushAsync(new ChatPage(ID, Utilities.IsMedic));
                            break;
                    };
                };

            ParentGrid.GestureRecognizers.Add(ParentGridTapped);

            ColumnDefinition ColDef0 = new ColumnDefinition
            {
                Width = new GridLength(0.5, GridUnitType.Star)
            };

            ColumnDefinition ColDef1 = new ColumnDefinition
            {
                Width = new GridLength(1.5, GridUnitType.Star)
            };

            ParentGrid.ColumnDefinitions.Add(ColDef0);
            ParentGrid.ColumnDefinitions.Add(ColDef1);

            /** Gird0 holds the image and the online offline status **/
            Grid Grid0 = new Grid();

            
            /** Set the child grid to appear in grid view with index 0 **/
            Grid.SetColumn(Grid0, 0);

            UserImage = new ImageCircle.Forms.Plugin.Abstractions.CircleImage
            {
                Source = ImageSource.FromUri(new Uri("https://www.clipartmax.com/png/middle/258-2582267_circled-user-male-skin-type-1-2-icon-male-user-icon.png")),
                Aspect = Aspect.AspectFit
            };

            Grid0.Children.Add(UserImage);

            /**Grid1 holds the name, time etc**/
            Grid Grid1 = new Grid();


            Grid.SetColumn(Grid1, 1);
            /** Grid1 needs 2 rowdefinintions to separate
             * 1 for the toplevel and the other for the lower level
             * **/
            RowDefinition RowDef0 = new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star)
            };

            RowDefinition RowDef1 = new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star)
            };

            Grid1.RowDefinitions.Add(RowDef0);
            Grid1.RowDefinitions.Add(RowDef1);

            /** inside GRID1 , the top layer GRID is used by the name and time 
             * while LowerLevel holds the message brief content
             * **/
            Grid TopLevel = new Grid();

            Grid.SetRow(TopLevel, 0);
            /** the top level is dived into two sections the name and the time
             * so we need two columndefinitions
             * **/

            TopLevel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            TopLevel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Label ReminderTypeLabel = new Label
            {
                Text = ReminderType,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = (Color)App.Current.Resources["_MedAppLightBlue"],
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            Grid.SetColumn(ReminderTypeLabel, 0);

            Label TimeLabel = new Label
            {
                Text = Time,
                FontSize = 10,
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            Grid.SetColumn(TimeLabel, 1);

            TopLevel.Children.Add(ReminderTypeLabel);
            TopLevel.Children.Add(TimeLabel);

            /**thw lowerlevel is made of just a label of content**/

            Label ContentLabel = new Label
            {
                Text =  Content,
                FontSize = 16
            };

            Grid.SetRow(ContentLabel, 1);

            Grid1.Children.Add(TopLevel);
            Grid1.Children.Add(ContentLabel);


            ParentGrid.Children.Add(Grid0);
            ParentGrid.Children.Add(Grid1);

            StackLayout AllStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0
            };

            AllStack.Children.Add(ParentGrid);
            AllStack.Children.Add(new BoxView { Style = App.Current.Resources["_BoxViewBottomLine"] as Style, BackgroundColor = Color.White });

            return AllStack;
        }
    }
}
