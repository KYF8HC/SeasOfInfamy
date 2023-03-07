using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        private GameObject player;
        private Fighter aiFighter;
        private void Start()
        {
            player = FindObjectOfType<PlayerController>().gameObject;
            aiFighter = GetComponent<Fighter>();
        }
        private void Update()
        {
            if (player == null) { return; }
            if (DistanceToPlayer() < chaseDistance)
            {
                if (aiFighter.CanAttack(player))
                {
                    aiFighter.Attack(player);
                }
            }
            else
            {
                aiFighter.Cancel();
            }
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }
}