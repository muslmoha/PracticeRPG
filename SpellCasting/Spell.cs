using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Combat;
using System;

namespace RPG.SpellCasting
{
    public class Spell : MonoBehaviour
    {
        private SpellData spell;
        private float spellStartTime;
        private bool isOnCooldown;
        // Start is called before the first frame update

        Quaternion rotation;
        Vector3 position;

        void Awake()
        {
            rotation = transform.rotation;
            position = transform.position;
        }

        void Start()
        {
            spellStartTime = Time.time;
            isOnCooldown = true;
        }

        void LateUpdate()
        {
            CheckIfIsOnCooldown();

            transform.rotation = rotation;
            transform.position = position;
        }

        private bool CheckIfIsOnCooldown()
        {
            if(Time.time >= spellStartTime + spell.coolDown) isOnCooldown = false;
            return isOnCooldown;
        }


        // Update is called once per frame
        void Update()
        {
            if (!spell) GetSpell();
        }

        public bool IsOnCooldown()
        {
            return isOnCooldown;
        }

        private void GetSpell()
        {
            spell = transform.parent.gameObject.GetComponent<SpellCastController>().GetSpellData();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<CombatTarget>()) return;
            var enemiesHit = other.gameObject.GetComponent<Health>();
            if (enemiesHit.IsDead()) return;
            enemiesHit.TakeDamage(spell.GetSpellDamage());
        }
    }
}