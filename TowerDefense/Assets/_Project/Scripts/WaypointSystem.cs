using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace DoomsDayDefense
{
    public class WaypointSystem : MonoBehaviour
    {
        public List<Transform> waypoints = new List<Transform>();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            if (waypoints[0]) Gizmos.DrawSphere(waypoints[0].position, 0.1f);
            for (int i = 0; i < waypoints.Count - 1; i++)
            {
                if (waypoints[i] && waypoints[i + 1])
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
            }
        }
    }
}
