﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace JojoBehaviourTree { 
    public class BehaviourTreeAgent : MonoBehaviour {


        [Header("Internal")]
        public List<Transform> targets = new List<Transform>();
        public Transform currentTarget;
        public NavMeshAgent currentNavMeshAgent;
        public float nextShoot;
        public float fireRate = 1f;

        private bool doOnce = true;
        private Selector rootSelector;
        public Vector3 startPosition;

        // Use this for initialization
        void Start () {
            startPosition = transform.position;

            nextShoot = fireRate;

            currentNavMeshAgent = GetComponent<NavMeshAgent>();

            rootSelector = new Selector();
            Sequence loadedSequence = new Sequence();
            Selector moveTo = new Selector();        
            Sequence seeSequence = new Sequence();
        
            SeeOpponent seeOpponent = new SeeOpponent(this);
            Shoot shoot = new Shoot(this);
            Move move = new Move(this);
            Wait wait = new Wait(this);
            Loaded loaded = new Loaded(this);

            rootSelector.addElementIncomposite(loadedSequence);
            rootSelector.addElementIncomposite(wait);

            loadedSequence.addElementIncomposite(loaded);
            loadedSequence.addElementIncomposite(moveTo);

            moveTo.addElementIncomposite(seeSequence);
            moveTo.addElementIncomposite(move);

            seeSequence.addElementIncomposite(seeOpponent);
            seeSequence.addElementIncomposite(shoot);

        }
	
	    // Update is called once per frame
	    void Update () {
            //Récupération des target
            if (doOnce)
            {
                doOnce = false;
                GameObject[] tempArray;
                tempArray = GameObject.FindGameObjectsWithTag("Target");
                foreach (GameObject g in tempArray)
                {
                    if (g.GetInstanceID() != gameObject.GetInstanceID())
                    {
                        if(!g.GetComponent<BehaviourTreeAgent>())
                            targets.Add(g.transform);
                    }
                }                        
            }

            if (nextShoot >= 0)
            {
                nextShoot -= Time.deltaTime;
            }
            rootSelector.execute();
        }

        public bool canShoot()
        {
            return nextShoot <= 0;
        }

        public void resetShoot()
        {
            nextShoot = fireRate;
        }

        void OnCollisionEnter(Collision collision)
        {
 
                currentNavMeshAgent.Warp(startPosition);
        }
    }
}