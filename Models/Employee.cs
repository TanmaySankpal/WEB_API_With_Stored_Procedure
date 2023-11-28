using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_API_With_Stored_Procedure.Models
{
    public class Employee
    {   
       // public int Id { get; set; }

        public string Name { get; set; }

        public string Email_ID { get; set; }

        public DateTime? Booking_Date { get; set; }

        public string Event_Name { get; set; }

        public DateTime? Event_Date { get; set; }
        
        public string Gender { get; set; }

        public string BloodGroup { get; set; }

        public string Mobile_No { get; set; }

        public string Status { get; set; }

    }
}