using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

using Telemedic.Templates.Enums;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Globalization;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace Telemedic
{
    /**
     * Please add new methods to the end of the file for consitency
     * **/
    class Utilities
    {
        //http://localhost:8000
        //http://192.168.1.114:8000

        //public static String BaseAddress = "http://192.168.0.205:8000";
        //public static String BaseAddress = "http://192.168.0.210:1000";
        public static String BaseAddress = "https://telemedapp.herokuapp.com";

        public static String ResourceBaseAddress = "Telemedic.Assets.";
        public static String DistanceType = "km";
        public static String Currency = "NGN";
        public static String NoInternet = "Unnable to connect, Please check internet connection";
        public static String ApplicationName = "Telemedic";


        public static int ID = 0;
        public static int MedicID = 0;
        public static int IOSPaddingLeft = 0;
        public static int IOSPaddingTop
        {
            get
            {
                return (Device.RuntimePlatform == Device.iOS) ? DependencyService.Get<IStatusBar>().GetHeight() : 0;
            }
            set
            {
                IOSPaddingTop = value;
            }
        }
        public static int IOSPaddingRight = 0;
        public static int IOSPaddingBottom = 0;


        public static bool IsLoggedIn = false, IsMedic = true;

        public static double Balance = 0;

        public static ImageSource Source(String FileName, Type ClassType)
        {
            return ImageSource.FromResource(ResourceBaseAddress + FileName, ClassType);
        }

        /**
         * summary This Method returns time in minute or hours, taking a param name="TimeInSeconds" which as the name suggests in time in seconds
         * returns a String of the Time in either in minutes or hours based on the param name="InHours" which determines what UserType of time to return
         * **/
        public static String TimeInMinutesOrHours(int TimeInSeconds, bool InHours)
        {
            if (InHours)
            {

                return (((int)(TimeInSeconds) / 60) / 60).ToString("D2") + ":" + (((int)(TimeInSeconds) / 60) % 60).ToString("D2") + ":" + (TimeInSeconds % 60).ToString("D2");
            }
            else
            {
                return ((int)(TimeInSeconds) / 60).ToString("D2") + ":" + (TimeInSeconds % 60).ToString("D2");
            }
        }

        /**
         * summary This Method Clears the navigation stack
         * **/

        public static void ClearNavigationStack()
        {
            var existingPages = Application.Current.MainPage.Navigation.NavigationStack;
            foreach (var page in existingPages)
            {
                //Application.Current.MainPage.Navigation.RemovePage(page);
            }
        }
        /**
         * summary This Method Handles the creation of Post data from the values passed as param name="Data"
         * returns the Encoded Data which would be passed to the web api
         * **/
        public static FormUrlEncodedContent PostDataEncoder(Dictionary<int, String[]> Data)
        {
            var FormPostData = new List<KeyValuePair<String, String>>();
            foreach (int key in Data.Keys)
            {
                FormPostData.Add(new KeyValuePair<string, string>(Data[key][0], Data[key][1]));
            }

            var UrlEncoedContent = new FormUrlEncodedContent(FormPostData);
            return UrlEncoedContent;
        }
        /**
         * summary this method handled the resizing of the application on load
         * **/
        public static void DoResize(Grid ParentGrid)
        {
            int MaxWidth = 550;
            if (ParentGrid.Width > MaxWidth)
            {
                //DisplayAlert("Alert", "" + ParentGrid.Width, "cancel");
                ParentGrid.WidthRequest = MaxWidth;
                ParentGrid.HorizontalOptions = LayoutOptions.Center;
            }
            else
            {
                if (ParentGrid.HorizontalOptions.Equals(LayoutOptions.Center)) ParentGrid.HorizontalOptions = LayoutOptions.CenterAndExpand;
            }
        }



        /** OpacityToggler toggles the opacity between the default opacity and 1**/

        public static double OpacityToggler(double CurrentOpacity, double DefaultOpacity)
        {
            double Opacity = DefaultOpacity;

            if (CurrentOpacity.Equals(DefaultOpacity))
            {
                Opacity = 1;
            }

            return Opacity;
        }

        /**
         * summary=AuthCOntrolHider hides control from the user based on the param name="ClassID" passed
         * param name="ParentLayout" is the containing layout where hidden controls exists
         * param name="ClassID" is the class id which should be hidden  from the current user
         * **/
        public static void AuthControlHider<T>(T ParentLayout, String ClassID) where T : Layout<View>
        {
            foreach (View ChildView in (ParentLayout).Children)
            {
                if (ChildView.ClassId == null)
                {
                    continue;
                }

                if (ChildView.ClassId.Equals(ClassID))
                {
                    ChildView.IsVisible = false;
                }
            }
        }

        /**  **/

        public static void CreateStars(StackLayout ParentLayout, double Stars, Type PageType)
        {
            for (int i = 0; i < Math.Floor(Stars); i++)
            {
                ParentLayout.Children.Add(
                    new Image
                    {
                        Source = Utilities.Source("ic_star_yellow.png", PageType),
                        WidthRequest = 20,
                        HeightRequest = 20
                    });
            }

            if (Math.Floor(Stars) != Math.Ceiling(Stars))
            {
                ParentLayout.Children.Add(
                    new Image
                    {
                        Source = Utilities.Source("ic_half_star_yellow.png", PageType),
                        WidthRequest = 20,
                        HeightRequest = 20
                    });
            }
        }

        /**
         * summary this method handled the resizing of the application on load on the IOS platform
         * 
         * **/
        public static void IOSPageFitter<T>(T ParentGrid, String RunTimePlatform, int Left, int Top, int Right, int Bottom) where T : Layout
        {
            if (RunTimePlatform == Device.iOS) ParentGrid.Padding = new Thickness(Left, Top, Right, Bottom);
        }

        /**
         * summary method to download user image from  the server and save locally for 7 days
         * **/
        public static UriImageSource UriSource(String ImageAddress)
        {
            return new UriImageSource
            {
                Uri = new Uri(ImageAddress),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(7, 0, 0, 0, 0)
            };
        }

        public static void SendNotification(int ID, bool IsMedic, String Title, String Description, NotificationType UserNotificationType)
        {
            var notificator = DependencyService.Get<IToastNotificator>();

            notificator.Notify(delegate
            {

                if (UserNotificationType == NotificationType.Chat)
                {
                    App.Current.MainPage.Navigation.PushAsync(new ChatPage(ID, IsMedic));
                }
                else if (UserNotificationType == NotificationType.Reminder)
                {
                    App.Current.MainPage.Navigation.PushAsync(new NotificationPage(ID));
                }
            }, new NotificationOptions()
            {
                Title = Title,
                Description = Description,
                IsClickable = true,
                WindowsOptions = new WindowsOptions()
                {
                    LogoUri = (Device.RuntimePlatform == Device.UWP) ? "Assets/__icon.png" : "__icon.png"
                },
                ClearFromHistory = false,
                AllowTapInNotificationCenter = true,
                AndroidOptions = new AndroidOptions()
                {
                    HexColor = "#F99D1C",
                    ForceOpenAppOnNotificationTap = true
                }
            });
        }

        /**
         * summary CreateAlertDialog shows a displayalert to the user, after the user has closed the displayalert, performs an action
         * param name="buttonText" is the text to be shown to the user
         * param name="message" is the message to be shown to the user
         * param name="title" is the title of the alert
         * param name="afterHideCallback" is the action to be performed after user has closed alert dialog
         * **/
        public static async Task CreateAlertDialog(string title, string message, string buttonText, Action afterHideCallback)
        {
            await App.Current.MainPage.DisplayAlert(title, message, buttonText);

            afterHideCallback?.Invoke();
        }

        /**
         * summary RunTask param name="action" is the action to be performed when the task runs
         * **/
        public static async Task RunTask(Action action)
        {
            //await Task.ListenForNewConversation(async () => 
            //{
            //    action?.Invoke();
            //});

            await Task.Run(action);
        }


        /** code to check if page passed exists in the navigation stack **/
        public static bool CheckNavigationStack(Page YourPage)
        {
            return App.Current.MainPage.Navigation.NavigationStack.Any(p => p == YourPage);
        }


        /// <summary>
        /// Converts the datetime passed to user friendly info e.g 2019/01/02 can be returned as Wed or Jan 2 or 2019 based on the current datetime
        /// </summary>
        /// <param name="PassedDateTime">
        /// the datetime to be converted
        /// </param>
        /// <param name="CST">
        /// ChatSectionType determines how the time would be converted as different convertions are needed for different views
        /// e.g in List All Chat the date 2019/01/02 can be retured as Wed or Jan or 2019
        /// and Chat Page could be hh:mm or ddd, hh:mm
        /// </param>
        /// <returns>
        /// a string of the converted value
        /// </returns>
        public static String GetValidatedTime(DateTime PassedDateTime, ChatSectionType CST)
        {
            String Result = "";
            DateTime Now = DateTime.Now;

            CultureInfo cul = CultureInfo.CurrentCulture;
            int WeekNum = cul.Calendar.GetWeekOfYear(PassedDateTime, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);
            int CurrentWeekNum = cul.Calendar.GetWeekOfYear(Now, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);

            //same day
            if (PassedDateTime.ToLocalTime().ToString("yyyyMMdd") == Now.ToLocalTime().ToString("yyyyMMdd"))
            {
                Result = PassedDateTime.ToLocalTime().ToString("hh:mm");
            }
            else
            {
                //same week
                if (PassedDateTime.ToLocalTime().ToString("yyyyMM") == Now.ToLocalTime().ToString("yyyyMM") && CurrentWeekNum == WeekNum)
                {
                    Result = (CST == ChatSectionType.ChatList) ? PassedDateTime.ToLocalTime().ToString("ddd") : PassedDateTime.ToLocalTime().ToString("ddd, hh:mm");
                }
                else if (PassedDateTime.ToLocalTime().ToString("yyyyMM") == Now.ToLocalTime().ToString("yyyyMM") && CurrentWeekNum > WeekNum)
                {
                    //different weeks same month
                    Result = (CST == ChatSectionType.ChatList) ? PassedDateTime.ToLocalTime().ToString("MMM, dd") : PassedDateTime.ToLocalTime().ToString("MMM dd, hh:mm");
                }
                //different months
                else if (PassedDateTime.ToLocalTime().ToString("yyyyMM") != Now.ToLocalTime().ToString("yyyyMM"))
                {
                    //same year
                    if (PassedDateTime.Year == Now.Year)
                    {
                        Result = (CST == ChatSectionType.ChatList) ? PassedDateTime.ToLocalTime().ToString("MMM") : PassedDateTime.ToLocalTime().ToString("MMM dd, hh:mm");
                    }
                    else
                    {
                        //different years
                        Result = (CST == ChatSectionType.ChatList) ? PassedDateTime.ToLocalTime().ToString("MMM yyyy") : PassedDateTime.ToLocalTime().ToString("yyyy MMM dd, hh:mm");
                    }
                }
            }
            return Result;
        }

        /// <summary>
        /// returns the time in 12 hour formart
        /// </summary>
        /// <param name="TimeIn24Hours">the time in 24 hr format to be converted</param>
        /// <returns></returns>
        public static String TimeIn12Hours(TimeSpan TimeIn24Hours)
        {
            String ReturnedTime = "";

            if (TimeIn24Hours.Hours == 0)
            {
                ReturnedTime = 12 + ":" + TimeIn24Hours.Minutes.ToString("D2") + " " + "AM";
            }
            else if (TimeIn24Hours.Hours == 12)
            {
                ReturnedTime = 12 + ":" + TimeIn24Hours.Minutes.ToString("D2") + " " + "PM";
            }
            else if (TimeIn24Hours.Hours > 12)
            {
                ReturnedTime = (TimeIn24Hours.Hours - 12) + ":" + TimeIn24Hours.Minutes.ToString("D2") + " " + "PM";
            }
            else
            {
                ReturnedTime = TimeIn24Hours.Hours + ":" + TimeIn24Hours.Minutes.ToString("D2") + " " + "AM";
            }

            return ReturnedTime;
        }

        /// <summary>
        /// returns the week number of the passeddatetime
        /// </summary>
        /// <param name="PassedDateTime">datetime to get the weeekly value</param>
        /// <returns></returns>
        public static int WeekNumber(DateTime PassedDateTime)
        {
            CultureInfo cul = CultureInfo.CurrentCulture;
            int WeekNum = cul.Calendar.GetWeekOfYear(PassedDateTime, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);

            return WeekNum;
        }

        public static async Task<bool> CheckPermission(Permission PermissionType, String Message)
        {
            var PermissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(PermissionType);
            if (PermissionStatus != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(PermissionType))
                {
                    await App.Current.MainPage.DisplayAlert("Alert", Message, "OK");
                }

                var Results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { PermissionType });
                PermissionStatus = Results[PermissionType];
            }

            return (PermissionStatus == PermissionStatus.Granted);
        }

        
    }

    /**
         * ControlTaggerclass was created to tag values on controls
         * typeparam name="T" is the type which would be tagged on to the control 
         * **/

    public static class ControlTagger<T>
    {
        public static readonly BindableProperty TagProperty = BindableProperty.Create("Tag", typeof(T), typeof(ControlTagger<T>), null);

        public static T GetTag(BindableObject bindable)
        {
            return (T)bindable.GetValue(TagProperty);
        }

        public static void SetTag(BindableObject bindable, T value)
        {
            bindable.SetValue(TagProperty, value);
        }
    }
}
