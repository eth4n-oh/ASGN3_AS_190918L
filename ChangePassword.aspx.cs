using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASGN3_AS_190918L
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static byte[] Key = null;
        static byte[] IV = null;
        static byte[] nric = null;
        static string userid = null;
        static string finalHash;
        static string salt;
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
                            label_change_user.Text = reader["email"].ToString();

                            //cipherTextNRIC = (byte[])reader["Nric"];
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

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            string dbHash = getDBHash(userid);
            string dbSalt = getDBSalt(userid);
            string newpwd = HttpUtility.HtmlEncode(tb_change_newpwd.Text.ToString().Trim());
            string cfmpwd = HttpUtility.HtmlEncode(tb_change_cfmpwd.Text.ToString().Trim());
            if (newpwd == cfmpwd)
            {
                string pwd = cfmpwd;
                //Generate random "salt"
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] saltByte = new byte[8];
                //Fills array of bytes with a cryptographically strong sequence of random values.
                rng.GetBytes(saltByte);
                salt = Convert.ToBase64String(saltByte);
                SHA512Managed hashing = new SHA512Managed();
                string pwdWithSalt = pwd + salt;
                byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                finalHash = Convert.ToBase64String(hashWithSalt);
                RijndaelManaged cipher = new RijndaelManaged();

                updateAccount();
                Response.Redirect("Login.aspx", false);
            }
            else
            {
                lbl_pwdError.Text = "Both Password inputs do not match";
                lbl_pwdError.ForeColor = Color.Red;
            }

        }

        protected void updateAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Account SET passwordHash = @passwordHash, passwordSalt = @passwordSalt WHERE email = @email"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@email", Session["UserID"]);
                            cmd.Parameters.AddWithValue("@passwordHash", finalHash);
                            cmd.Parameters.AddWithValue("@passwordSalt", salt);
                            cmd.Connection = con;
                            try
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.ToString());
                            }
                            finally
                            {
                                con.Close();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
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
    }
}