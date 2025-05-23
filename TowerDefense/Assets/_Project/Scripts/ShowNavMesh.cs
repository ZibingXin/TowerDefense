/* File Name: ShowNavMesh.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * Create a mesh to show the NavMesh in MiniMap Camera.
 * 
 */

using UnityEngine;
using UnityEngine.AI;

namespace DoomsDayDefense
{
    public class ShowNavMesh : MonoBehaviour
    {
        void Start()
        {
            ShowMesh();
        }

        void ShowMesh()
        {
            // NavMesh.CalculateTriangulation returns a NavMeshTriangulation object.
            NavMeshTriangulation meshData = NavMesh.CalculateTriangulation();

            // Create a new mesh and chuck in the NavMesh's vertex and triangle data to form the mesh.
            Mesh mesh = new Mesh();
            mesh.vertices = meshData.vertices;
            mesh.triangles = meshData.indices;

            // Assigns the newly-created mesh to the MeshFilter on the same GameObject.
            GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}
