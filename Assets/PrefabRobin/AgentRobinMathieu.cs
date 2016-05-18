using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentRobinMathieu : Entity
{

    [Header("IA")]

    public Vector3 startPoint;
    public GameObject nearestTarget;
    public BoxCollider nearTargetCollider;
    public List<GameObject> targets;
    public NavMeshAgent agent;
    bool hasWin = false;

    [Header("Values")]

    float timeFreezeEnemy = 1.0f;
    public bool isShooting = true;
    public static string playerID = "Squad Robin";
    TeamNumber parentNumber;

    protected override void Start()
    {

        parentNumber = transform.parent.parent.GetComponent<TeamNumber>();
        parentNumber.teamName = playerID;
        startPoint = transform.position;
        targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
        agent = GetComponent<NavMeshAgent>();

        for (int i = targets.Count - 1; i >= 0; --i)
        {
            if (targets[i].name == "Robin")
            {
                targets.Remove(targets[i]);
            }
        }
        //InvokeRepeating("Gagne", 0.0f, 1.5f);
        base.Start();
    }

    void Gagne()
    {
        bool isWin = true;
        if (isWin && !hasWin)
        {
            hasWin = true;
            Debug.Log("J'ai gagné : Robin MATHIEU");
        }
    }

    protected virtual void UpdateTarget()
    {

        GameObject target = null;

        for (int i = targets.Count - 1; i >= 0; --i)
        {
            if (target == null || (target && Vector3.Distance(target.transform.position, transform.position) > Vector3.Distance(targets[i].transform.position, transform.position)))
            {
                target = targets[i];
            }
        }
        if (target)
        {
            nearestTarget = target;
            nearTargetCollider = target.GetComponent<BoxCollider>();
        }
        else
        {
            nearestTarget = null;
            nearTargetCollider = null;
        }
    }

    protected virtual void UpdateRoad()
    {
        agent.SetDestination(targets[Random.Range(0, targets.Count - 1)].transform.position);
    }

    IEnumerator FreezeEnemy(GameObject enemy)
    {
        enemy.GetComponent<NavMeshAgent>().Stop();

        yield return new WaitForSeconds(timeFreezeEnemy);

        enemy.GetComponent<NavMeshAgent>().Resume();
    }
}
