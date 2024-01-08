using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPerfab;
        static bool hasSpawned = false; //static variables live and die with the application

        private void Awake()
        {
            if (hasSpawned) return;
            

            SpawnPersistentObject();
            hasSpawned = true;
        }

        private void SpawnPersistentObject()
        {
            GameObject persistentObject = Instantiate(persistentObjectPerfab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}