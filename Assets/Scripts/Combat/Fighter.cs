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
        public event EventHandler OnPlayerStopAttack;
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttack = 1f;
        [SerializeField] private float baseDamage = 10f;

        private float fighterSpeedFraction =  1f;
        private float timeSinceLastAttack = Mathf.Infinity;
        private Health target;
        private Mover moverAgent;

        private void Awake()
        {
            moverAgent = GetComponent<Mover>();
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.GetHealth() == 0) return;
            if (!GetIsInRange())
            {
                moverAgent.MoveTo(target.transform.position, fighterSpeedFraction);
            }
            else
            {
                moverAgent.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack >= timeBetweenAttack)
            {
                //This will trigger the animation, which will then trigger the Hit() animation event.
                OnPlayerAttack?.Invoke(this, EventArgs.Empty);
                timeSinceLastAttack = 0f;
            }
            
        }

        //Animation Event
        private void Hit()
        {
            if(target == null) return;
            target.TakeDamage(baseDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }
        public bool CanAttack(GameObject combatTarget)
        {
            //Checks if the target is dead, if it is we can not attack it
            if (combatTarget == null) 
            {
                return false; 
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && targetToTest.GetHealth() != 0;
        }
        public void Attack(GameObject combatTarget)
        {
            target = combatTarget.GetComponent<Health>();
            GetComponent<ActionScheduler>().StartAction(this);
        }
        public void Cancel()
        {
            //Canceling the attacking
            OnPlayerStopAttack?.Invoke(this, EventArgs.Empty);
            moverAgent.Cancel();
            target = null;
        }
    }
}