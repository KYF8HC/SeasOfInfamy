using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        private enum DestinationIdentifier
        {
            PortalPairOne,
            PortalPairTwo
        }
        [SerializeField] private int sceneIDToLoad = 1;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private DestinationIdentifier destination;
        [SerializeField] private float fadeOutWaitingTime = 1f;
        [SerializeField] private float fadeInWaitingTime = 2f;
        [SerializeField] private float fadeWaitingTime = .5f;

        private Fader fader;
        private void Awake()
        {
            fader = FindObjectOfType<Fader>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>() != null)
            {
                StartCoroutine(Transition());
            }
        }
        private  IEnumerator Transition()
        {
            if(sceneIDToLoad < 0)
            {
                Debug.LogError("Scene to load not set.");
                yield break;
            }
            DontDestroyOnLoad(gameObject);

            yield return fader.FadeOut(fadeOutWaitingTime);
            yield return SceneManager.LoadSceneAsync(sceneIDToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            
            yield return new WaitForSeconds(fadeWaitingTime);
            yield return fader.FadeIn(fadeInWaitingTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
        }

        private Portal GetOtherPortal()
        {
            Portal[] portals = FindObjectsOfType<Portal>();
            foreach (Portal portal in portals)
            {
                if(portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }
    }
}