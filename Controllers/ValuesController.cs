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
using Newtonsoft.Json;

namespace WEB_API_With_Stored_Procedure.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString);

        Employee Emp = new Employee();

        // GET api/values
        public List<Employee> Get()
        {
            SqlDataAdapter da = new SqlDataAdapter("use_GetAllEmployees", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Employee> lstEmployee = new List<Employee>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee emp = new Employee();
                    emp.Name = dt.Rows[i]["Name"].ToString();
                    emp.Email_ID = dt.Rows[i]["Email_Id"].ToString();
                    if (dt.Rows[i]["Booking_Date"] != DBNull.Value && DateTime.TryParse(dt.Rows[i]["Booking_Date"].ToString(), out DateTime parsedDate))
                    {
                        emp.Booking_Date = parsedDate; // Assign the parsed DateTime value
                    }
                    else
                    {
                        emp.Booking_Date = null; // Handle the case where the value is DBNull or cannot be parsed
                    }
                    emp.Event_Name = dt.Rows[i]["Event_Name"].ToString();
                    if (dt.Rows[i]["Event_Date"] != DBNull.Value && DateTime.TryParse(dt.Rows[i]["Event_Date"].ToString(), out DateTime parsedDate1))
                    {
                        emp.Event_Date = parsedDate1; // Assign the parsed DateTime value
                    }
                    else
                    {
                        emp.Event_Date = null; // Handle the case where the value is DBNull or cannot be parsed
                    }
                    emp.Gender = dt.Rows[i]["Gender"].ToString();
                    emp.BloodGroup = dt.Rows[i]["BloodGroup"].ToString();
                    emp.Mobile_No = dt.Rows[i]["Mobile_No"].ToString();
                    emp.Status = dt.Rows[i]["Status"].ToString();
                    lstEmployee.Add(emp);
                }
            }
            if (lstEmployee.Count > 0)
            {
                return lstEmployee;
            }
            else
            { return null; }

        }

        // GET api/values/5
        public Employee Get(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("use_GetEmplyeeById", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Employee emp = new Employee();

            if (dt.Rows.Count > 0)
            {

                emp.Name = dt.Rows[0]["Name"].ToString();
                emp.Email_ID = dt.Rows[0]["Email_Id"].ToString();
                if (dt.Rows[0]["Booking_Date"] != DBNull.Value && DateTime.TryParse(dt.Rows[0]["Booking_Date"].ToString(), out DateTime parsedDate))
                {
                    emp.Booking_Date = parsedDate; // Assign the parsed DateTime value
                }
                else
                {
                    emp.Booking_Date = null; // Handle the case where the value is DBNull or cannot be parsed
                }
                emp.Event_Name = dt.Rows[0]["Event_Name"].ToString();
                if (dt.Rows[0]["Event_Date"] != DBNull.Value && DateTime.TryParse(dt.Rows[0]["Event_Date"].ToString(), out DateTime parsedDate1))
                {
                    emp.Event_Date = parsedDate1; // Assign the parsed DateTime value
                }
                else
                {
                    emp.Event_Date = null; // Handle the case where the value is DBNull or cannot be parsed
                }
                emp.Gender = dt.Rows[0]["Gender"].ToString();
                emp.BloodGroup = dt.Rows[0]["BloodGroup"].ToString();
                emp.Mobile_No = dt.Rows[0]["Mobile_No"].ToString();
                emp.Status = dt.Rows[0]["Status"].ToString();

            }

            if (emp != null)
            {
                return emp;
            }
            else
            { return null; }

        }



        // POST api/values
        public string Post(Employee employee)
        {
            string msg = "";

            if (employee != null)
            {
                SqlCommand cmd = new SqlCommand("InsertEvent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Customer_ID", employee.Id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Email_ID", employee.Email_ID);
                cmd.Parameters.AddWithValue("@Booking_Date", employee.Booking_Date);
                cmd.Parameters.AddWithValue("@Event_Name", employee.Event_Name);
                cmd.Parameters.AddWithValue("@Event_Date", employee.Event_Date);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@BloodGroup", employee.BloodGroup);
                cmd.Parameters.AddWithValue("@Mobile_No", employee.Mobile_No);
                cmd.Parameters.AddWithValue("@Status", employee.Status);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    msg = "Data has been inserted";
                }
                else
                {
                    msg = "Error";
                }
            }
            return msg;
        }

        // PUT api/values/5
        public String Put(int id, Employee employee)
        {
            string msg = "";

            if (employee != null)
            {
                SqlCommand cmd = new SqlCommand("Use_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Customer_ID", id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Email_ID", employee.Email_ID);
                cmd.Parameters.AddWithValue("@Booking_Date", employee.Booking_Date);
                cmd.Parameters.AddWithValue("@Event_Name", employee.Event_Name);
                cmd.Parameters.AddWithValue("@Event_Date", employee.Event_Date);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@BloodGroup", employee.BloodGroup);
                cmd.Parameters.AddWithValue("@Mobile_No", employee.Mobile_No);
                cmd.Parameters.AddWithValue("@Status", employee.Status);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    msg = "Data has been updated";
                }
                else
                {
                    msg = "Error";
                }
            }
            return msg;
        }

        // DELETE api/values/5
        public string Delete(int id)
        {
            string msg = "";


            SqlCommand cmd = new SqlCommand("Use_DeleteEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Customer_ID", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                msg = "Data has been deleted";
            }
            else
            {
                msg = "Error";
            }
            return msg;
        }

    }
}
