using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Controller;
using Telemedic.Templates;

using Plugin.Connectivity;

using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Permissions.Abstractions;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        String[] ss = new String[] { "Hello", "Hi", "How is it going today", "Fine So Far Thanks" };

        int RecepientID, MedicID = 0;

        public ChatPage()
        {
            InitializeComponent();
            _RecordAudio.Source = Utilities.Source("ic_mic_black_48dp.png", typeof(ChatPage));
            _TakePicture.Source = Utilities.Source("baseline_camera_alt_black_48dp.png", typeof(ChatPage));
            _More.Source = Utilities.Source("ic_more_vert.png", typeof(ChatPage));

            if (Device.RuntimePlatform == Device.UWP) _BackButton.Source = "Assets/ic_arrow_back_ios.png";


            NavigationPage.SetHasNavigationBar(this, false);

        }

        public ChatPage(int ID, bool IsMedic)
        {
            InitializeComponent();
            _RecordAudio.Source = Utilities.Source("ic_mic_black_48dp.png", typeof(ChatPage));
            _TakePicture.Source = Utilities.Source("baseline_camera_alt_black_48dp.png", typeof(ChatPage));
            _More.Source = Utilities.Source("ic_more_vert.png", typeof(ChatPage));

            this.RecepientID = ID;

            if (Device.RuntimePlatform == Device.UWP) _BackButton.Source = "Assets/ic_arrow_back_ios.png";

            NavigationPage.SetHasNavigationBar(this, false);


        }

        public ChatPage(int RecepientID, bool IsMedic, String SenderName)
        {
            InitializeComponent();
            _RecordAudio.Source = Utilities.Source("ic_mic_black_48dp.png", typeof(ChatPage));
            _TakePicture.Source = Utilities.Source("baseline_camera_alt_black_48dp.png", typeof(ChatPage));
            _More.Source = Utilities.Source("ic_more_vert.png", typeof(ChatPage));
            _RecipientName.Text = SenderName;
            this.RecepientID = RecepientID;

            if (Device.RuntimePlatform == Device.UWP) _BackButton.Source = "Assets/ic_arrow_back_ios.png";

            NavigationPage.SetHasNavigationBar(this, false);
        }

        public ChatPage(int RecepientID, bool IsMedic, String SenderName, int MedicID)
        {
            InitializeComponent();
            _RecordAudio.Source = Utilities.Source("ic_mic_black_48dp.png", typeof(ChatPage));
            _TakePicture.Source = Utilities.Source("baseline_camera_alt_black_48dp.png", typeof(ChatPage));
            _More.Source = Utilities.Source("ic_more_vert.png", typeof(ChatPage));
            _RecipientName.Text = SenderName;
            this.RecepientID = RecepientID;
            this.MedicID = MedicID;

            if (Device.RuntimePlatform == Device.UWP) _BackButton.Source = "Assets/ic_arrow_back_ios.png";

            NavigationPage.SetHasNavigationBar(this, false);


        }


        protected async override void OnAppearing()
        {
            var UsersDetails = await SettingsController.GetSettings(RecepientID);
            if (_RecipientName.Text == "User's Name") _RecipientName.Text = UsersDetails["name"];
            if (Convert.ToBoolean(UsersDetails["IsMedic"])) MedicID = Convert.ToInt32(UsersDetails["MedicId"]);

            base.OnAppearing();

            Utilities.IOSPageFitter(_ParentGrid, Device.iOS, Utilities.IOSPaddingLeft, Utilities.IOSPaddingTop, Utilities.IOSPaddingRight, Utilities.IOSPaddingBottom);

            _MessageStack.ChildAdded += async delegate
            {
                var lastChild = _MessageStack.Children.LastOrDefault();
                //if (lastChild != null && Device.RuntimePlatform == Device.iOS) await _ScrollView.ScrollToAsync(lastChild, ScrollToPosition.Start, false);
                //if (lastChild != null && Device.RuntimePlatform != Device.iOS) await _ScrollView.ScrollToAsync(lastChild, ScrollToPosition.End, false);
                await _ScrollView.ScrollToAsync(lastChild, ScrollToPosition.Start, false);
                System.Diagnostics.Debug.WriteLine("Child Added, Total Count " + _MessageStack.Children.Count);
            };

            ChatController.ListenForNewConversation = CrossConnectivity.Current.IsConnected;
            var xx = await ChatController.LocalConversationToStack(_MessageStack, RecepientID);

            if (CrossConnectivity.Current.IsConnected && CrossConnectivity.IsSupported) await ChatController.LoadConversationToStack(_MessageStack, RecepientID, Convert.ToBoolean(xx["MadeChanges"]), _ChatControlsHolder, Convert.ToInt32(xx["LastID"]));


            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    await _ScrollView.ScrollToAsync(_MessageStack.Children.LastOrDefault(), ScrollToPosition.Start, false);
            //}
            //else
            //{
            await _ScrollView.ScrollToAsync(_MessageStack.Children.LastOrDefault(), ScrollToPosition.Start, false);
            //}



        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ChatController.ListenForNewConversation = false;
        }

        
        

        private void _MessageEntry_Unfocused(object sender, FocusEventArgs e)
        {
            if (String.IsNullOrEmpty(_MessageEntry.Text))
            {
                _MediaIconHolder.IsVisible = true;
                _SendMessage.IsVisible = false;
                _MessageStack.Focus();
            }
        }

        private async void _SendMessage_Clicked(object sender, EventArgs e)
        {
            var xx = await ChatController.SendChat(RecepientID, _MessageEntry.Text);
            //if(xx) _MessageStack.Children.Add(SingleMessageTemplate.CreateMessage01(1, _MessageEntry.Text, DateTime.Now.ToShortTimeString()));


            //reset text
            _MessageEntry.Text = "";
        }

        

        private async void _RecordAudio_Tapped(object sender, EventArgs e)
        {
            try
            {
                bool MicPermissionGranted = await Utilities.CheckPermission(Permission.Storage, Utilities.ApplicationName + " would need access to device microphone for recording.");

                if (MicPermissionGranted)
                {
                    FileData fileData = await CrossFilePicker.Current.PickFile();
                    if (fileData == null)
                        return; // user canceled file picking

                    string fileName = fileData.FileName;

                    System.Diagnostics.Debug.WriteLine("File name chosen: " + fileName);
                }
                else
                {
                    await DisplayAlert("Alert", "Microphone permission needed for this action.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert",ex.Message,"Ok");
            }
        }

        private async void _TakePicture_Tapped(object sender, EventArgs e)
        {
            try
            {
                bool CameraPermissionGranted = await Utilities.CheckPermission(Permission.Storage, Utilities.ApplicationName + " would need access to device camera to take pictures.");
                if (CameraPermissionGranted)
                {

                }
                else
                {
                    await DisplayAlert("Alert", "Camera permission needed for this action.", "Ok");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void _More_Clicked(object sender, EventArgs e)
        {
            /**
             * display an action sheet to the user to select action
             * **/
            String UserSelectedAction = "";
            if (!Utilities.IsMedic)
            {
                UserSelectedAction = await DisplayActionSheet("Select Action", "Cancel", null, new String[] { "Submit Review" });
            }
            else
            {
                UserSelectedAction = await DisplayActionSheet("Select Action", "Cancel", null, new String[] { "Read Review", "Medical History" });
            }

            switch (UserSelectedAction)
            {
                case "Submit Review":
                    await Navigation.PushAsync(new AddReviewPage(MedicID));
                    break;
                case "Read Review":
                    await Navigation.PushAsync(new ReviewPage(RecepientID));
                    break;
                case "Medical History":
                    await Navigation.PushAsync(new AddRecordPage(RecepientID, Templates.Enums.RecordType.Medical));
                    break;
            }
        }

        private void _MessageStack_Focused(object sender, FocusEventArgs e)
        {
            _MediaIconHolder.IsVisible = true;
            _SendMessage.IsVisible = false;
        }

        private void _ScrollView_Focused(object sender, FocusEventArgs e)
        {
            if (_SendMessage.IsFocused)
            {
                _MessageStack.Focus();
            }
        }

        private void _MessageEntry_Focused(object sender, FocusEventArgs e)
        {
            _MediaIconHolder.IsVisible = false;
            _SendMessage.IsVisible = true;
        }

        private void _BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }


    }
}