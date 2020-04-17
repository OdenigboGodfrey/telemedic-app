using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Controller;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedPractWorkSettingsDisplay : ContentPage
    {
        String Price, BookingTime, AccountName, AccountNumber, BankName;

        public MedPractWorkSettingsDisplay()
        {
            InitializeComponent();
        }

        public MedPractWorkSettingsDisplay(int Price, String AvailableHours, String AccountName, String BankName, String AccountNumber)
        {
            InitializeComponent();
            _Price.Text = Price.ToString(); this.Price = Price.ToString();
            _BookingTime.Text = AvailableHours; this.BookingTime = AvailableHours;
            _AccountName.Text = "Name: " + AccountName; this.AccountName = AccountName;
            _AccountNumber.Text = "Account Number: " + AccountNumber; this.AccountNumber = AccountNumber;
            _BankName.Text = "Bank Name: " + BankName; this.BankName = BankName;

        }

    }
}