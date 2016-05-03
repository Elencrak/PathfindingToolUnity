using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JordanAgentNPlanque : MonoBehaviour {

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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            if(col.gameObject.GetComponent<bulletScript>().launcherName != "Pelolance")
                nav.Warp(initPos);
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

        count = 0;

        nav = gameObject.GetComponent<NavMeshAgent>();

        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, points[1].position - transform.position, out hit, 10000.0f);
        if (hit.collider.tag == "Target" && hit.collider.transform.parent.name != "Pelolance")
        {
            target = hit.collider.gameObject.transform;
        }

        if (fireCoolDown + startFireCoolDown < Time.time && target != null)
            fire();

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
        ((GameObject)temp).transform.LookAt(target.position + target.forward * 3);
        ((GameObject)temp).GetComponent<bulletScript>().launcherName = "Pelolance";
    }

}
