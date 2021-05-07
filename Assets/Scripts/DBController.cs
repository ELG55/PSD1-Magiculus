using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DBController : MonoBehaviour
{
    public List<Text> PlayerNames = new List<Text>();
    public List<Text> Scores = new List<Text>();
    public List<Text> Times = new List<Text>();
    public List<Text> Numbers = new List<Text>();

    public List<System.Tuple<string, string, double, double>> highscores;
    public int page = 0;

    public GameObject scoreboardButtonNext;
    public GameObject scoreboardButtonPrev;

    DBInterface DBInterface;

    void Start()
    {
        DBInterface = FindObjectOfType<DBInterface>();
    }
    private void Awake()
    {
        DBInterface = GameObject.Find("DBInterface").GetComponent<DBInterface>();
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
        if (highscores!=null)
        {
            for (int i = 0; i < PlayerNames.Count; i++)
            {
                if (i >= highscores.Count)
                {
                    PlayerNames[i].text = "";
                    Scores[i].text = "";
                    Times[i].text = "";
                    Numbers[i].text = "" + (i + 1);
                }
                else
                {
                    PlayerNames[i].text = highscores[i].Item1 + " (" + highscores[i].Item2 + ")";
                    Scores[i].text = highscores[i].Item3.ToString();
                    Times[i].text = highscores[i].Item4.ToString();
                    Numbers[i].text = "" + (i + 1);
                }
            }
        }
    }
    private void clearScoreboard()
    {
        foreach (Text playername in PlayerNames)
        {
            playername.text = "";
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
        if (page < 2)
        {
            page += 1;
            if (page >= 2)
            {
                scoreboardButtonNext.GetComponent<Button>().interactable = false;
            }
            if (page > 0)
            {
                scoreboardButtonPrev.GetComponent<Button>().interactable = true;
            }
            clearScoreboard();
            for (int i = 0; i < PlayerNames.Count; i++)
            {
                if (((page * PlayerNames.Count) + i) >= highscores.Count)
                {
                    PlayerNames[i].text = "";
                    Scores[i].text = "";
                    Times[i].text = "";
                    Numbers[i].text = "" + (((page * PlayerNames.Count) + i) + 1);
                }
                else
                {
                    PlayerNames[i].text = highscores[(page * PlayerNames.Count) + i].Item1 + " (" + highscores[(page * PlayerNames.Count) + i].Item2 + ")";
                    Scores[i].text = highscores[(page * PlayerNames.Count) + i].Item3.ToString();
                    Times[i].text = highscores[(page * PlayerNames.Count) + i].Item4.ToString();
                    Numbers[i].text = "" + (((page * PlayerNames.Count) + i) + 1);
                }
            }
        }
    }
    public void bckPage()
    {
        if (page > 0)
        {
            page += -1;
            if (page <= 0)
            {
                scoreboardButtonPrev.GetComponent<Button>().interactable = false;
            }
            if (page < 2)
            {
                scoreboardButtonNext.GetComponent<Button>().interactable = true;
            }
            clearScoreboard();
            for (int i = 0; i < PlayerNames.Count; i++)
            {
                if (((page * PlayerNames.Count) + i) >= highscores.Count)
                {
                    PlayerNames[i].text = "";
                    Scores[i].text = "";
                    Times[i].text = "";
                    Numbers[i].text = "" + (((page * PlayerNames.Count) + i) + 1);
                }
                else
                {
                    PlayerNames[i].text = highscores[(page * PlayerNames.Count) + i].Item1 + " (" + highscores[(page * PlayerNames.Count) + i].Item2 + ")";
                    Scores[i].text = highscores[(page * PlayerNames.Count) + i].Item3.ToString();
                    Times[i].text = highscores[(page * PlayerNames.Count) + i].Item4.ToString();
                    Numbers[i].text = "" + (((page * PlayerNames.Count) + i) + 1);
                }
            }
        }
    }
}
