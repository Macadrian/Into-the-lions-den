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
    
    public List<GameObject> patrolEnemies;
    public List<Transform> patrolEnemySpawnPoint;
    public List<GameObject> ghostEnemies;
    public List<Transform> ghostEnemySpawnPoint;

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
        for (int i = 0; i < patrolEnemies.Count; i++)
        {
            patrolEnemies[i].transform.position = patrolEnemySpawnPoint[i].position;
        }
        for (int i = 0; i < ghostEnemies.Count; i++)
        {
            ghostEnemies[i].transform.position = ghostEnemySpawnPoint[i].position;
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

    public void PauseGameForDialogs()
    {
        Time.timeScale = 0f;

        //Disable the scripts and object that continue
        GameObject[] baseEnemies = GameObject.FindGameObjectsWithTag("BaseEnemy");
        if (baseEnemies != null)
        {
            for (int i = 0; i < baseEnemies.Length; i++)
            {
                baseEnemies[i].GetComponent<Unit>().enabled = false;
            }
        }

        GameObject[] bichitos = GameObject.FindGameObjectsWithTag("Bichito");
        if (bichitos != null)
        {
            for(int i = 0; i < bichitos.Length; i++)
            {
                bichitos[i].GetComponent<Bug>().enabled = false;
            }
        }

        GameObject[] fantasmas = GameObject.FindGameObjectsWithTag("Ghost");
        if (fantasmas != null)
        {
            for (int i = 0; i < fantasmas.Length; i++)
            {
                fantasmas[i].GetComponent<Fantasma>().enabled = false;
            }
        }
    }

    public void PauseGame()
    {
        PauseGameForDialogs();

        //Enable panel for pause menu

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        //Enable the scripts and object that continue
        GameObject[] baseEnemies = GameObject.FindGameObjectsWithTag("BaseEnemy");
        if (baseEnemies != null)
        {
            for (int i = 0; i < baseEnemies.Length; i++)
            {
                baseEnemies[i].GetComponent<Unit>().enabled = true;
            }
        }

        GameObject[] bichitos = GameObject.FindGameObjectsWithTag("Bichito");
        if (bichitos != null)
        {
            for (int i = 0; i < bichitos.Length; i++)
            {
                bichitos[i].GetComponent<Bug>().enabled = true;
            }
        }

        GameObject[] fantasmas = GameObject.FindGameObjectsWithTag("Ghost");
        if (fantasmas != null)
        {
            for (int i = 0; i < fantasmas.Length; i++)
            {
                fantasmas[i].GetComponent<Fantasma>().enabled = true;
            }
        }
    }
}
