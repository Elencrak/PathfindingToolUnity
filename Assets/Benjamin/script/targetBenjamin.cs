using UnityEngine;
using System.Collections;

public class targetBenjamin : MonoBehaviour {

	void OnCollisionEnter(Collision col)
    {
        if (col.transform.GetComponent<bulletScript>())
            Destroy(gameObject);
    }
}
