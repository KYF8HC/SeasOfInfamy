using RPG.Saving;
using RPG.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        private const string defaultSaveFile = "Saves";

        [SerializeField] private float fadeInTime = .5f;
        [SerializeField] private float waitBeforeFade = .5f;
        private IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return new WaitForSeconds(waitBeforeFade);
            yield return fader.FadeIn(fadeInTime);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }
}