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

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatListAllPage : ContentPage
    {
        private Image UserImage;

        public ChatListAllPage()
        {
            InitializeComponent();

            //_ChatsListAll.Children.Add(ListAllChatTemplate.CreateNewStackType01("Name1", "", "Content of a very long descripton here, some spellings are wrong sha", "10:04", this, 0, 0));
            //_ChatsListAll.Children.Add(ListAllChatTemplate.CreateNewStackType01("Name2", "", "Content of a very long descripton here, some spellings are wrong sha", "10:04", this, 0,1));
            //_ChatsListAll.Children.Add(ListAllChatTemplate.CreateNewStackType01("Name3", "", "Content of a very long descripton here, some spellings are wrong sha", "10:04", this, 0,1));
            //_ChatsListAll.Children.Add(Templates.ListAllChatTemplate.CreateNewStackType01("Name3", "", "Content of a very long descripton here, some spellings are wrong sha", "10:04", this, 0,2));
            //_ChatsListAll.Children.Add(Templates.ListAllChatTemplate.CreateNewStackType01("Name4", "", "Content of a very long descripton here, some spellings are wrong sha", "10:04", this, 0,0));
            _BgImage.Source = Utilities.Source("topbar.jpg", typeof(ChatListAllPage));
            _PopUpMakeCall.Source = Utilities.Source("ic_call_black.png", typeof(ChatListAllPage));
            _PopUpMakeVideoCall.Source = Utilities.Source("ic_videocam_black_48dp.png", typeof(ChatListAllPage));
            _PopUpMessage.Source = Utilities.Source("ic_message_black.png", typeof(ChatListAllPage));
            _PopUpUserInfo.Source = Utilities.Source("ic_info_black.png", typeof(ChatListAllPage));
            _PopUpUserImage.Source = Utilities.Source("IMG_5204.JPG", typeof(ChatListAllPage));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Utilities.IOSPageFitter(_ParentStack, Device.RuntimePlatform, 0, Utilities.IOSPaddingTop, 0, 0);

            if (CrossConnectivity.Current.IsConnected)
            {
                ChatController.ListenForChatListUpdate = true;

                var xx = await ChatController.LocalChatsListToStack(_ChatsListAll, this);
                System.Diagnostics.Debug.WriteLine("Done With Local, Result  " + xx);
                await ChatController.ChatsListToStack(_ChatsListAll, this, false);
            }
            else
            {
                var xx = await ChatController.LocalChatsListToStack(_ChatsListAll, this);
            }
        }

        protected override void OnDisappearing()
        {
            ChatController.ListenForChatListUpdate = false;
            base.OnDisappearing();
        }

        private void _UserPopUpGrid_Unfocused(object sender, FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                ((AbsoluteLayout)sender).IsVisible = false;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            _UserPopUpGrid.IsVisible = false;
        }

        private void _PopUpMessage_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChatPage(Convert.ToInt32(((Dictionary<String, object>)ControlTagger<Object>.GetTag(UserImage))["ID"]), Utilities.IsMedic));
            //Navigation.PushAsync(new DetailedWaitingPatient(0,"Jendy Jasper","9:00AM","",Utilities.Source("IMG5204.jpg",typeof(ChatListAllPage))));
        }

        private void _PopUpMakeCall_Tapped(object sender, EventArgs e)
        {
            UserImage = FindByName("_PopUpUserImage") as Image;
            /** get the tagged data from the _PopUserImage as pass it to the CallCenter constructor **/
            Navigation.PushAsync(new CallCenter(Convert.ToInt32(((Dictionary<String, object>)ControlTagger<Object>.GetTag(UserImage))["ID"]), (ImageSource)((Dictionary<String, object>)ControlTagger<Object>.GetTag(UserImage))["Image"], ((Dictionary<String, object>)ControlTagger<Object>.GetTag(UserImage))["Name"].ToString()));

        }

        private void _PopUpMakeVideoCall_Tapped(object sender, EventArgs e)
        {
            UserImage = FindByName("_PopUpUserImage") as Image;
            /** get the tagged data from the _PopUserImage as pass it to the VideoCallCenter constructor **/
            Navigation.PushAsync(new VideoCallCenter(Convert.ToInt32(((Dictionary<String, object>)ControlTagger<Object>.GetTag(UserImage))["ID"]), (ImageSource)((Dictionary<String, object>)ControlTagger<Object>.GetTag(UserImage))["Image"], ((Dictionary<String, object>)ControlTagger<Object>.GetTag(UserImage))["Name"].ToString()));
        }

        private void _PopUpUserInfo_Tapped(object sender, EventArgs e)
        {

        }

        private void StackLayout_Focused(object sender, FocusEventArgs e)
        {


        }
    }
}