using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private PlayerController player;
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            player = FindObjectOfType<PlayerController>();
        }

        private void EnableControl(PlayableDirector obj)
        {
            player.enabled = true;
        }

        private void DisableControl(PlayableDirector obj)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.enabled = false;
        }
    }
}