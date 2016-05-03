using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JordanKamikazeAgent : MonoBehaviour {

    private GameObject bullet;
    private List<GameObject> enemies;
    private Vector3 initPos;
    public List<Transform> points;
    private int count;
    private bool touchedEveryone = false;
    private NavMeshAgent nav;
    private float startFireCoolDown;
    private float fireCoolDown = 1.0f;
    private Transform target;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            if (col.gameObject.GetComponent<bulletScript>().launcherName != "Pelolance")
            {
                target = col.gameObject.transform;
                fire();
                this.transform.position = initPos;
            }
        }
    }

    // Use this for initialization
    void Start () {
        bullet = Resources.Load("Bullet") as GameObject;

        initPos = this.transform.position;

        GameObject[] temp = GameObject.FindGameObjectsWithTag("Target");

        enemies = new List<GameObject>(temp);

        enemies.Remove(gameObject);

        count = 0;

        nav = gameObject.GetComponent<NavMeshAgent>();

        target = null;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x != points[0].position.x && transform.position.z != points[0].position.z)
        {
            nav.SetDestination(points[0].position);
        }
    }

    void fire()
    {
        startFireCoolDown = Time.time;
        Object temp = Instantiate(bullet);
        ((GameObject)temp).transform.position = this.transform.position - this.transform.forward;
        ((GameObject)temp).transform.LookAt(target.position - target.forward);
        ((GameObject)temp).GetComponent<bulletScript>().launcherName = "Pelolance";
    }
}
