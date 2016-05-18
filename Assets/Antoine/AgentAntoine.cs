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

    public bool esquive;

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
        points = new GameObject[lol.transform.childCount];
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

            /*Vector3 D = target.transform.position - transform.position;
            Vector3 D2 = target.transform.position - transform.position;


            float top = (D.x * D2.x) + (D.y * D2.y) + (D.z * D2.z);
            float leftBot = Mathf.Sqrt((D.x * D.x) + (D.y * D.y) + (D.z * D.z));
            float rightBot = Mathf.Sqrt((D2.x * D2.x) + (D2.y * D2.y) + (D2.z * D2.z));
            float alpha = Mathf.Acos(top / (leftBot * rightBot));

            spawnBulletRotation.transform.Rotate(Vector3.up, alpha);*/

            GameObject go = Instantiate(bullet, spawnBullet.transform.position, Quaternion.identity) as GameObject;
            go.GetComponent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;
            go.transform.LookAt(target.transform.position/* + target.transform.forward*/);
            canShoot = false;
            lastShoot = 0.0f;
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
                index = Random.Range(0, points.Length);
                if (index >= points.Length)
                    index = 0;
                PathPoint = points[index].transform.position;
            }
            GetComponent<NavMeshAgent>().SetDestination(PathPoint);
            //transform.position = transform.position + /*new Vector3(0f, 0f, offset * 0.02f);*/ transform.right * offset * 0.02f;
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
            GetComponent<NavMeshAgent>().SetDestination(points[index].transform.position);
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

    public void Dodge(Vector3 pos, Vector3 v, Vector3 forward)
    {
        if (canShoot && (!target || Vector3.Distance(transform.position, target.transform.position) > Vector3.Distance(transform.position, pos)))
        {
            spawnBulletRotation.transform.LookAt(pos);
            GameObject go = Instantiate(bullet, spawnBullet.transform.position, Quaternion.identity) as GameObject;
            go.GetComponent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;
            go.transform.LookAt(pos + forward);
            canShoot = false;
        }
        else if(esquive != true)
        {
            esquive = true;
            StartCoroutine(Esquive(v));
        }
    }

    IEnumerator Esquive(Vector3 v)
    {
        GetComponent<NavMeshAgent>().SetDestination(transform.position + v * 10);
        yield return new WaitForSeconds(2f);
        GetComponent<NavMeshAgent>().SetDestination(points[index].transform.position);
        esquive = false;
    }
}