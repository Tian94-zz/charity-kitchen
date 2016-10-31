using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharityKitchen
{
    public class Client
    {
        private int ClientID
        {
            get;set;
        }
        private string ClientFirstName
        {
            get;set;
        }
        private string ClientLastName
        {
            get;set;
        }
        private DateTime ClientDOB
        {
            get;set;
        }
        private string ClientPhoneNumber
        {
            get;set;
        }
        private string ClientEmail
        {
            get;set;
        }
        private string ClientAddress
        {
            get;set;
        }
        private string ClientPostCode
        {
            get;set;
        }
        private string ClientSuburb
        {
            get;set;
        }
        private int ClientState
        {
            get;set;
        }

        public Client()
        {
            ClientFirstName = "";
            ClientLastName = "";
            ClientAddress = "";
            ClientDOB = DateTime.Now;
            ClientEmail = "";
            ClientID = 0;
            ClientPhoneNumber = "";
            ClientPostCode = "";
            ClientState = 0;
            ClientSuburb = "";
        }

        public Client(int ID, string FirstName, string LastName, DateTime DOB, string PhoneNumber, string Email, string Address, int State, string Suburb, string Postcode)
        {
            ClientID = ID;
            ClientFirstName = FirstName;
            ClientLastName = LastName;
            ClientDOB = DOB;
            ClientPhoneNumber = PhoneNumber;
            ClientEmail = Email;
            ClientAddress = Address;
            ClientState = State;
            ClientSuburb = Suburb;
            ClientPostCode = Postcode;
        }
    }
}