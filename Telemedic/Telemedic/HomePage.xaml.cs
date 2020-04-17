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
	public partial class HomePage : TabbedPage
    {
		public HomePage ()
		{
            bool IsMedic = Utilities.IsMedic;
			InitializeComponent ();


            if (IsMedic)
            {
                this.Children.Remove(this.Children[0]);
            }
            else
            {
                this.Children.Remove(this.Children[1]);
            }
        }

        public HomePage(int ID,bool IsMedic)
        {
            InitializeComponent();


            if (IsMedic)
            {
                this.Children.Remove(this.Children[0]);
            }
            else
            {
                this.Children.Remove(this.Children[1]);
            }
        }
    }
}