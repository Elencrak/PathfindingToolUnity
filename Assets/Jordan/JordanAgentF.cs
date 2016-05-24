using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JordanAgentF : MonoBehaviour {

    private StateMachineJordan statemachine;
    private float startAttack, delayAttack;
    private float delayDodge, startDelayDodge;
    private GameObject currentTarget, bullet;
    private List<GameObject> enemies;
    private Vector3 initPos;
    private bool bulletInRange;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            transform.position = initPos;
            currentTarget = enemies[Random.Range(0, enemies.Count)];
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            if(col.gameObject.GetComponent<bulletScript>().launcherName != "Pelolance")
                bulletInRange = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            bulletInRange = false;
        }
    }

    // Use this for initialization
    void Start ()
    {
        bulletInRange = false;
        startAttack = -10.0f;
        delayAttack = 1.0f;
        delayDodge = 0.1f;

        initPos = this.transform.position;

        TransitionJordan tAttack, tTrue, tStartDodge, tStopDodge;
        tAttack = new TransitionJordan(checkFireAttack);
        tTrue = new TransitionJordan(checkTrue);
        tStartDodge = new TransitionJordan(checkStartDodge);
        tStopDodge = new TransitionJordan(checkStopDodge);

        GameObject[] temp = GameObject.FindGameObjectsWithTag("Target");
        enemies = new List<GameObject>(temp);
        for(int i = 0; i<temp.Length; i++)
        {
            if (temp[i].name == "FWindy")
                enemies.Remove(temp[i]);
        }

        currentTarget = enemies[Random.Range(0, enemies.Count)];

        StateMoveJordan move = new StateMoveJordan();
        StateAttackJordanF attack = new StateAttackJordanF();
        StateDodgeJordanF dodge = new StateDodgeJordanF();
        move.setTarget(currentTarget);
        move.setTransform(transform);
        attack.setTarget(currentTarget);
        attack.setTransform(transform);
        dodge.setTarget(currentTarget);
        dodge.setTransform(transform);

        tTrue.setNextState(move);
        List<TransitionJordan> tempList = new List<TransitionJordan>();
        tempList.Add(tTrue);
        attack.setTransition(tempList);

        tAttack.setNextState(attack);
        tempList = new List<TransitionJordan>();
        tempList.Add(tAttack);
        move.setTransition(tempList);

        List<StateJordan> listState = new List<StateJordan>();
        listState.Add(move);
        listState.Add(attack);

        StateMachineJordan subStatemachine = new StateMachineJordan(listState, move);
        subStatemachine.setTarget(currentTarget);
        subStatemachine.setTransform(transform);

        tStopDodge.setNextState(subStatemachine);
        tempList = new List<TransitionJordan>();
        tempList.Add(tStopDodge);
        dodge.setTransition(tempList);

        tStartDodge.setNextState(dodge);
        tempList = new List<TransitionJordan>();
        tempList.Add(tStartDodge);
        subStatemachine.setTransition(tempList);

        listState = new List<StateJordan>();
        listState.Add(subStatemachine);
        listState.Add(dodge);

        statemachine = new StateMachineJordan(listState, subStatemachine);
        statemachine.setTarget(currentTarget);
        statemachine.setTransform(transform);

        bullet = Resources.Load("Bullet") as GameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (statemachine.getTarget() == null)
            statemachine.setTarget(currentTarget);

        statemachine.step();
	}

    public bool checkFireAttack()
    {
        if (startAttack + delayAttack < Time.time && Vector3.Distance(transform.position, currentTarget.transform.position) < 7.0f)
        {
            startAttack = Time.time;
            return true;
        }
        return false;
    }

    public void fire()
    {
        Object temp = Instantiate(bullet);
        ((GameObject)temp).transform.position = transform.position + transform.forward;
        ((GameObject)temp).transform.LookAt(currentTarget.transform.position);
        ((GameObject)temp).GetComponent<bulletScript>().launcherName = "Pelolance";
    }

    public bool checkStartDodge()
    {
        startDelayDodge = Time.time;
        return bulletInRange;
    }

    public bool checkStopDodge()
    {
        if(!bulletInRange || delayDodge + startDelayDodge < Time.time)
            return true;
        return false;
    }

    public bool checkTrue()
    {
        return true;
    }
}
