using UnityEngine;

namespace DoomsDayDefense
{
    public class WaypointManager : MonoBehaviour
    {
        public Transform[] waypoints;
        private static WaypointManager _instance;

        public static WaypointManager Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }

        public Vector3 GetWaypointPosition(int index)
        {
            return waypoints[index].position;
        }
    }
}
