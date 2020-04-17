using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace Telemedic.Templates
{
    static class ListAllChatTemplate
    {
        private static ImageCircle.Forms.Plugin.Abstractions.CircleImage UserImage;
        /**
        * summary CreateNewGridType01 creates the Grid display
        * param name="SenderName" is the user's name
        * param name="ImageAddress" is the address of the image file
        * param name="Content" is the message content
        * param name="Time" is the time the message was delivered
        * returns return the new Grid
        * **/

        
        public static Grid CreateNewGridType01(String SenderName,String ImageAddress,String Content,String Time, ContentPage con,int ID,int NewMessageCount)
        {
            
            Grid ParentGrid = new Grid
            {
                HeightRequest = 75
            };
            
            

            ColumnDefinition ColDef0 = new ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            };

            ColumnDefinition ColDef1 = new ColumnDefinition
            {
                Width = new GridLength(1.7, GridUnitType.Star)
            };

            ParentGrid.ColumnDefinitions.Add(ColDef0);
            ParentGrid.ColumnDefinitions.Add(ColDef1);

            TapGestureRecognizer ParentGridTapped = new TapGestureRecognizer();
            ParentGridTapped.Tapped += delegate 
            {
                if (((AbsoluteLayout)con.FindByName("_UserPopUpGrid")).IsVisible)
                {
                    ((AbsoluteLayout)con.FindByName("_UserPopUpGrid")).IsVisible = false;
                }
            };

            ParentGrid.GestureRecognizers.Add(ParentGridTapped);

            /** Gird0 holds the image and the online offline status **/
            Grid Grid0 = new Grid();

            /** grid1 should be tappable and should send some info to the chatpage**/
            TapGestureRecognizer Grid0Tapped = new TapGestureRecognizer();
            Grid0Tapped.Tapped += delegate
            {
                ((AbsoluteLayout)con.FindByName("_UserPopUpGrid")).IsVisible = true;
                //((AbsoluteLayout)con.FindByName("_UserPopUpGrid")).StyleId = ReceiversID.ToString();
                //((Image)con.FindByName("_PopUpUserImage")).Source = (Source == null) ? Utilities.Source(ImageAddress,typeof(ListAllChatTemplate)): Source;
                ((Image)con.FindByName("_PopUpUserImage")).Source = UserImage.Source;

                /** UserInfoArray holds the information which would be tagged to the control **/
                var UserInfoArray = new Dictionary<String,object>
                {
                    {"ReceiversID", ID},
                    { "Name", SenderName},
                    { "Image",UserImage.Source }
                };

                ControlTagger<Object>.SetTag(((Image)con.FindByName("_PopUpUserImage")), UserInfoArray);
                
                //App.Current.MainPage.Navigation.PushAsync(new ChatPage());
                
            };

            Grid0.GestureRecognizers.Add(Grid0Tapped);
            /** Set the child grid to appear in grid view with index 0 **/
            Grid.SetColumn(Grid0, 0);

            UserImage = new ImageCircle.Forms.Plugin.Abstractions.CircleImage
            {
                //ImageSource.FromUri(new Uri("https://www.clipartmax.com/png/middle/258-2582267_circled-user-male-skin-type-1-2-icon-male-user-icon.png"))
                //
                Source = Utilities.Source("IMG_5204.JPG", typeof(ListAllChatTemplate)),
                Aspect = Aspect.AspectFit,
            };

            Label OnlineOffline = new Label
            {
                Text = "ONLINE",
                TextColor = Color.Green,
                FontAttributes = FontAttributes.Bold,
                FontSize = 12,
                VerticalOptions = LayoutOptions.End,
                VerticalTextAlignment = TextAlignment.End,
                HorizontalTextAlignment = TextAlignment.Center
            };

            Grid0.Children.Add(UserImage);
            //Grid0.Children.Add(OnlineOffline);

            /**Grid1 holds the name, time etc**/
            Grid Grid1 = new Grid { Margin = new Thickness(3,0,0,0) };

            /** grid1 should be tappable and should send some info to the chatpage**/
            TapGestureRecognizer Grid1Tapped = new TapGestureRecognizer();
            Grid1Tapped.Tapped += delegate
            {
                if (((AbsoluteLayout)con.FindByName("_UserPopUpGrid")).IsVisible)
                {
                    ((AbsoluteLayout)con.FindByName("_UserPopUpGrid")).IsVisible = false;
                }
                else
                {
                    App.Current.MainPage.Navigation.PushAsync(new ChatPage(ID,true,SenderName));
                }
            };

            Grid1.GestureRecognizers.Add(Grid1Tapped);

            Grid.SetColumn(Grid1, 1);
            /** Grid1 needs 2 rowdefinintions to separate
             * 1 for the toplevel and the other for the lower level
             * **/
            RowDefinition RowDef0 = new RowDefinition
            {
                Height = new GridLength(.3, GridUnitType.Star)
            };

            RowDefinition RowDef1 = new RowDefinition
            {
                Height = new GridLength(.7, GridUnitType.Star)
            };

            Grid1.RowDefinitions.Add(RowDef0);
            Grid1.RowDefinitions.Add(RowDef1);

            /** inside GRID1 , the top layer GRID is used by the name and time 
             * while LowerLevel holds the message brief content
             * **/
            Grid TopLevel = new Grid();

            Grid.SetRow(TopLevel,0);
            /** the top level is dived into two sections the name and the time
             * so we need two columndefinitions
             * **/

            TopLevel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            TopLevel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Label NameLabel = new Label
            {
                Text = SenderName,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Start,
                
            };

            Grid.SetColumn(NameLabel,0);
            
            Label TimeLabel = new Label
            {
                Text = Time,
                FontSize = 10,
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            Grid.SetColumn(TimeLabel, 1);

            TopLevel.Children.Add(NameLabel);
            TopLevel.Children.Add(TimeLabel);

            /**the bottomlevel is made of  a label of content and the new message counter**/
            Grid BottomLevel = new Grid();

            Grid.SetRow(BottomLevel, 1);

            BottomLevel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.7, GridUnitType.Star) });
            BottomLevel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.3, GridUnitType.Star) });

            Label ContentLabel = new Label
            {
                Text = (Content.Length > 40) ? Content.Substring(0, 40) + "..." : Content,
                VerticalTextAlignment = TextAlignment.Start
            };

            Label MessageCounterLabel = new Label
            {
                Text = (NewMessageCount == 0) ? "" : NewMessageCount.ToString(),
                FontSize = 10,
                HorizontalTextAlignment = TextAlignment.End
            };

            Grid.SetColumn(ContentLabel, 0);
            Grid.SetColumn(MessageCounterLabel, 1);

            BottomLevel.Children.Add(ContentLabel);
            BottomLevel.Children.Add(MessageCounterLabel);


            Grid1.Children.Add(TopLevel);
            Grid1.Children.Add(BottomLevel);


            ParentGrid.Children.Add(Grid0);
            ParentGrid.Children.Add(Grid1);
            ParentGrid.Children.Add(new BoxView { Style = App.Current.Resources["_BoxViewBottomLine"] as Style, BackgroundColor = (Color)App.Current.Resources["_MedAppAsh"] });
            return ParentGrid;
        }

        /**
        * summary CreateNewGridType01 creates the Grid display
        * param name="SenderName" is the user's name
        * param name="ImageAddress" is the address of the image file
        * param name="Content" is the message content
        * param name="Time" is the time the message was delivered
        * returns return the new Grid
        * **/


        public static StackLayout CreateNewStackType01(String SenderName, String ImageAddress, String Content, String Time, ContentPage con, int ReceiversID, int NewMessageCount,int MedicID)
        {

            Grid ParentGrid = new Grid
            {
                HeightRequest = 75
            };



            ColumnDefinition ColDef0 = new ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            };

            ColumnDefinition ColDef1 = new ColumnDefinition
            {
                Width = new GridLength(1.7, GridUnitType.Star)
            };

            ParentGrid.ColumnDefinitions.Add(ColDef0);
            ParentGrid.ColumnDefinitions.Add(ColDef1);


            TapGestureRecognizer ParentGridTapped = new TapGestureRecognizer();
            ParentGridTapped.Tapped += delegate
            {
                if (((AbsoluteLayout)con.FindByName("_UserPopUpGrid")).IsVisible)
                {
                    ((AbsoluteLayout)con.FindByName("_UserPopUpGrid")).IsVisible = false;
                }
            };

            ParentGrid.GestureRecognizers.Add(ParentGridTapped);

            /** Gird0 holds the image and the online offline status **/
            Grid Grid0 = new Grid();

            /** grid1 should be tappable and should send some info to the chatpage**/
            TapGestureRecognizer Grid0Tapped = new TapGestureRecognizer();
            Grid0Tapped.Tapped += delegate
            {
                ((AbsoluteLayout)con.FindByName("_UserPopUpGrid")).IsVisible = true;
                //((AbsoluteLayout)con.FindByName("_UserPopUpGrid")).StyleId = ReceiversID.ToString();
                //((Image)con.FindByName("_PopUpUserImage")).Source = (Source == null) ? Utilities.Source(ImageAddress,typeof(ListAllChatTemplate)): Source;
                ((Image)con.FindByName("_PopUpUserImage")).Source = UserImage.Source;

                /** UserInfoArray holds the information which would be tagged to the control **/
                var UserInfoArray = new Dictionary<String, object>
                {
                    {"ReceiversID", ReceiversID},
                    { "Name", SenderName},
                    { "Image",UserImage.Source },
                    { "MedicID",MedicID }
                };

                ControlTagger<Object>.SetTag(((Image)con.FindByName("_PopUpUserImage")), UserInfoArray);

                //App.Current.MainPage.Navigation.PushAsync(new ChatPage());

            };

            Grid0.GestureRecognizers.Add(Grid0Tapped);
            /** Set the child grid to appear in grid view with index 0 **/
            Grid.SetColumn(Grid0, 0);

            UserImage = new ImageCircle.Forms.Plugin.Abstractions.CircleImage
            {
                //ImageSource.FromUri(new Uri("https://www.clipartmax.com/png/middle/258-2582267_circled-user-male-skin-type-1-2-icon-male-user-icon.png"))
                //
                Source = Utilities.Source("IMG_5204.JPG", typeof(ListAllChatTemplate)),
                Aspect = Aspect.AspectFit,
            };

            Label OnlineOffline = new Label
            {
                Text = "ONLINE",
                TextColor = Color.Green,
                FontAttributes = FontAttributes.Bold,
                FontSize = 12,
                VerticalOptions = LayoutOptions.End,
                VerticalTextAlignment = TextAlignment.End,
                HorizontalTextAlignment = TextAlignment.Center,
                IsVisible = false
            };

            Grid0.Children.Add(UserImage);
            //Grid0.Children.Add(OnlineOffline);

            /**Grid1 holds the name, time etc**/
            Grid Grid1 = new Grid();

            /** grid1 should be tappable and should send some info to the chatpage**/
            TapGestureRecognizer Grid1Tapped = new TapGestureRecognizer();
            Grid1Tapped.Tapped += delegate
            {
                if (((AbsoluteLayout)con.FindByName("_UserPopUpGrid")).IsVisible)
                {
                    ((AbsoluteLayout)con.FindByName("_UserPopUpGrid")).IsVisible = false;
                }
                else
                {
                    App.Current.MainPage.Navigation.PushAsync(new ChatPage(ReceiversID, Utilities.IsMedic,SenderName,MedicID));
                }
            };

            Grid1.GestureRecognizers.Add(Grid1Tapped);

            Grid.SetColumn(Grid1, 1);
            /** Grid1 needs 2 rowdefinintions to separate
             * 1 for the toplevel and the other for the lower level
             * **/
            RowDefinition RowDef0 = new RowDefinition
            {
                Height = new GridLength(.3, GridUnitType.Star)
            };

            RowDefinition RowDef1 = new RowDefinition
            {
                Height = new GridLength(.7, GridUnitType.Star)
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

            Label NameLabel = new Label
            {
                Text = SenderName,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Start,

            };

            Grid.SetColumn(NameLabel, 0);

            Label TimeLabel = new Label
            {
                Text = Time,
                FontSize = 10,
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            Grid.SetColumn(TimeLabel, 1);

            TopLevel.Children.Add(NameLabel);
            TopLevel.Children.Add(TimeLabel);

            /**the bottomlevel is made of  a label of content and the new message counter**/
            Grid BottomLevel = new Grid();

            Grid.SetRow(BottomLevel, 1);

            BottomLevel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.7, GridUnitType.Star) });
            BottomLevel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.3, GridUnitType.Star) });

            Label ContentLabel = new Label
            {
                Text = (Content.Length > 40) ? Content.Substring(0, 40) + "..." : Content,
                VerticalTextAlignment = TextAlignment.Start,
            };
            
            Label MessageCounterLabel = new Label
            {
                Text = (NewMessageCount == 0) ? "" : NewMessageCount.ToString(),
                FontSize = 10,
                HorizontalTextAlignment = TextAlignment.End
            };

            Grid.SetColumn(ContentLabel, 0);
            Grid.SetColumn(MessageCounterLabel, 1);

            BottomLevel.Children.Add(ContentLabel);
            BottomLevel.Children.Add(MessageCounterLabel);


            Grid1.Children.Add(TopLevel);
            Grid1.Children.Add(BottomLevel);


            ParentGrid.Children.Add(Grid0);
            ParentGrid.Children.Add(Grid1);

            //
            StackLayout AllStack = new StackLayout { Spacing = 0 };

            AllStack.Children.Add(ParentGrid);
            AllStack.Children.Add(new BoxView { Style = App.Current.Resources["_BoxViewBottomLine"] as Style, BackgroundColor = (Color.White) });

            return AllStack;
        }
    }
}
