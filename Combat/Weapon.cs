using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/New Weapon" +
        "", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponDmg = 5f;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";

        public float GetWeaponDamage()
        {
            return weaponDmg;
        }

        public float GetweaponRange()
        {
            return weaponRange;
        }

        public void Spawn(Transform rightHand, Transform leftHand, Animator anim)
        {
            DestroyOldWeapon(rightHand, leftHand);
            if(equippedPrefab != null)
            {
                Transform handTransform = GetHandTransform(rightHand, leftHand);
                GameObject weapon=Instantiate(equippedPrefab, handTransform);
                weapon.name = weaponName;
            }
            var overrideController = anim.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                anim.runtimeAnimatorController = animatorOverride;
            }
            else if(overrideController != null)
            {
                anim.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
            
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetHandTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDmg);
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }
    }
}