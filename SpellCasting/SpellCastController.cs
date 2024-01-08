using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SpellCasting
{
    public class SpellCastController : MonoBehaviour
    {
        ///private Animator spellAnim;
        private SpellData spellToCast;
        private GameObject instantiatedSpell;
        private ParticleSystem ps;
        //getting reset to default value when OnTriggerEnter is called, why?
        //Add timer for spellCD and Collider disabling
        //Add DOT
        private float spellStartTime;
        //Default values
        [SerializeField] private float castDelay = 1f;
        private float defaultcolliderDuration;
        private float defaultSpellCD = 4f;
        private float maxPSDuration = 0;

        private void GetMaxDuration()
        {
            foreach(var _ps in spellToCast.spellPrefab.GetComponentsInChildren<ParticleSystem>())
            {
                if (_ps.main.duration > maxPSDuration) maxPSDuration = _ps.main.duration;
                _ps.loop = false;
            }
        }

        public SpellData GetSpellData()
        {
            return spellToCast;
        }

        public void SetSpellAndCast(SpellData spell, Vector3 castPoint, Quaternion rotation)
        {
            spellToCast = spell;
            GetMaxDuration();
            ps = spell.spellPrefab.GetComponentInChildren<ParticleSystem>();

            instantiatedSpell = Instantiate(spell.spellPrefab.gameObject, castPoint, rotation);
            instantiatedSpell.transform.parent = gameObject.transform;

            ps.Play();

            Destroy(instantiatedSpell, maxPSDuration);

        }

        //just in case
        public void DestroySpellInstance(GameObject toDestroy, float timeToRemain = 0f)
        {
            Destroy(toDestroy, timeToRemain);
        }
    }
}