using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Will_IA_M2 : MonoBehaviour {
    [Range(0,8)]
    public int id;
    string teamName;

    [Header("Walk")]
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    public  string graphName ="will";

    [Header("Shoot")]
    GameObject bullet;
    public float shootSpeed=1;

    [Header("Dodge")]
    public float distanceDodge=4;
    public float timeToPos=1;

    Rigidbody rigid;
    MainStateMachineWill machine;
    

	void Start () {
        rigid = GetComponent<Rigidbody>();
        bullet = Resources.Load("Bullet") as GameObject;
        teamName = GetComponentInParent<TeamNumber>().teamName;

        // STATE
        SW_Dodge dodge = new SW_Dodge(id, speed, distanceDodge, timeToPos, graphName);
        SW_Shoot shoot = new SW_Shoot(id, bullet, shootSpeed, dodge);
        //FightStateMachineWill fight = new FightStateMachineWill(id, shoot);
        SW_Walk walk = new SW_Walk(id, speed, closeEnoughRange, graphName);
        

        // ADD TRANSITION
        walk.transitions.Add(new TW_VisionOnTarget(id, shoot, true));
        shoot.transitions.Add(new TW_VisionOnTarget(id, walk, false));

        // INIT MACHINE
        machine = new MainStateMachineWill(id,walk);
    }
	
	void Update () {
        machine.execute();
        rigid.velocity = Vector3.zero;
    }

    public void shoot(GameObject targ)
    {
        Vector3 futureDist=Vector3.zero;
        float temps = Vector3.Distance(transform.position, targ.transform.position) / bullet.GetComponent<bulletScript>().speed;
        if (targ.GetComponent<NavMeshAgent>())
        {
            Vector3 speed = targ.GetComponent<NavMeshAgent>().velocity;
            speed.y = 0;
            futureDist = (targ.transform.forward * temps * speed.magnitude);
        }
        

        GameObject spawnedBullet = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
        spawnedBullet.transform.LookAt(targ.transform.position + futureDist);
        spawnedBullet.GetComponent<bulletScript>().launcherName = teamName;


        Physics.IgnoreCollision(GetComponent<BoxCollider>(), spawnedBullet.GetComponent<CapsuleCollider>());
        //Physics.IgnoreCollision(GetComponent<CharacterController>(), spawnedBullet.GetComponent<CapsuleCollider>());
    }

    public void move(Vector3 pos)
    {
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        //playerController.SimpleMove(target.transform.position.normalized * speed * Time.deltaTime);
        transform.LookAt(pos);
    }
}
