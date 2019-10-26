using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Occluder occluders;
    public Transform playerTransform = default;
    public bool silverKey = false;
    public bool goldenKey = false;

    public Transform spawnPoint;
    
    public List<GameObject> enemies;
    public List<Transform> enemySpawnPoint;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public void ResetLevel()
    {
        playerTransform.position = spawnPoint.position;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.position = enemySpawnPoint[i].position;
        }
    }

    public void CambiarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
