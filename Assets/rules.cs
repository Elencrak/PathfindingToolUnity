using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class rules : MonoBehaviour {

    private class CheatComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x < y)
                return -1;
            return 1;
        }
    }


    //private static rules _instance = new rules();
    private static rules _instance;
    public static rules getInstance()
    {
        return _instance;
    }

    private bool first = true;

    private SortedList<int, string> scoreBoard = new SortedList<int, string>(new CheatComparer());
    private Dictionary<string, int> killBoard = new Dictionary<string, int>(2);
    private Dictionary<string, int> deathBoard = new Dictionary<string, int>(2);
    private Dictionary<string, int> ffBoard = new Dictionary<string, int>(2);
    private Text scoreBoardDisplay;

	// Use this for initialization
	void Start ()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        scoreBoardDisplay = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<Text>();

        Invoke("stopMatch", 2*60);
    }

    void stopMatch()
    {
        Time.timeScale = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (first)
        {
            GameObject[] agents = GameObject.FindGameObjectsWithTag("Target");
            foreach(GameObject agent in agents)
            {                
                agent.AddComponent<rulesCollision>();           
            }
            first = false;
        }


        string txt = "";
        foreach(KeyValuePair<int, string> score in scoreBoard)
        {
            string teamName = score.Value;
            int killCount = (killBoard.ContainsKey(teamName)?killBoard[teamName]:0);
            int deathCount = (deathBoard.ContainsKey(teamName) ? deathBoard[teamName] : 0);
            int ffCount = (ffBoard.ContainsKey(teamName) ? ffBoard[teamName]:0);
            txt = score.Key + "\t\t\t\t" + killCount + "\t\t\t" + deathCount + "\t\t\t\t" + ffCount + "\t\t\t\t\t\t" + teamName + "\n" + txt;
        }
        txt = "score\t\tkills\t\tdeaths\t\t\"mistakes\"\t\t\tteam name\n" + txt;
        scoreBoardDisplay.text = "Scores : \n\n" + txt;
	}

    private int updateScore(string teamName, int delta)
    {
        int index = scoreBoard.IndexOfValue(teamName);
        int currentScore;
        if (index >= 0)
        {
            currentScore = scoreBoard.Keys[index];
            currentScore += delta;
            scoreBoard.RemoveAt(index);
        }
        else
        {
            currentScore = delta;
        }
        scoreBoard.Add(currentScore, teamName);
        return currentScore;
    }

    private void updateMap(Dictionary<string, int> map, string teamName)
    {
        int currentScore = 0;
        if (map.ContainsKey(teamName))
            currentScore = map[teamName];
        currentScore++;
        map[teamName] = currentScore;
    }

    private void addFF(string teamName)
    {
        updateMap(ffBoard,teamName);
        updateScore(teamName, -1);
    }

    private void addKill(string teamName)
    {
        updateMap(killBoard, teamName);
        updateScore(teamName, 1);
    }

    private void addDeath(string teamName)
    {
        updateMap(deathBoard, teamName);
        updateScore(teamName, -1);
    }

    public void score(GameObject target, GameObject bullet)
    {
        string sourceTeamName = bullet.GetComponent<bulletScript>().launcherName;
        string targetTeamName = target.GetComponentInParent<TeamNumber>().teamName;

        if (targetTeamName.Equals(sourceTeamName))
            addFF(sourceTeamName);
        else
        {
            addKill(sourceTeamName);
            addDeath(targetTeamName);
        }
    }
}