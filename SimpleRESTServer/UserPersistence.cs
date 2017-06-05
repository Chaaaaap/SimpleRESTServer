using SimpleRESTServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using System.Collections;

namespace SimpleRESTServer
{
    public class UserPersistence
    {

        private MySql.Data.MySqlClient.MySqlConnection conn;

        public UserPersistence()
        {
            string myConnectionString;
            myConnectionString = "server=databasetest.cgredk49egoe.eu-central-1.rds.amazonaws.com;uid=Mikkel;pwd=aln1poin;database=LalaDB";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public long saveUser(User userToSave)
        {
            string sqlString = "INSERT INTO users (username, password, firstname, lastname, token) VALUES" +
                "('" + userToSave.UserName + "','" + userToSave.Password + "','" + userToSave.FirstName + "','" + userToSave.LastName + "','" + userToSave.Token + "')";

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();

            long id = cmd.LastInsertedId;

            return id;
        }

        public User getUser(int id)
        {
            User user = new User();
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            string sqlString = "SELECT * FROM users WHERE id = " + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            mySQLReader = cmd.ExecuteReader();
            if(mySQLReader.Read())
            {
                user.Id = mySQLReader.GetInt32(0);
                user.UserName = mySQLReader.GetString(1);
                user.Password = mySQLReader.GetString(2);
                user.FirstName = mySQLReader.GetString(3);
                user.LastName = mySQLReader.GetString(4);
                user.Token = mySQLReader.GetString(5);

                return user;
            } else
            {
                return null;
            }
        }

        public ArrayList getUsers()
        {
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

        public bool deleteUser(int id)
        {
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

        public bool updateUser(int id, User user)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            string sqlString = "SELECT * FROM users WHERE id = " + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            mySQLReader = cmd.ExecuteReader();
            if (mySQLReader.Read())
            {
                mySQLReader.Close();

                sqlString = "UPDATE users SET username='" + user.UserName + "', password='" + user.Password + "', firstname='" 
                    + user.FirstName + "', lastname='" + user.LastName + "', token='" + user.Token + "WHERE id="+user.Id; 
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}