using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Telemedic.Templates;
using Telemedic.Templates.Enums;
using Plugin.Connectivity;

using Telemedic.Controller;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListRecordsPage : ContentPage
    {
        String ass = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco 
        laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, 
        sunt in culpa qui officia deserunt mollit anim id est laborumLorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magnaaliqua. 
        Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla 
        pariatur. Excepteur sint occaecatcupidatat non proident, sunt in culpa qui officiadeserunt mollit anim id est laborum.";

        int ID, MPID;
        RecordType UserRecordType;

        public ListRecordsPage()
        {
            InitializeComponent();
        }

        public ListRecordsPage(int ID, int MPID, bool IsMedic, RecordType UserRecordType)
        {
            InitializeComponent();
            this.ID = ID;
            this.MPID = MPID;
            this.UserRecordType = UserRecordType;
            this.Title = (RecordType.Drug != UserRecordType) ? Enum.GetName(typeof(RecordType), UserRecordType) + " Records" : "Drug Prescriptions";
            /**
             * if the user is not a medic and record type is not personal, disable the add record button
             * as the user can only add record to the personal records page
             * **/
            if (!IsMedic && !UserRecordType.Equals(RecordType.Personal)) _AddRecordButton.IsVisible = false;

            //_ParentStack.Children.Add(RecordTemplate.RecordTemplate01(0, "Title", ass, false));
            //_ParentStack.Children.Add(RecordTemplate.RecordTemplate01(0, "Title 2", "Content", false));
        }

        public ListRecordsPage(int ID, RecordType UserRecordType)
        {
            InitializeComponent();
            this.ID = ID;
            this.UserRecordType = UserRecordType;
            this.Title = (RecordType.Drug != UserRecordType) ? Enum.GetName(typeof(RecordType), UserRecordType) + " Records" : "Drug Prescriptions";

            if (!UserRecordType.Equals(RecordType.Personal)) _AddRecordButton.IsVisible = false;

            //_ParentStack.Children.Add(RecordTemplate.RecordTemplate01(0,"Title",ass,false));
            //_ParentStack.Children.Add(RecordTemplate.RecordTemplate01(0, "Title 2", "Content", false));
        }

        public ListRecordsPage(int ID)
        {
            InitializeComponent();

            this.ID = ID;

            _ParentStack.Children.Add(RecordTemplate.RecordTemplate01(0, "Title", ass, false));
            _ParentStack.Children.Add(RecordTemplate.RecordTemplate01(0, "Title 2", "Content", false));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (CrossConnectivity.IsSupported && CrossConnectivity.Current.IsConnected)
            {
                if (_ParentStack.Children.Count > 0) _ParentStack.Children.Clear();
                await MedicalRecordController.GetMedicalRecords(UserRecordType, _ParentStack);
            }
            else
            {

            }
        }

        private void _AddRecordButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddRecordPage(ID, UserRecordType));
        }
    }
}