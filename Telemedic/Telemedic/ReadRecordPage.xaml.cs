using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReadRecordPage : ContentPage
    {
        public ReadRecordPage()
        {
            InitializeComponent();
        }

        public ReadRecordPage(int ID)
        {
            InitializeComponent();
        }

        public ReadRecordPage(int ID,String Record, String Title)
        {
            InitializeComponent();
            _ReviewMessage.Text = Record;
            _Title.Text = Title;
        }
    }
}