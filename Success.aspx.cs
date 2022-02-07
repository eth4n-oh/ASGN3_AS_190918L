using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASGN3_AS_190918L
{
    public partial class Success : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static byte[] Key = null;
        static byte[] IV = null;
        static byte[] nric = null;
        static string userid = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    userid = Session["UserID"].ToString();
                    displayUserProfile(userid);
                }
            } 
            else
            {
                Response.Redirect("Login.aspx", false);
            }
            //userid = Session["UserID"].ToString();
            //displayUserProfile(userid);
        }
        protected void displayUserProfile(string userid)
        {
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select * FROM ACCOUNT WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["email"] != DBNull.Value)
                        {
                            lbl_userEmail.Text = reader["email"].ToString();

                            //cipherTextNRIC = (byte[])reader["Nric"];
                        }
                        if (reader["firstName"] != DBNull.Value)
                        {
                            lbl_userfirstName.Text = reader["firstName"].ToString();

                            //cipherTextNRIC = (byte[])reader["Nric"];
                        }
                        if (reader["lastName"] != DBNull.Value)
                        {
                            lbl_userlastName.Text = reader["lastName"].ToString();

                            //cipherTextNRIC = (byte[])reader["Nric"];
                        }
                        if (reader["imageName"] != DBNull.Value)
                        {
                            image_User.ImageUrl = "~/Images/" + Path.GetFileName(reader["imageName"].ToString());

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            finally
            {
                connection.Close();
            }
        }

        protected void btn_logOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            Response.Redirect("Login.aspx", false);

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }

            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }
        }
        protected string getDBSalt(string email)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select passwordSalt FROM Account WHERE email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["passwordSalt"] != null)
                        {
                            if (reader["passwordSalt"] != DBNull.Value)
                            {
                                s = reader["passwordSalt"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;
        }
        protected string getDBHash(string email)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select passwordHash FROM Account WHERE email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["passwordHash"] != null)
                        {
                            if (reader["passwordHash"] != DBNull.Value)
                            {
                                h = reader["passwordHash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;
        }

        protected void btn_changePwd_Click(object sender, EventArgs e)
        {
            Session["UserID"] = userid;
            string guid = Guid.NewGuid().ToString();
            Session["AuthToken"] = guid;
            Response.Cookies.Add(new HttpCookie("AuthToken", guid));
            Response.Redirect("ChangePassword.aspx", false);
        }
    }
}