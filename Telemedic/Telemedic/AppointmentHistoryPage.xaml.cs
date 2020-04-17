using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Telemedic.Templates;
using Telemedic.Templates.Enums;

namespace Telemedic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppointmentHistoryPage : ContentPage
	{
        bool IsMedic = Utilities.IsMedic;
		public AppointmentHistoryPage ()
		{
			InitializeComponent ();
            Utilities.AuthControlHider(_ParentStack, "For-Doctor");
		}

        public AppointmentHistoryPage(int ID,bool IsMedic)
        {
            InitializeComponent();
            this.IsMedic = IsMedic;
            if (IsMedic)
            {
                _ParentStack.Children.Add(AppointmentHistoryTemplate.TemplateForDoctor02(0, 0, "Mr. Silas", "30 Cherubium Junction, \nWetheral Road, \nOwerri Imo State", "9:30AM", Status.Approved, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud.", "000"));
                _ParentStack.Children.Add(AppointmentHistoryTemplate.TemplateForDoctor02(0, 0, "Mr. Jendy", "20 IBC, \nOrji, \nOwerri Imo State", "11:30AM", Status.Waiting, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud.", "000"));
            }
            else
            {
                _ParentStack.Children.Add(AppointmentHistoryTemplate.TemplateForUser01(0, HistoryType.Doctor, "Dr. Towel", "35 Cherubium Junction, \nWetheral Road, \nOwerri Imo State"));
                _ParentStack.Children.Add(AppointmentHistoryTemplate.TemplateForUser01(0, HistoryType.Hospital, "Dr. Pan", "40 Cherubium Junction, \nWetheral Road, \nOwerri Imo State"));
                Utilities.AuthControlHider(_ParentStack, "For-Doctor");
            }


        }
    }
}