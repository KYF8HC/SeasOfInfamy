using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class AttackAnimation : MonoBehaviour
    {
        private const string ATTACK_ANIMATION_PLAY = "attack";
        private const string ATTACK_ANIMATION_STOP = "stopAttack";
        private Fighter fighter;
        private Animator animator;
        private void Awake()
        {
            fighter= GetComponent<Fighter>();
            animator= GetComponent<Animator>();
        }
        private void Start()
        {
            fighter.OnPlayerAttack += Fighter_OnPlayerAttack;
            fighter.OnPlayerStopAttack += Fighter_OnPlayerStopAttack;
        }

        private void Fighter_OnPlayerStopAttack(object sender, System.EventArgs e)
        {
            animator.ResetTrigger(ATTACK_ANIMATION_PLAY);
            animator.SetTrigger(ATTACK_ANIMATION_STOP);
        }

        private void Fighter_OnPlayerAttack(object sender, System.EventArgs e)
        {
            animator.ResetTrigger(ATTACK_ANIMATION_STOP);
            animator.SetTrigger(ATTACK_ANIMATION_PLAY);
        }
    }
}