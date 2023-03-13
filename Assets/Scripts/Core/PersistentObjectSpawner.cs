using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject persistentObjectPrefab;

        private static bool hasSpawned = false;
        private void Awake()
        {
            if (!hasSpawned)
            {
                SpawnPersistentObjects();
                hasSpawned = true;
            }
        }
        private void SpawnPersistentObjects()
        {
            GameObject persistentOjbect = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentOjbect);
        }
    }
}