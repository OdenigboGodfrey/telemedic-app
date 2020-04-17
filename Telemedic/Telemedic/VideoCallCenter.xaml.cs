using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Telemedic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VideoCallCenter : ContentPage
	{
        private const double DefOpacity = 0.3;
		public VideoCallCenter ()
		{
			InitializeComponent ();
		}

        public VideoCallCenter(int ID, ImageSource CalleePicture, String Name)
        {
            InitializeComponent();
            _BgImage.Source = CalleePicture;
            _HangupIcon.Source = Utilities.Source("ic_call_end_light_blue.png", typeof(VideoCallCenter));
            _MicOffIcon.Source = Utilities.Source("ic_mic_off_light_blue.png", typeof(VideoCallCenter));
            _VideoCamIcon.Source = Utilities.Source("ic_videocam_light_blue.png", typeof(VideoCallCenter));
            _VolumeIcon.Source = Utilities.Source("ic_volume_mute.png", typeof(VideoCallCenter));
            _DropDownArrowIcon.Source = Utilities.Source("ic_keyboard_arrow_down_white.png", typeof(VideoCallCenter));
            _CalleePic.Source = CalleePicture;
            _CalleeName.Text = Name;
        }

        private void _VolumeIcon_Tapped(object sender, EventArgs e)
        {
            _VolumeIcon.Opacity = Utilities.OpacityToggler(_VolumeIcon.Opacity, DefOpacity);
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