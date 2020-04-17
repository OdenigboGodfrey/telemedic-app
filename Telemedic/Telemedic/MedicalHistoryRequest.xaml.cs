using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Telemedic.Templates;

namespace Telemedic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MedicalHistoryRequest : ContentPage
	{
		public MedicalHistoryRequest ()
		{
			InitializeComponent ();
		}

        public MedicalHistoryRequest(int ID)
        {
            InitializeComponent();
            _ParentStack.Children.Add(MedicalHistoryRequestTemplate.RequestTemplate02(0, "Dr. Silas"));
            _ParentStack.Children.Add(MedicalHistoryRequestTemplate.RequestTemplate02(0, "Dr. Jendy"));
        }
    }
}