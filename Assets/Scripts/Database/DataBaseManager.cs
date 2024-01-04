using System;
using System.Data;
using ScriptableObjects;
using UnityEngine;
using MySql.Data.MySqlClient;
namespace Database
{
    public class DatabaseManager : MonoBehaviour
    {
        [SerializeField] private DatabaseInfo databaseInfo;
        [SerializeField] private GameConfig gameConfig;
        private MySqlConnection _connection;
        private void Awake()
        {
            ConnectToDatabase();
        }

        private void Start()
        {
            CheckAndUpdatePlayer(20);
        }

        private void ConnectToDatabase()
        {
            try
            {
                _connection = new MySqlConnection(databaseInfo.ConnectionString);
                _connection.Open();
                Debug.Log("Connected to the database!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error: {e.Message}");
            }
        }
        
        private void OnApplicationQuit()
        {
            if (_connection == null || _connection.State == ConnectionState.Closed) return;
            _connection.Close();
            Debug.Log("Connection closed.");
        }

        private void CheckAndUpdatePlayer(int userId)
        {
            try
            {
                if (IsPlayerExist(userId))
                {
                    if (GetPlayCount(userId) >= gameConfig.MaxNumberPlay)
                    {
                       return;
                    }
                    DateTime lastLogin = GetLastLogin(userId);
                    DateTime currentLocalTime = DateTime.Now;
                    
                    if (lastLogin.Date < currentLocalTime.Date)
                    {
                        ResetPlayCount(userId);
                        Debug.Log($"Player with ID {userId} exists. Reset play_count to 1 and updated last_login for a new day.");
                    }
                    else
                    {
                        IncrementPlayCount(userId);
                        Debug.Log($"Player with ID {userId} exists. Incremented play_count and updated last_login.");
                    }
                }
                else
                {
                    AddNewUser(userId);
                    Debug.Log($"Player with ID {userId} doesn't exist. Added to the database.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error checking and updating player: {e.Message}");
            }
        }

        private DateTime GetLastLogin(int userId)
        {
            string query = $"SELECT last_login FROM user_info WHERE id = {userId};";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            object result = cmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                return Convert.ToDateTime(result);
            }
            else
            {
                return DateTime.MinValue;
            }
        }
        
        private int GetPlayCount(int userId)
        {
            string query = $"SELECT play_count FROM user_info WHERE id = {userId};";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            object result = cmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                return -1;
            }
        }
        
        private void ResetPlayCount(int userId)
        {
            DateTime currentLocalTime = DateTime.Now;
            string formattedTime = currentLocalTime.ToString("yyyy-MM-dd HH:mm:ss");

            string query = $"UPDATE user_info SET play_count = 1, last_login = '{formattedTime}' WHERE id = {userId};";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.ExecuteNonQuery();
        }

        private void IncrementPlayCount(int userId)
        {
            DateTime currentLocalTime = DateTime.Now;
            string formattedTime = currentLocalTime.ToString("yyyy-MM-dd HH:mm:ss");

            string query = $"UPDATE user_info SET play_count = play_count + 1, last_login = '{formattedTime}' WHERE id = {userId};";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.ExecuteNonQuery();
        }

        private bool IsPlayerExist(int userId)
        {
            string query = $"SELECT COUNT(*) FROM user_info WHERE id = {userId};";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
        
        private void AddNewUser(int userId)
        {
            try
            {
                DateTime currentLocalTime = DateTime.Now;
                string formattedTime = currentLocalTime.ToString("yyyy-MM-dd HH:mm:ss");
                var query = $"INSERT INTO user_info (id, last_login, play_count, money) VALUES ({userId}, {Utils.Utils.GetCurrentLocalTime}, 0, 0.00);";
                var cmd = new MySqlCommand(query, _connection);
                cmd.ExecuteNonQuery();
                Debug.Log($"User with id {userId} added to the database.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error adding user: {e.Message}");
            }
        }
        
        
        private void DeleteUser(int userId)
        {
            try
            {
                var query = $"DELETE FROM user_info WHERE id = {userId};";
                var cmd = new MySqlCommand(query, _connection);
                cmd.ExecuteNonQuery();
                Debug.Log($"User with id {userId} deleted from the database.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error deleting user: {e.Message}");
            }
        }
    }
}