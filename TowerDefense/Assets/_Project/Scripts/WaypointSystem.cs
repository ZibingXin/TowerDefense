/* File Name: WaypointSystem.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * List of waypoints for the enemies to follow.
 * Draw the gizmos to show the waypoints.
 * 
 */


using System.Collections.Generic;
using UnityEngine;

namespace DoomsDayDefense
{
    public class WaypointSystem : MonoBehaviour
    {
        // List of waypoints for the enemies to follow
        public List<Transform> waypoints = new List<Transform>();


        // Draw the gizmos to show the waypoints
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            // Draw the waypoints and the lines between them in a loop
            for (int i = 0; i < waypoints.Count - 1; i++)
            {
                Gizmos.DrawWireSphere(waypoints[i].position, 0.2f);
                if (waypoints[i] && waypoints[i + 1])
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
            }
        }
    }
}
