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
    private Text scoreBoardDisplay;

	// Use this for initialization
	void Start ()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        scoreBoardDisplay = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<Text>();
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
            txt = score.Value + "\t\t\t\t" + score.Key + "\n" + txt;
        }
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

    public int score(GameObject target, GameObject bullet)
    {
        string sourceTeamName = bullet.GetComponent<bulletScript>().launcherName;

        int r = updateScore(sourceTeamName, 1);

        string targetTeamName = target.GetComponentInParent<TeamNumber>().teamName;

        updateScore(targetTeamName, -1);

        return r;
    }
}