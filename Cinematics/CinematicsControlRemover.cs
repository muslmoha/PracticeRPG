using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicsControlRemover : MonoBehaviour
    {
        PlayableDirector playableDirector;
        GameObject player;

        private void Start()
        {
            playableDirector = GetComponent<PlayableDirector>();
            player = GameObject.FindGameObjectWithTag("Player");
            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;

        }
        void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}