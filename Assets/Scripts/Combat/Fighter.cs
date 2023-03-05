using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        public event EventHandler OnPlayerAttack;
        [SerializeField] private float weaponRange = 2f;
        private Transform target;
        private Mover moverAgent;

        private void Awake()
        {
            moverAgent = GetComponent<Mover>();
        }
        private void Update()
        {
            if (target == null) return;
            if (!GetIsInRange())
            {
                moverAgent.MoveTo(target.position);
            }
            else
            {
                moverAgent.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            OnPlayerAttack?.Invoke(this, EventArgs.Empty);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) <= weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            
            target = combatTarget.transform;
            GetComponent<ActionScheduler>().StartAction(this);
        }
        public void Cancel()
        {
            //Canceling the attacking
            target = null;
        }

        //Animation Event
        private void Hit()
        {

        }
    }
}