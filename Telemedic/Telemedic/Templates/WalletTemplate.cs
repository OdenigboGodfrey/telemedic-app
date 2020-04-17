using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Telemedic.Templates.Enums;

namespace Telemedic.Templates
{
    static class WalletTemplate
    {
        
        public static Grid TransactionTemplate01(String Amount, TransactionType Type,String TransactionInfo)
        {
            String TransactionTypeLabelText = Enum.GetName(typeof(TransactionType),Type);

            Grid ParentGrid = new Grid();
            /** col def for the transactions **/
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Button AmountButton = new Button
            {
                Text = Amount,
                TextColor = Color.Black,
                CornerRadius = 8,
                BackgroundColor = Color.Transparent

            };

            AmountButton.Clicked += delegate 
            {
                App.Current.MainPage.DisplayAlert("Trasaction Details",TransactionInfo,"Okay");
                
                //show tranaction info here
            };

            Grid.SetColumn(AmountButton, 0);

            Label TransactionTypeLabel = new Label
            {
                Text = TransactionTypeLabelText,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            switch (Type)
            {
                case TransactionType.Credit:
                    TransactionTypeLabel.TextColor = (Color)Application.Current.Resources["colorGreen"];
                    break;
                case TransactionType.Debit:
                    TransactionTypeLabel.TextColor = (Color)Application.Current.Resources["colorRed"];
                    break;
            };

            Grid.SetColumn(TransactionTypeLabel,1);

            ParentGrid.Children.Add(AmountButton);
            ParentGrid.Children.Add(TransactionTypeLabel);

            return ParentGrid;
        }
    }
}
