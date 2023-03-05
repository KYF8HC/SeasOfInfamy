using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{

    public class Mover : MonoBehaviour
    {
        [SerializeField] private Transform targerTransform;
        private bool isWalking;
        private NavMeshAgent moverAgent;

        private void Awake()
        {
            moverAgent = GetComponent<NavMeshAgent>();
        }
        public void MoveTo(Vector3 destination)
        {
            moverAgent.destination = destination;
        }
        public NavMeshAgent GetMoverAgent()
        {
            return moverAgent;
        }
    }
}