using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private bool isTriggeredAlready = false;
        private void OnTriggerEnter(Collider other)
        {
            if (isTriggeredAlready) return;
            if (other.GetComponent<PlayerController>() != null)
            {
                isTriggeredAlready = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}