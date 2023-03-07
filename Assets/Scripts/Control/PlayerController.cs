using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover playerMover;
        private Fighter playerFighter;
        private Health health;
        private void Awake()
        {
            playerMover = GetComponent<Mover>();
            playerFighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }
        void Update()
        {
            if(health.GetHealth() == 0)
            {
                return;
            }
            if (InteractWithCombat())
            {
                return;
            }
            if (InteractWithMovement())
            {
                return;
            }
            print("Nothing to do");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null)
                {
                    continue;
                }
                if (!playerFighter.CanAttack(target.gameObject))
                {
                    continue;
                }
                if (Input.GetMouseButton(0))
                {
                    //If the target is dead the character
                    //just going to move to the place we clicked.
                    //If it is not dead we are going to attack it.
                    playerFighter.Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    playerMover.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}