using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentAntoine : MonoBehaviour
{
    public GameObject target;
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    private Vector3 currentTarget;

    public GameObject[] enemies;

    public float rate = 1.0f;
    public float lastShoot = 0.0f;
    public bool canShoot = true;

    bool finished = false;

    public Material cube;

    public Vector3 SpawnPos;

    public GameObject[] points;
    public int index = 0;
    private Vector3 PathPoint;

    public GameObject bullet;
    public GameObject spawnBullet;
    public GameObject spawnBulletRotation;

    public AnimationCurve curve;

    private float offset = 0;

    // Use this for initialization
    void Start()
    {
        SpawnPos = transform.position;

        PathPoint = points[index].transform.position;

        InvokeRepeating("ChangeColor", 0.5f, 0.1f);

        InvokeRepeating("FindNewTarget", 0.5f, 0.5f);

        enemies = GameObject.FindGameObjectsWithTag("Target");

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == gameObject)
            {
                enemies[i] = null;
            }
        }

        GameObject lol = GameObject.Find("PointsAntoine");
        for(int i = 0; i<lol.transform.childCount; i++)
        {
            points[i] = lol.transform.GetChild(i).gameObject;
        }

        bullet = Resources.Load("Bullet") as GameObject;

        //FindNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        offset = 7 * Mathf.Sin(Time.time * 5);

        if (canShoot && target != null)
        {
            spawnBulletRotation.transform.LookAt(target.transform.position);
            GameObject go = Instantiate(bullet, spawnBullet.transform.position, Quaternion.identity) as GameObject;
            go.GetComponent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;
            go.transform.LookAt(target.transform.position);
            canShoot = false;
        }
        else
        {
            lastShoot += Time.deltaTime;
            if (lastShoot >= rate)
            {
                lastShoot = 0.0f;
                canShoot = true;
            }
            if(!target)
                spawnBulletRotation.transform.LookAt(PathPoint);
        }


        if (!finished)
        {
            if(Vector3.Distance(transform.position, PathPoint) <= 3f)
            {
                index++;
                if (index >= points.Length)
                    index = 0;
                PathPoint = points[index].transform.position;
            }
            GetComponent<NavMeshAgent>().SetDestination(PathPoint);
            transform.position = transform.position + /*new Vector3(0f, 0f, offset * 0.02f);*/ transform.right * offset * 0.02f;
        }
        else
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 50f);
        }
    }

    void ChangeColor()
    {
        if (!finished)
            GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f, 1), Random.Range(0f, 1), Random.Range(0f, 1));
    }

    void OnCollisionEnter(Collision other)
    {
        /*if(other.gameObject.tag == "Target" && other.gameObject == target)
        {
            FindInTargets(other.gameObject);
            FindNewTarget();
            if (target == null)
            {
                finished = true;
                GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Cube").transform.position);
                GetComponent<MeshRenderer>().material = cube;
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                Debug.Log("Antoine a posé ses couilles sur vos nez !");
            }
        }
        else
        {
            FindInTargets(other.gameObject);
        }*/

        if (other.gameObject.tag == "Bullet")
        {
            GetComponent<NavMeshAgent>().Warp(SpawnPos);
        }
    }

    /*void FindInTargets(GameObject obj)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (obj == enemies[i])
            {
                enemies[i] = null;
            }
        }
    }*/

    void FindNewTarget()
    {
        target = null;
        float dist = Mathf.Infinity;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                float tempDist = Vector3.Distance(transform.position, enemies[i].transform.position);
                if (enemies[i] == gameObject)
                {
                    enemies[i] = null;
                }
                else if (tempDist < dist)
                {
                    Vector3 fwd = enemies[i].transform.position - transform.position;
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, fwd, out hit) && hit.transform.tag == "Target")
                    {
                        target = enemies[i];
                        //transform.LookAt(target.transform.position);
                        spawnBulletRotation.transform.LookAt(target.transform.position);
                        dist = tempDist;
                    }
                }
            }
        }
    }
}