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
    private System.DateTime initialTime;
    public int currentCost = 0;

    private SortedList<int, GameObject> scoreBoard = new SortedList<int, GameObject>(new CheatComparer());
    private Text scoreBoardDisplay;

	// Use this for initialization
	void Start ()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        initialTime = System.DateTime.Now;
        scoreBoardDisplay = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        currentCost = (int)(System.DateTime.Now - initialTime).TotalSeconds;
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
        foreach(KeyValuePair<int, GameObject> score in scoreBoard)
        {
            txt = score.Value.name + "\t\t\t\t" + score.Key + "\n" + txt;
        }
        scoreBoardDisplay.text = "Scores : \n\n" + txt;
	}

    public long score(GameObject o1, GameObject o2)
    {
        int index = scoreBoard.IndexOfValue(o1);
        int currentScore;
        if (index >= 0)
        {
            currentScore = scoreBoard.Keys[index];
            currentScore += currentCost;
            scoreBoard.RemoveAt(index);
        }
        else
        {
            currentScore = currentCost;
        }
        scoreBoard.Add(currentScore, o1);
        return currentScore;
    }
}