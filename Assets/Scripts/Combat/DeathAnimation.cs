using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class DeathAnimation : MonoBehaviour
    {
        private const string DEATH_ANIMATION_TRIGGER = "die";
        private Animator animator;
        private Health characterHealth;
        private bool isDead = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            characterHealth = GetComponent<Health>();
        }
        private void Update()
        {
            if (animator == null)
            {
                return;
            }
            if (characterHealth == null)
            {
                return;
            }
            if (characterHealth.GetHealth() == 0 && !isDead)
            {
                isDead = true;
                animator.SetTrigger(DEATH_ANIMATION_TRIGGER);
            }
        }
    }
}