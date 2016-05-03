using UnityEngine;
using System.Collections;
using System;

public class Entity : MonoBehaviour {

    public int life;
    public Vector3 _posStart;
    public int maxLife = 10;

    // Use this for initialization
    protected virtual void Start () {
        _posStart = transform.position;
        life = maxLife;
	}

    public void Hit(int damage)
    {
        if(life - damage > 0)
        {
            life -= damage;
        }
        else
        {
            Respawn();
        }
    }

    void Respawn()
    {
        life = maxLife;
        GetComponent<NavMeshAgent>().Warp(_posStart);
    }
}
