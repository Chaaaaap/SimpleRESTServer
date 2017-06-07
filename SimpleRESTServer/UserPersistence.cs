using SimpleRESTServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using System.Collections;
using System.Configuration;

namespace SimpleRESTServer
{
    public class UserPersistence
    {

        public long saveUser(User userToSave)
        {
            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString = ConfigurationManager.ConnectionStrings["AWS"].ConnectionString;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();



                string sqlString = "INSERT INTO users (username, password, firstname, lastname, token) VALUES" +
                    "('" + userToSave.UserName + "','" + userToSave.Password + "','" + userToSave.FirstName + "','" + userToSave.LastName + "','" + userToSave.Token + "')";

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();

                long id = cmd.LastInsertedId;
                return id;

            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw e;
            }
            finally
            {
                conn.Close();

            }
        }

        public User getUser(int id)
        {
            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString = ConfigurationManager.ConnectionStrings["AWS"].ConnectionString;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();



                User user = new User();
                MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

                string sqlString = "SELECT * FROM users WHERE id = " + id.ToString();
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    user.Id = mySQLReader.GetInt32(0);
                    user.UserName = mySQLReader.GetString(1);
                    user.Password = mySQLReader.GetString(2);
                    user.FirstName = mySQLReader.GetString(3);
                    user.LastName = mySQLReader.GetString(4);
                    user.Token = mySQLReader.GetString(5);

                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public ArrayList getUsers()
        {
            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString = ConfigurationManager.ConnectionStrings["AWS"].ConnectionString;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                ArrayList userList = new ArrayList();

                MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

                string sqlString = "SELECT * FROM users";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {
                    User user = new User();
                    user.Id = mySQLReader.GetInt32(0);
                    user.UserName = mySQLReader.GetString(1);
                    user.Password = mySQLReader.GetString(2);
                    user.FirstName = mySQLReader.GetString(3);
                    user.LastName = mySQLReader.GetString(4);
                    user.Token = mySQLReader.GetString(5);

                    userList.Add(user);
                }
                return userList;
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool deleteUser(int id)
        {
            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString = ConfigurationManager.ConnectionStrings["AWS"].ConnectionString;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();



                MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

                string sqlString = "SELECT * FROM users WHERE id = " + id.ToString();
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    mySQLReader.Close();

                    sqlString = "DELETE FROM users WHERE id = " + id.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool updateUser(int id, User user)
        {
            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString = ConfigurationManager.ConnectionStrings["AWS"].ConnectionString;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();



                MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

                string sqlString = "SELECT * FROM users WHERE id = " + id.ToString();
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    mySQLReader.Close();

                    sqlString = "UPDATE users SET username='" + user.UserName + "', password='" + user.Password + "', firstname='"
                        + user.FirstName + "', lastname='" + user.LastName + "', token='" + user.Token + "WHERE id=" + user.Id;
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
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