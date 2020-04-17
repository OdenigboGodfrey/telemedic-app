using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Templates;
using Telemedic.Templates.Enums;
using Telemedic.Controller;
using Plugin.Connectivity;

namespace Telemedic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WalletPage : ContentPage
	{
        bool IsMedic = Utilities.IsMedic;

        public WalletPage()
        {
            InitializeComponent();

            Utilities.IOSPageFitter(_ParentStack, Device.RuntimePlatform, 0, Utilities.IOSPaddingTop, 0, 0);

            _BgImage.Source = Utilities.Source("topbar.jpg", typeof(WalletPage));

            //_TransactionParent.Children.Add(WalletTemplate.TransactionTemplate01("200.00",TransactionType.Debit,"Paid 200.00 to Doctor Mrs Connel.\n Reason : Advice Tips on becoming transgender like him/her."));
            //_TransactionParent.Children.Add(WalletTemplate.TransactionTemplate01("400.00", TransactionType.Credit, "Funded Account With the Sum of 400.00 on 28/09/2018."));
            //_TransactionParent.Children.Add(WalletTemplate.TransactionTemplate01("400.00", TransactionType.Credit, "Funded Account With the Sum of 400.00 on 28/09/2018."));


            if (IsMedic)
            {
                _FundAccount.Text = "Withdraw Funds";
            }
            else
            {
                _FundAccount.Text = "Fund Account";
            }
        }

        public WalletPage(int ID, bool IsMedic)
        {
            InitializeComponent();

            Utilities.IOSPageFitter(_ParentStack, Device.RuntimePlatform, 0, Utilities.IOSPaddingTop, 0, 0);
            _BgImage.Source = Utilities.Source("topbar.jpg", typeof(WalletPage));
            //_TransactionParent.Children.Add(WalletTemplate.TransactionTemplate01("200.00", TransactionType.Debit, "Paid 200.00 to Doctor Mrs Connel.\n Reason : Advice Tips on becoming transgender like him/her."));
            //_TransactionParent.Children.Add(WalletTemplate.TransactionTemplate01("400.00", TransactionType.Credit, "Funded Account With the Sum of 400.00 on 28/09/2018."));

            if (IsMedic)
            {
                _FundAccount.Text = "Withdraw Funds";
            }
            else
            {
                _FundAccount.Text = "Fund Account";

            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossConnectivity.IsSupported && CrossConnectivity.Current.IsConnected) await TransactionController.LoadTransactionHistoryToStack(_TransactionParent, _Balance);
        }

        private void _FundAccount_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (IsMedic)
                {
                    Navigation.PushAsync(new WithdrawalPage());
                }
                else
                {
                    Navigation.PushAsync(new FundAccountPage());
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", ex.Message, "ok");
            }

        }
    }
}