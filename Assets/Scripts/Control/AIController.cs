using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 5f;
        [SerializeField] private float waypointStopTime = 5f;
        [SerializeField] private float waypointTolerance = .5f;
        [Range(0f, 1f)]
        [SerializeField] private float patrolSpeedFraction = 0.2f;

        private GameObject player;
        private Fighter aiFighter;
        private Health health;
        private Mover aiMover;

        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceLastStopped = Mathf.Infinity;
        private int patrolIndex = 0;
        private void Awake()
        {
            player = FindObjectOfType<PlayerController>().gameObject;
            aiFighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            aiMover = GetComponent<Mover>();
        }

        private void Start()
        {
            guardPosition = transform.position;
        }
        private void Update()
        {
            if (health.GetHealth() == 0)
            {
                return;
            }
            if (player == null)
            {
                return;
            }
            if (InAttackRangeOfPlayer() && aiFighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastStopped += Time.deltaTime;
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceLastStopped = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if (timeSinceLastStopped < waypointStopTime) return;
            aiMover.StartMoveAction(nextPosition, patrolSpeedFraction);
        }

        private Vector3 GetCurrentWaypoint()
        {
           return patrolPath.GetWaypoint(patrolIndex);
        }

        private void CycleWaypoint()
        {
            patrolIndex = patrolPath.GetNextIndex(patrolIndex);
        }

        private bool AtWaypoint()
        {
            return Vector3.Distance(transform.position, GetCurrentWaypoint()) < waypointTolerance;
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            aiFighter.Attack(player);
        }

        public float GetChaseDistance()
        {
            return chaseDistance;
        }
        private bool InAttackRangeOfPlayer()
        {
            //Checking if the player is in the attack range
            return Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
        }
    }
}