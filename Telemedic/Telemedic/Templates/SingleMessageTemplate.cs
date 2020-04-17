using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Telemedic.Templates
{
    static class SingleMessageTemplate
    {
        /**
        * summary CreateMessage01 creates the message display
        * param name="Directions" pass 0 for left and 1 for right which is based on who sebds fronm the db
        * param name="Text" message text to be displayed
        * returns return the new Grid
        * **/
        public static StackLayout CreateMessage01(int Direction, String Text, String Time)
        {
            Grid MessageGrid = new Grid();
            ColumnDefinition ColDef0 = new ColumnDefinition();
            ColumnDefinition ColDef1 = new ColumnDefinition();

            if (Direction == 0)
            {
                ColDef0.Width = new GridLength(.7, GridUnitType.Star);
                ColDef1.Width = new GridLength(.3, GridUnitType.Star);
            }
            else
            {
                ColDef0.Width = new GridLength(.3, GridUnitType.Star);
                ColDef1.Width = new GridLength(.7, GridUnitType.Star);
            }

            MessageGrid.ColumnDefinitions.Add(ColDef0);
            MessageGrid.ColumnDefinitions.Add(ColDef1);
            /**
             * Holding frame which encloses the message label
             * **/
            Frame HoldingFrame = new Frame { CornerRadius = 3, Margin = 3 };
            HoldingFrame.Style = Application.Current.Resources["_MessagesFrame"] as Style;
            HoldingFrame.HorizontalOptions = (Direction == 1 ) ? LayoutOptions.End : LayoutOptions.Start;
            HoldingFrame.BackgroundColor = (Direction == 1) ? (Color)Application.Current.Resources["_MedAppLightBlue"] : (Color)Application.Current.Resources["_MedAppBlack"];
            HoldingFrame.Padding = new Thickness(5,3,5,3);
            Grid.SetColumn(HoldingFrame, Direction);
            /**
             * Create a label to hold the message content
             * **/
            Label MessageText = new Label();
            MessageText.Style = Application.Current.Resources["_MessageLabel"] as Style;
            MessageText.HorizontalOptions = LayoutOptions.Start;
            MessageText.Text = Text;
            MessageText.FontSize = 16;
            MessageText.Margin = (Direction == 1) ? new Thickness(0,0,3,0) : new Thickness(3, 0, 0, 0);

            /**
             * Add the MessageText Label to the HoldingFrame
             * **/
            HoldingFrame.Content = MessageText;
            MessageGrid.Children.Add(HoldingFrame);

            /**
             * create the time label
             * **/
            Label TimeLabel = new Label { Margin = new Thickness(0,1,0,0)};
            TimeLabel.Text = Time;
            TimeLabel.Style = Application.Current.Resources["_TimeLabel"] as Style;
            TimeLabel.HorizontalOptions = (Direction == 1) ? LayoutOptions.End : LayoutOptions.Start;

            StackLayout AllStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0
            };

            AllStack.Children.Add(MessageGrid);
            AllStack.Children.Add(TimeLabel);

            return AllStack;
        }

        //public static Fr

    }
}
