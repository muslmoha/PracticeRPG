using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SpellCasting
{
    [CreateAssetMenu(fileName = "newSpell", menuName = "Data/Spell Data")]
    public class SpellData : ScriptableObject
    {
        [SerializeField] public SpellNamesEnum spellName;
        [SerializeField] private int id;
        [SerializeField] public GameObject spellPrefab;
        [SerializeField] public float colliderDurationTime;
        [SerializeField] private float damage = 20f;
        [SerializeField] public bool isDamageOverTime = false;
        [SerializeField] public float dotDuration;
        [SerializeField] public float castRange = 1000f;
        [SerializeField] public float coolDown = 15f;

        private float spellStartTime { get; set; }

        public float GetSpellDamage()
        {
            return this.damage;
        }

        public void SetSpellDamage(float newDamage) => damage = newDamage;

    }
}