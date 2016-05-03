using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JordanAgent : MonoBehaviour {

    private List<GameObject> enemies;
    public List<Transform> points;
    private int count;
    private bool touchedEveryone = false;
    private NavMeshAgent nav;
	GameObject bullet;
	GameObject target;
	float timer = 1;

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
		bullet = Resources.Load ("Bullet") as GameObject;
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Target");

        enemies = new List<GameObject>(temp);

        enemies.Remove(gameObject);

        count = 0;

        nav = gameObject.GetComponent<NavMeshAgent>();
    }

	void ShootTest()
	{
		Vector3 asmodunk = this.gameObject.transform.position;
		asmodunk.y += 2;
		GameObject currentBullet = Instantiate (bullet, asmodunk, Quaternion.identity) as GameObject;
		target = GameObject.Find ("MiformatAgent-0");
		currentBullet.transform.LookAt (target.transform.position);
		currentBullet.GetComponent<bulletScript>().launcherName = "yolo";
	}

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) 
		{
			ShootTest ();
			timer = 1;
		}
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
                        count++;
                    break;
                case 4:
                    nav.SetDestination(points[count].position);
                    if (transform.position.x == points[count].position.x && transform.position.z == points[count].position.z)
                        count++;
                    break;
                case 5:
                    nav.SetDestination(points[count].position);
                    if (transform.position.x == points[count].position.x && transform.position.z == points[count].position.z)
                        count++;
                    break;
                case 6:
                    nav.SetDestination(points[count].position);
                    if (transform.position.x == points[count].position.x && transform.position.z == points[count].position.z)
                        count++;
                    break;
                case 7:
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
