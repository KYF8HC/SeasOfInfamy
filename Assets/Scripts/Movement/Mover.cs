using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] float moveSpeedMax = 6f;

        private bool isWalking;
        private NavMeshAgent moverAgent;
        private Health health;
        private void Awake()
        {
            moverAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if(health.GetHealth() == 0)
            {
                moverAgent.enabled = false;
            }
        }
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            //To separate the simple movement action from the actual moving
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            //Actual moving
            moverAgent.isStopped = false;
            moverAgent.speed = moveSpeedMax * Mathf.Clamp01(speedFraction);
            moverAgent.destination = destination;
        }

        public void Cancel()
        {
            //Stoping the movement
            moverAgent.isStopped = true;
        }

        public NavMeshAgent GetMoverAgent()
        {
            return moverAgent;
        }
    }
}