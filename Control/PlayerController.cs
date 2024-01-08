using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover mover;
        private Fighter fighter;
        private Health health;
        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if (health.IsDead()) return;
            if (InteractWithCombat()) return;
            if(InteractWithMovement()) return;
        }

        private Boolean InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!fighter.CanAttack(target.gameObject)) continue;

                if(Input.GetMouseButton(0))
                {
                    fighter.Attack(target.gameObject);
                }
                return true; //because if I can interact with combat I want to show a combat cursor. This allows for it, continue will skip over this if not target
            }
            return false;
        }

        private Boolean InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit); //passing the empty hit variable because the method will give what it hit into the hit variable

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.StartMoveAction(hit.point, 1f);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
