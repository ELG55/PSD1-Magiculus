using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class DBInterface : MonoBehaviour
{
    private MySqlConnectionStringBuilder stringBuilder;
    public string Server;
    public string Database;
    public string UserID;
    public string Password;


    void Start()
    {
        stringBuilder = new MySqlConnectionStringBuilder();
        stringBuilder.Server = Server;
        stringBuilder.Database = Database;
        stringBuilder.UserID = UserID;
        stringBuilder.Password = Password;
    }

    public void InsertHighscore(string playerName, string playerClass, int levelID, int playerScore, int playerTime)
    {
        using (MySqlConnection connection = new MySqlConnection(stringBuilder.ConnectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO scoreboard (PlayerName, PlayerClass, LevelID, PlayerScore, PlayerTime) VALUES (@playerName, @playerClass, @levelID, @playerScore, @playerTime)" ;
                command.Parameters.AddWithValue("@playerName", playerName);
                command.Parameters.AddWithValue("@highscore", playerClass);
                command.Parameters.AddWithValue("@highscore", levelID);
                command.Parameters.AddWithValue("@highscore", playerScore); 
                command.Parameters.AddWithValue("@highscore", playerTime);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("DBInterface: Could not insert the highscore! " + System.Environment.NewLine + ex.Message);
            }
        }
    }



}
