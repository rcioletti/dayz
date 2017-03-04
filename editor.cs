using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkServer;
using GTANetworkShared;

namespace DayZ
{
    class EditorManager : Script
    {
        //SANCHEZ SPAWN POINTS
        Vector3[] sanchezspawns = new[] { new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f) };

        //DUKES SPAWN POINTS
        Vector3[] dukesspawns = new[] { new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f) };

        //SPAWNPOINT ITEMS	
        public static readonly Vector3[] residential = new[] { new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f) };
        public static readonly Vector3[] industrial = new[] { new Vector3(-31.84, -1646.87, 28.20f), new Vector3(12.28f, -1605.95f, 28.50f), new Vector3(-40.78f, -1675.09f, 28.40f), new Vector3(-19.08f, -1674.98f, 28.40f) };
        public static readonly Vector3[] farm = new[] { new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f) };
        public static readonly Vector3[] supermarket = new[] { new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f) };
        public static readonly Vector3[] military = new[] { new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f) };

        public EditorManager()
        {

        }

    }
}
