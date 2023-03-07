using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private float waypointGizmoRadius = .3f;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Vector3 child = GetWaypoint(i);
                Gizmos.DrawSphere(child, waypointGizmoRadius);
                Gizmos.DrawLine(child, GetWaypoint(GetNextIndex(i)));
            }
        }
        public int GetNextIndex(int index)
        {
            if(index + 1 >= transform.childCount)
            {
                return 0;
            }
            return index + 1;
        }
        public Vector3 GetWaypoint(int index)
        {
            return transform.GetChild(index).position;
        }
    }
}