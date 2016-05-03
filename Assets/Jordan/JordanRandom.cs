using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JordanRandom : MonoBehaviour {


    private GameObject bullet;
    private List<GameObject> enemies;
    private Vector3 initPos;
    private Transform target;
    public List<Transform> points;
    private int count;
    private bool touchedEveryone = false;
    private NavMeshAgent nav;
    private float startFireCoolDown;
    private float fireCoolDown = 1.0f;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Bullet")
        {
            randomTarget();
        }

        if (col.tag == "Target")
            target = col.transform;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            if (col.gameObject.GetComponent<bulletScript>().launcherName != "Pelolance")
            {
                nav.Warp(initPos);
                randomTarget();
            }
        }
    }

    // Use this for initialization
    void Start()
    {

        bullet = Resources.Load("Bullet") as GameObject;

        initPos = this.transform.position;

        GameObject[] temp = GameObject.FindGameObjectsWithTag("Target");

        enemies = new List<GameObject>(temp);

        enemies.Remove(gameObject);
        GameObject tempGO = GameObject.Find("VieuxPlanqueAgent");
        enemies.Remove(tempGO);
        tempGO = GameObject.Find("NouveauPlanqueAgent");
        enemies.Remove(tempGO);

        count = 0;

        nav = gameObject.GetComponent<NavMeshAgent>();

        target = null;

        randomTarget();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, target.position - transform.position, out hit, 10000.0f);

        if (fireCoolDown + startFireCoolDown < Time.time && hit.collider.tag == "Target")
            fire();

        if (transform.position != target.position)
        {
            nav.SetDestination(target.position);
        }
    }

    void randomTarget()
    {
        target = enemies[Random.Range(0, enemies.Count)].transform;
    }

    void fire()
    {
        startFireCoolDown = Time.time;
        Object temp = Instantiate(bullet);
        ((GameObject)temp).transform.position = this.transform.position + this.transform.forward;
        ((GameObject)temp).transform.LookAt(target.position + target.forward * 3);
        ((GameObject)temp).GetComponent<bulletScript>().launcherName = "Pelolance";
    }
}
