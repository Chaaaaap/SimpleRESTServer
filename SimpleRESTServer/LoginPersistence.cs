using SimpleRESTServer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SimpleRESTServer
{
    public class LoginPersistence
    {
        string myConnectionString = ConfigurationManager.ConnectionStrings["AWS"].ConnectionString;

        public User Login(string username, string password)
        {
            MySql.Data.MySqlClient.MySqlConnection conn = null;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string sqlString = "SELECT * FROM users WHERE username='" + username + "'";

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                MySql.Data.MySqlClient.MySqlDataReader mySQLReader = cmd.ExecuteReader();

                User user = new User();

                if (mySQLReader.Read())
                {
                    user.Id = mySQLReader.GetInt32(0);
                    user.UserName = mySQLReader.GetString(1);
                    user.Password = mySQLReader.GetString(2);
                    user.FirstName = mySQLReader.GetString(3);
                    user.LastName = mySQLReader.GetString(4);
                    user.Token = mySQLReader.GetString(5);
                    mySQLReader.Close();

                    
                }

                if(user.Password.Equals(password) && user.UserName.Equals(username))
                {
                    DateTime latestLoginDate = DateTime.Today;
                    DateTime latestLoginTime = DateTime.Now.AddHours(2);
                    sqlString = "UPDATE users SET latestlogin='" + latestLoginDate.ToString("yyyy-MM-dd") +" " + latestLoginTime.ToString("HH:mm:ss") + "' WHERE id=" + user.Id;
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                    cmd.ExecuteNonQuery();
                    return user;
                } else
                {
                    return null;
                }

            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                conn.Close();
            }

        }
    }
}