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


    private static DBInterface saveInstance;

    void Start()
    {
        stringBuilder = new MySqlConnectionStringBuilder();
        stringBuilder.Server = Server;
        stringBuilder.Database = Database;
        stringBuilder.UserID = UserID;
        stringBuilder.Password = Password;
    }

    public void InsertHighscore(string playerName, string playerClass, string levelID, double playerScore, double playerTime)
    {
        using (MySqlConnection connection = new MySqlConnection(stringBuilder.ConnectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO scoreboard (PlayerName, PlayerClass, LevelID, PlayerScore, PlayerTime) VALUES (@playerName, @playerClass, @levelID, @playerScore, @playerTime)";
                command.Parameters.AddWithValue("@playerName", playerName);
                command.Parameters.AddWithValue("@playerClass", playerClass);
                command.Parameters.AddWithValue("@levelID", levelID);
                command.Parameters.AddWithValue("@playerScore", playerScore);
                command.Parameters.AddWithValue("@playerTime", playerTime);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("DBInterface: Could not insert the highscore! " + System.Environment.NewLine + ex.Message);
            }
        }
    }

    public List<System.Tuple<string, string, double, double>> RetrieveTopFiveHighscores(string level)
    {
        List<System.Tuple<string, string, double, double>> topFive = new List<System.Tuple<string, string, double, double>>();
        using (MySqlConnection connection = new MySqlConnection(stringBuilder.ConnectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT PlayerName, PlayerClass, PlayerScore, PlayerTime FROM scoreboard WHERE LevelID = \"" + level + "\"  ORDER BY PlayerScore DESC, PlayerTime ASC LIMIT 15";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var ordinal = reader.GetOrdinal("PlayerName");
                    string PlayerName = reader.GetString(ordinal);
                    ordinal = reader.GetOrdinal("PlayerClass");
                    string PlayerClass = reader.GetString(ordinal);
                    ordinal = reader.GetOrdinal("PlayerScore");
                    double PlayerScore = reader.GetDouble(ordinal);
                    ordinal = reader.GetOrdinal("PlayerTime");
                    double PlayerTime = reader.GetDouble(ordinal);
                    System.Tuple<string, string, double, double> entry = new System.Tuple<string, string, double, double>(PlayerName, PlayerClass, PlayerScore, PlayerTime);
                    topFive.Add(entry);
                }
                connection.Close();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("DBInterface: Could not retrieve the top five highscores! " + System.Environment.NewLine + ex.Message);
                return null;
            }
        }
        return topFive;
    }


    void Awake()
    {
        DontDestroyOnLoad(this);

        if (saveInstance == null)
        {
            saveInstance = this;
        }
        else
        {
            Destroy(gameObject); // Used Destroy instead of DestroyObject
        }
    }
}
