using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Health target;
        private Mover mover;
        private Animator anim;
        private ActionScheduler actionScheduler;
        float timeSinceLastAttack = Mathf.Infinity;

        [SerializeField] float attackCD = 1f;
        [SerializeField] Transform rightHandTranform = null;
        [SerializeField] Transform leftHandTranform = null;
        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] string defaultWeaponName = "Unarmed";

        Weapon currentWeapon = null;


        private void Start()
        {
            mover = GetComponent<Mover>();
            anim = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();

            Weapon weapon = Resources.Load<Weapon>(defaultWeaponName);
            EquipWeapon(weapon);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (target.IsDead()) return;

            if (!GetIsRange())
            {
                mover.MoveTo(target.transform.position, 1f);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;

            Animator anim = GetComponent<Animator>();
            weapon.Spawn(rightHandTranform, leftHandTranform, anim);
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > attackCD)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            anim.ResetTrigger("stopAttack");
            anim.SetTrigger("attack");
        }

        //Animation Event
        void Hit()
        {
            if (target == null) return;
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTranform, leftHandTranform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.GetWeaponDamage());
            }
            
        }

        void Shoot()
        {
            Hit();
        }

        private bool GetIsRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetweaponRange();
        }

        public void Attack(GameObject target)
        {

            actionScheduler.StartAction(this);
            this.target = target.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return (targetToTest != null && !targetToTest.IsDead());
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            mover.Cancel();
        }

        private void StopAttack()
        {
            anim.SetTrigger("stopAttack");
            anim.ResetTrigger("attack");
        }
    }
}
