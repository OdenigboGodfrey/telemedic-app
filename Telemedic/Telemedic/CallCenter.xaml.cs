using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CallCenter : ContentPage
	{
        private const double DefOpacity = .3;

        public CallCenter ()
		{
			InitializeComponent ();
		}

        public CallCenter(int ID,ImageSource CalleePicture,String Name)
        {
            InitializeComponent();
            _BgImage.Source = Utilities.Source("Med_ToolBar.jpg",typeof(CallCenter));
            _HangupIcon.Source = Utilities.Source("ic_call_end_light_blue.png", typeof(CallCenter));
            _MicOffIcon.Source = Utilities.Source("ic_mic_off_light_blue.png",typeof(CallCenter));
            _VideoCamIcon.Source = Utilities.Source("ic_videocam_light_blue.png",typeof(CallCenter));
            _VolumeIcon.Source = Utilities.Source("ic_volume_mute.png",typeof(CallCenter));
            _DropDownArrowIcon.Source = Utilities.Source("ic_keyboard_arrow_down_white.png",typeof(CallCenter));
            _CalleePic.Source = CalleePicture;
            _CalleeName.Text = Name;
        }
        
        private void _VolumeIcon_Tapped(object sender, EventArgs e)
        {
            _VolumeIcon.Opacity = Utilities.OpacityToggler(_VolumeIcon.Opacity,DefOpacity);
        }

        private void _VideoCamIcon_Tapped(object sender, EventArgs e)
        {
            _VideoCamIcon.Opacity = Utilities.OpacityToggler(_VideoCamIcon.Opacity, DefOpacity);
        }

        private void _MicOffIcon_Tapped(object sender, EventArgs e)
        {
            _MicOffIcon.Opacity = Utilities.OpacityToggler(_MicOffIcon.Opacity, DefOpacity);
        }

        
    }
}