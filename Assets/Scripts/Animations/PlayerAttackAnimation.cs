using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class PlayerAttackAnimation : MonoBehaviour
    {
        private const string PLAYER_ANIMATOR_ATTACK = "attack";
        private void Start()
        {
            GetComponent<Fighter>().OnPlayerAttack += Fighter_OnPlayerAttack;
        }

        private void Fighter_OnPlayerAttack(object sender, System.EventArgs e)
        {
            GetComponent<Animator>().SetTrigger(PLAYER_ANIMATOR_ATTACK);
        }
    }
}