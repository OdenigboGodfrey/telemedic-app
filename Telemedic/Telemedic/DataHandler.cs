using System;
using Telemedic.Templates.Enums;
using Xamarin.Forms;

namespace Telemedic
{
    public class OTPData
    {
        public String OTPCode1 { get; set; }
        public String OTPCode2 { get; set; }
        public String OTPCode3 { get; set; }
        public String OTPCode4 { get; set; }
        public GeneralDetails GD { get; set; }

        public OTPData(String OTPCode1, String OTPCode2, String OTPCode3, String OTPCode4, GeneralDetails GD)
        {
            this.OTPCode1 = OTPCode1;
            this.OTPCode2 = OTPCode2;
            this.OTPCode3 = OTPCode3;
            this.OTPCode4 = OTPCode4;
            this.GD = GD;
        }
    }

    public class GeneralDetails
    {
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Password { get; set; }
        public String UserType { get; set; }
        public String ShortCountryCode { get; set; }

        public GeneralDetails(String Email,String Phone, String Password,String UserType, String ShortCountryCode)
        {
            this.Email = Email;
            this.Phone = Phone;
            this.Password = Password;
            this.UserType = UserType;
            this.ShortCountryCode = ShortCountryCode;
        }
    }

    public class UserPersonalDetails
    {
        public String Fullname { get; set; }
        public String Username { get; set; }
        public String Gender { get; set; }
        public String BloodGroup { get; set; }
        public String Address { get; set; }
        public String DOB { get; set; }
        public GeneralDetails GD { get; set; }

        public UserPersonalDetails(GeneralDetails GD, String Fullname,String Username,String Gender,String BloodGroup,String Address,String DOB)
        {
            this.Fullname = Fullname;
            this.Username = Username;
            this.Gender = Gender;
            this.BloodGroup = BloodGroup;
            this.Address = Address;
            this.DOB = DOB;
            this.GD = GD;
        }
    }

    public class HealthPractionerPersonalDetails
    {
        public String Fullname { get; set; }
        public String Username { get; set; }
        public String Gender { get; set; }
        public String BloodGroup { get; set; }
        public String Address { get; set; }
        public String DOB { get; set; }
        public GeneralDetails GD { get; set; }
        public String Field { get; set; }
        public String BioData { get; set; }

        public HealthPractionerPersonalDetails(GeneralDetails GD, String Fullname, String Username, String Gender, String BloodGroup, String Address, String DOB, String Field, String BioData)
        {
            this.Fullname = Fullname;
            this.Username = Username;
            this.Gender = Gender;
            this.BloodGroup = BloodGroup;
            this.Address = Address;
            this.DOB = DOB;
            this.GD = GD;
            this.Field = Field;
            this.BioData = BioData;
        }
    }

    public class BookAppointmentDetails
    {
        public int MPID { get; set; }
        public MedicalPractitionerType MPType { get; private set; }
        public ImageSource MPImage { get; private set; }
        public String MedPractName { get; private set; }
        public String Address { get; private set; }
        public String BookingTime { get; private set; }

        public BookAppointmentDetails(int MPID, MedicalPractitionerType MPType, ImageSource MPImage, String MedPractName, String Address, String BookingTime)
        {
            this.MPID = MPID;
            this.MPType = MPType;
            this.MPImage = MPImage;
            this.MedPractName = MedPractName;
            this.Address = Address;
            this.BookingTime = BookingTime;
        }
    }

    public class MedPractProfileDetails
    {
        public int ID { get; } = Utilities.ID;
        public int MPID { get; set; }
        public MedicalPractitionerType MPType { get; set; }
        public double Stars { get; set; }
        public double Fee { get; set; }
        public String WorkTime { get; set; }
        public double Location { get; set; }
        public ImageSource MPImage { get; set; }
        public String MPName { get; set; }
        public String Address { get; set; }
        public String Description { get; set; }

        public MedPractProfileDetails(int MPID, MedicalPractitionerType MPType, double Stars, double Fee, String WorkTime, double Location, ImageSource MPImage, String MPName, String Address, String Description)
        {
            this.MPID = MPID;
            this.MPType = MPType;
            this.Stars = Stars;
            this.Fee = Fee;
            this.WorkTime = WorkTime;
            this.Location = Location;
            this.MPImage = MPImage;
            this.MPName = MPName;
            this.Address = Address;
            this.Description = Description;
        }
    }
}
