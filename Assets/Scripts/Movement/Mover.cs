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
        private bool isWalking;
        private NavMeshAgent moverAgent;

        private void Awake()
        {
            moverAgent = GetComponent<NavMeshAgent>();
        }

        public void StartMoveAction(Vector3 destination)
        {
            //To separate the simple movement action from the actual moving
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            //Actual moving
            moverAgent.isStopped = false;
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