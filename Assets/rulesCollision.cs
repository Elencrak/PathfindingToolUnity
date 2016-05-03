using UnityEngine;
using System.Collections;

public class rulesCollision : MonoBehaviour {

    Entity entity;

	// Use this for initialization
	void Start () {
        entity = GetComponent<Entity>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            entity.Hit(1);
            rules.getInstance().score(gameObject, collision.gameObject);
        }
    }
}
