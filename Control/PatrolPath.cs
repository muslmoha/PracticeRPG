using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float gizmoRadius = 0.5f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i).position, gizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i).position, GetWaypoint(j).position);
            }
        }

        public int GetNextIndex(int i)
        {
            if(i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Transform GetWaypoint(int i)
        {
            return transform.GetChild(i);
        }
    }
}