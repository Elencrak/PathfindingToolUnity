using UnityEngine;
using System.Collections;

namespace JojoKiller { 
    public class Member : MonoBehaviour
    {
        public float fireRate;
        public float idleTime;
        public float walkTime;

        public float timerShoot;
        public float timerIdle;
        public float timerWalk;

        // Use this for initialization
        void Start()
        {
            timerShoot = fireRate;
            timerIdle = idleTime;
            timerWalk = walkTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (timerShoot > 0)
            {
                timerShoot -= Time.deltaTime;
            }

            if (timerIdle > 0)
            {
                timerIdle -= Time.deltaTime;
            }

            if (timerWalk > 0)
            {
                timerWalk -= Time.deltaTime;
            }
        }

        public bool canShoot()
        {
            return timerShoot <= 0;
        }

        public  bool changeToIdle()
        {
            return timerIdle <= 0;
        }

        public  bool changeToWalk()
        {
            return timerWalk <= 0;
        }

        // Execute les actions
        public void shoot()
        {
            Debug.Log("Tire bang!");
            timerShoot = fireRate;
        }

        public void walk()
        {
            Debug.Log("Move Walk!");
            timerWalk = walkTime;
        }

        public void idle()
        {
            Debug.Log("gogole");
            timerIdle = idleTime;
        }
    }
}
