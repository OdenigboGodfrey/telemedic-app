using System;
using System.Collections.Generic;
using System.Text;
using Telemedic.Templates.Enums;
using Xamarin.Forms;

namespace Telemedic.Templates
{
    static class MedPractListTemplate
    {
        private static string deftime = "8:00AM - 4:00PM";

        public static Grid ListTemplate01(int MedPractID, MedicalPractitionerType MPT, String MedPractName, String Description, double Stars, ImageSource MedPractImage, double Distance, String Address, double Fee, String WorkTime)
        {
            Color TypeColour = (Color)App.Current.Resources["colorGreen"];
            String TypeName = Enum.GetName(typeof(MedicalPractitionerType),MPT);

            deftime = (String.IsNullOrEmpty(WorkTime)) ? deftime : WorkTime;

            Page page = new MedPractProfile(new MedPractProfileDetails(MedPractID, MedicalPractitionerType.Doctor, Stars, Fee, deftime, Distance, MedPractImage, MedPractName, Address, Description));

            switch (MPT)
            {
                case MedicalPractitionerType.Hospital:
                    TypeColour = (Color)App.Current.Resources["colorRed"];
                    new MedPractProfile(new MedPractProfileDetails(MedPractID, MedicalPractitionerType.Hospital, Stars, Fee, deftime, Distance, MedPractImage, MedPractName, Address, Description));
                    break;
                case MedicalPractitionerType.Laboratory:
                    TypeColour = (Color)App.Current.Resources["colorGold"];
                    new MedPractProfile(new MedPractProfileDetails(MedPractID, MedicalPractitionerType.Laboratory, Stars, Fee, deftime, Distance, MedPractImage, MedPractName, Address, Description));
                    break;
                case MedicalPractitionerType.Pharmacy:
                    TypeColour = (Color)App.Current.Resources["_MedAppBlack"];
                    new MedPractProfile(new MedPractProfileDetails(MedPractID, MedicalPractitionerType.Pharmacy, Stars, Fee, deftime, Distance, MedPractImage, MedPractName, Address, Description));
                    break;
            };

            Grid ParentGrid = new Grid
            {
                BackgroundColor = (Color)App.Current.Resources["_MedAppLightBlue"],
            };

            

            TapGestureRecognizer ParentGridTapped = new TapGestureRecognizer();
            ParentGridTapped.Tapped += delegate 
            {
                Application.Current.MainPage.Navigation.PushAsync(page);
            };

            ParentGrid.GestureRecognizers.Add(ParentGridTapped);

            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.2, GridUnitType.Star) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.8, GridUnitType.Star) });

            /**Left Stack for the Name, description and etc**/
            StackLayout LeftStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = 5,
            };
            Grid.SetColumn(LeftStack,0);

            Label NameLabel = new Label
            {
                Text = MedPractName,
                FontAttributes = FontAttributes.Bold,
                TextDecorations = TextDecorations.Underline,
                TextColor = Color.White,
                FontSize = 22,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start
            };

            LeftStack.Children.Add(NameLabel);

            Label DescriptionLabel = new Label
            {
                Text = (Description.Length > 150) ? Description.Substring(0, 150) + "..." : Description,
                HeightRequest = 100,
                TextColor = Color.White
            };

            LeftStack.Children.Add(DescriptionLabel);

            LeftStack.Children.Add(new Label
            {
                Text = "Rating",
                TextDecorations = TextDecorations.Underline,
                FontSize = 18,
                HorizontalTextAlignment = TextAlignment.Start,
                TextColor = Color.White
            });

            StackLayout StarsStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };

            Utilities.CreateStars(StarsStack, Stars, typeof(MedPractListTemplate));

            LeftStack.Children.Add(StarsStack);

            /**Right Stack for the image and etc**/
            StackLayout RightStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            Grid.SetColumn(RightStack, 1);

            StackLayout ImageAndTypeStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Image MedPractPicture = new Image
            {
                Source = MedPractImage,
                Aspect = Aspect.AspectFit,
                HeightRequest = 100,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            ImageAndTypeStack.Children.Add(MedPractPicture);

            ImageAndTypeStack.Children.Add(new Label
            {
                Text = TypeName,
                BackgroundColor = TypeColour,
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.FillAndExpand
            });


            RightStack.Children.Add(ImageAndTypeStack);

            RightStack.Children.Add(new Label
            {
                Text = "Location",
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = 18
            });

            RightStack.Children.Add(new Label
            {
                Text = Distance + Utilities.DistanceType,
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.FillAndExpand
            });

            ParentGrid.Children.Add(LeftStack);
            ParentGrid.Children.Add(RightStack);


            return ParentGrid;
        }

        public static Frame ListTemplate02(int MedPractID, MedicalPractitionerType MPT, String MedPractName, String Description, double Stars, ImageSource MedPractImage, double Distance,String Address, double Fee, String WorkTime)
        {
            Color TypeColour = (Color)App.Current.Resources["colorGreen"];
            String TypeName = Enum.GetName(typeof(MedicalPractitionerType), MPT);

            deftime = (String.IsNullOrEmpty(WorkTime)) ? deftime : WorkTime;

            Page page = new MedPractProfile(new MedPractProfileDetails(MedPractID, MedicalPractitionerType.Doctor, Stars, Fee, deftime, Distance, MedPractImage, MedPractName, Address, Description));
            
            switch (MPT)
            {
                case MedicalPractitionerType.Hospital:
                    TypeColour = (Color)App.Current.Resources["colorRed"];
                    page = new MedPractProfile(new MedPractProfileDetails(MedPractID, MedicalPractitionerType.Hospital, Stars, Fee, deftime, Distance, MedPractImage, MedPractName, Address, Description));
                    break;
                case MedicalPractitionerType.Laboratory:
                    TypeColour = (Color)App.Current.Resources["colorGold"];
                    page = new MedPractProfile(new MedPractProfileDetails(MedPractID, MedicalPractitionerType.Laboratory, Stars, Fee, deftime, Distance, MedPractImage, MedPractName, Address, Description));
                    break;
                case MedicalPractitionerType.Pharmacy:
                    TypeColour = (Color)App.Current.Resources["_MedAppBlack"];
                    page = new MedPractProfile(new MedPractProfileDetails(MedPractID, MedicalPractitionerType.Pharmacy, Stars, Fee, deftime, Distance, MedPractImage, MedPractName, Address, Description));
                    break;
            };


            Grid ParentGrid = new Grid
            {
                Margin = 5,
                BackgroundColor = (Color)App.Current.Resources["_MedAppLightBlue"],
            };



            TapGestureRecognizer ParentGridTapped = new TapGestureRecognizer();
            ParentGridTapped.Tapped += delegate
            {
                Application.Current.MainPage.Navigation.PushAsync(page);
            };

            ParentGrid.GestureRecognizers.Add(ParentGridTapped);

            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.2, GridUnitType.Star) });
            ParentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.8, GridUnitType.Star) });

            /**Left Stack for the Name, description and etc**/
            StackLayout LeftStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = 5,
            };
            Grid.SetColumn(LeftStack, 0);

            Label NameLabel = new Label
            {
                Text = MedPractName,
                FontAttributes = FontAttributes.Bold,
                TextDecorations = TextDecorations.Underline,
                TextColor = Color.White,
                FontSize = 22,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start
            };

            LeftStack.Children.Add(NameLabel);

            Label DescriptionLabel = new Label
            {
                Text = (Description.Length > 150) ? Description.Substring(0, 150) + "..." : Description,
                HeightRequest = 100,
                TextColor = Color.White
            };

            LeftStack.Children.Add(DescriptionLabel);

            LeftStack.Children.Add(new Label
            {
                Text = "Rating",
                TextDecorations = TextDecorations.Underline,
                FontSize = 18,
                HorizontalTextAlignment = TextAlignment.Start,
                TextColor = Color.White
            });

            StackLayout StarsStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };

            Utilities.CreateStars(StarsStack, Stars, typeof(MedPractListTemplate));

            LeftStack.Children.Add(StarsStack);

            /**Right Stack for the image and etc**/
            StackLayout RightStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            Grid.SetColumn(RightStack, 1);

            StackLayout ImageAndTypeStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Image MedPractPicture = new Image
            {
                Source = MedPractImage,
                Aspect = Aspect.AspectFit,
                HeightRequest = 100,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            ImageAndTypeStack.Children.Add(MedPractPicture);

            ImageAndTypeStack.Children.Add(new Label
            {
                Text = TypeName,
                BackgroundColor = TypeColour,
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.FillAndExpand
            });


            RightStack.Children.Add(ImageAndTypeStack);

            RightStack.Children.Add(new Label
            {
                Text = "Location",
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = 18
            });

            RightStack.Children.Add(new Label
            {
                Text = Distance + Utilities.DistanceType,
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.FillAndExpand
            });

            ParentGrid.Children.Add(LeftStack);
            ParentGrid.Children.Add(RightStack);



            return new Frame { Content = ParentGrid, CornerRadius = 10, Margin = 5, Padding = 5, BackgroundColor = (Color)App.Current.Resources["_MedAppLightBlue"] };
        }
    }
}
