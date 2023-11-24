using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEB_API_With_Stored_Procedure.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace WEB_API_With_Stored_Procedure.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnection con =new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString);

        Employee Emp = new Employee();

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public string Post(Employee employee)
        {
            string msg = "";

            if (employee != null)
            {
                SqlCommand cmd = new SqlCommand("use_AddEmployee", con);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Customer_ID", employee.Id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Email_ID", employee.MailId);
                cmd.Parameters.AddWithValue("@Booking_Date", employee.BookingDate   );
                cmd.Parameters.AddWithValue("@Event_Name", employee.EventName);
                cmd.Parameters.AddWithValue("@Event_Date", employee.EventDate);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@BloodGroup", employee.BloodGroup);
                cmd.Parameters.AddWithValue("@Mobile_No", employee.Mobile_No);
                cmd.Parameters.AddWithValue("@Status", employee.Status);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if(i>0)
                {
                    msg= "Data has been inserted";
                }
                else
                {
                    msg= "Error";
                }
            }
            return msg;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
