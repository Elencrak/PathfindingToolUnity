using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JordanAgent : MonoBehaviour {

    private List<GameObject> enemies;
    public List<Transform> points;
    private int count;
    private bool touchedEveryone = false;
    private NavMeshAgent nav;

    void OnEnterCollision(Collision col)
    {
        if(col.gameObject.tag == "Target")
        {
            if(enemies.Contains(col.gameObject))
            {
                AddPoint();
                enemies.Remove(col.gameObject);

                if (enemies.Count == 0)
                    touchedEveryone = true;
            }
        }
    }

	// Use this for initialization
	void Start () {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Target");

        enemies = new List<GameObject>(temp);

        enemies.Remove(gameObject);

        count = 0;

        nav = gameObject.GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!touchedEveryone)
        {
            switch (count)
            {
                case 0:
                    nav.SetDestination(points[count].position);
                    if (transform.position.x == points[count].position.x && transform.position.z == points[count].position.z)
                        count++;
                    break;
                case 1:
                    nav.SetDestination(points[count].position);
                    if (transform.position.x == points[count].position.x && transform.position.z == points[count].position.z)
                        count++;
                    break;
                case 2:
                    nav.SetDestination(points[count].position);
                    if (transform.position.x == points[count].position.x && transform.position.z == points[count].position.z)
                        count++;
                    break;
                case 3:
                    nav.SetDestination(points[count].position);
                    if (transform.position.x == points[count].position.x && transform.position.z == points[count].position.z)
                        count = 0;
                    break;
            }
        }
	}

    void AddPoint()
    {

    }
}
