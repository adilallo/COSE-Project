using UnityEngine;

public class FlipNormals : MonoBehaviour
{
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            Mesh mesh = meshFilter.mesh;
            Vector3[] normals = mesh.normals;

            // Invert the normals
            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] = -normals[i];
            }

            mesh.normals = normals;

            // Reverse the order of triangles to maintain correct winding order
            int[] triangles = mesh.triangles;
            for (int i = 0; i < triangles.Length; i += 3)
            {
                int temp = triangles[i];
                triangles[i] = triangles[i + 2];
                triangles[i + 2] = temp;
            }

            mesh.triangles = triangles;
        }
        else
        {
            Debug.LogError("MeshFilter not found on the GameObject.");
        }
    }
}
