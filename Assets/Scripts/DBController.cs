﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DBController : MonoBehaviour
{
    public InputField inputArea;
    public InputField inName;
    public InputField inClass;
    public InputField inScore;
    public InputField inTime;

    public List<Text> PlayerNames = new List<Text>();
    public List<Text> PlayerClasses = new List<Text>();
    public List<Text> Scores = new List<Text>();
    public List<Text> Times = new List<Text>();

    public List<System.Tuple<string, string, double, double>> highscores;
    public int page = 0;


    DBInterface DBInterface;

    void Start()
    {
        DBInterface = FindObjectOfType<DBInterface>();
    }

    public void InsertHighscore(string Name, string Class,string level, double score, double time)
    {
        if (DBInterface == null)
        {
            Debug.LogError("UserInterface: Could not insert a highscore. DBIitefrace is not present.");
            return;
        }
        if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
        {
            Debug.LogError("UserInterface: Could not insert a highscore. PlayerName is empty.");
            return;
        }
        if (string.IsNullOrEmpty(Class) || string.IsNullOrWhiteSpace(Class))
        {
            Debug.LogError("UserInterface: Could not insert a highscore. PlayerName is empty.");
            return;
        }
        if (string.IsNullOrEmpty(level) || string.IsNullOrWhiteSpace(level))
        {
            Debug.LogError("UserInterface: Could not insert a highscore. PlayerName is empty.");
            return;
        }

        DBInterface.InsertHighscore(Name, Class,level,score,time);
        }
    public void RetrieveTopFiveHighscores(string area)
    {
        if (DBInterface == null)
        {
            Debug.LogError("UserInterface: Could not retrieve the top five highscores. DBIitefrace is not present.");
            return;
        }
        if (string.IsNullOrEmpty(area) || string.IsNullOrWhiteSpace(area))
        {
            Debug.LogError("UserInterface: Could not insert a highscore. PlayerName is empty.");
            return;
        }
        clearScoreboard();
        page = 0;
        highscores = DBInterface.RetrieveTopFiveHighscores(area);
        for (int i = 0; i < PlayerNames.Count; i++)
        {
            PlayerNames[i].text = highscores[i].Item1;
            PlayerClasses[i].text = highscores[i].Item2;
            Scores[i].text = highscores[i].Item3.ToString();
            Times[i].text = highscores[i].Item4.ToString();
        }
    }
    private void clearScoreboard()
    {
        foreach (Text playername in PlayerNames)
        {
            playername.text = "";
        }
        foreach (Text playerclass in PlayerClasses)
        {
            playerclass.text = "";
        }
        foreach (Text score in Scores)
        {
            score.text = "";
        }
        foreach (Text time in Times)
        {
            time.text = "";
        }
    }

    public void nxtPage()
    {
        page += 1;
        clearScoreboard();
        for (int i = 0; i < PlayerNames.Count; i++)
        {
            PlayerNames[i].text = highscores[(page * 2) + i].Item1;
            PlayerClasses[i].text = highscores[(page * 2) + i].Item2;
            Scores[i].text = highscores[(page * 2) + i].Item3.ToString();
            Times[i].text = highscores[(page * 2) + i].Item4.ToString();
        }
    }
    public void bckPage()
    {
        if (page <= 0)
        {

        }
        else
        {
            page += -1;
            clearScoreboard();
            for (int i = 0; i < PlayerNames.Count; i++)
            {
                PlayerNames[i].text = highscores[(page * 2) + i].Item1;
                PlayerClasses[i].text = highscores[(page * 2) + i].Item2;
                Scores[i].text = highscores[(page * 2) + i].Item3.ToString();
                Times[i].text = highscores[(page * 2) + i].Item4.ToString();
            }
        }
    }
}
