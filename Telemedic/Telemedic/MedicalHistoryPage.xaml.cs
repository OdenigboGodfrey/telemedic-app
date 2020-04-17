using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Templates.Enums;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MedicalHistoryPage : ContentPage
	{
        int ID,MPID;
        bool IsMedic = false;
		public MedicalHistoryPage ()
		{
			InitializeComponent ();
		}



        public MedicalHistoryPage(int ID, bool IsMedic, int MPID)
        {
            InitializeComponent();

            this.ID = ID;
            this.MPID = MPID;
            this.IsMedic = IsMedic;

            if (IsMedic)
            {
                _ViewRequestButton.IsVisible = false;
            }
            
        }

        private void _ViewRequestButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MedicalHistoryRequest(ID));
        }

        private void DrugPrescriptions_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListRecordsPage(ID, MPID, IsMedic, RecordType.Drug));
        }

        private void LabRecords_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListRecordsPage(ID, MPID, IsMedic, RecordType.Lab));
        }

        private void MedicalRecords_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListRecordsPage(ID, MPID, IsMedic, RecordType.Medical));
        }

        private void PersonalRecords_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListRecordsPage(ID, MPID, IsMedic, RecordType.Personal));
        }
    }
}