using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] float fadeTime;
        CanvasGroup canvasGroup;

        // Start is called before the first frame update
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutInstant()
        {
            canvasGroup.alpha = 1;
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)// alpha is not 1
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null; //waits one frame
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)// alpha is not 1
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null; //waits one frame
            }
        }
    }
}