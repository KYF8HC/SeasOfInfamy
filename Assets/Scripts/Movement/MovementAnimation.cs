using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Control;

namespace RPG.Movement
{
    public class MovementAnimation : MonoBehaviour
    {
        private const string PLAYER_ANIMATOR_FORWARD_SPEED = "forwardSpeed";

        private Animator playereAnimator;
        private NavMeshAgent playerAgent;
        private Vector3 playerVelocity;
        private Vector3 localVelocity;

        private void Awake()
        {
            playereAnimator = GetComponent<Animator>();
            playerAgent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            playerVelocity = playerAgent.velocity;
            localVelocity = transform.InverseTransformDirection(playerVelocity);
            playereAnimator.SetFloat(PLAYER_ANIMATOR_FORWARD_SPEED, localVelocity.z);
        }
    }
}