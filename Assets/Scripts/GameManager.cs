using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Occluder occluders;
    public Transform playerTransform = default;
    public bool silverKey = false;
    public bool goldenKey = false;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Mesh GetMallaOclusores()
    {
        List<Vector3> vertices = new List<Vector3>();
        occluders.GetEdges(vertices);

        List<Vector2> normals = new List<Vector2>();

        for (int i = 0; i < vertices.Count; i += 2)
        {
            //Debug.Log("V" + i + " --> " + vertices[i]);
            //Debug.Log("V" + (i + 1) + " --> " + vertices[i + 1]);

            normals.Add(vertices[i + 1]);
            normals.Add(vertices[i + 0]);
        }

        // Simple 1:1 index buffer
        int[] indices = new int[vertices.Count];
        for(int i = 0; i < vertices.Count; i++)
        {
            indices[i] = i;
        }

        Mesh mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetUVs(0, normals);
        mesh.SetIndices(indices, MeshTopology.Lines, 0);

        return mesh;
    }
}
