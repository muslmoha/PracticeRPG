using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private bool hasBeenTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if (hasBeenTriggered || other.tag != "Player") return;
            GetComponent<PlayableDirector>().Play();
            hasBeenTriggered = true;
        }
    }
}