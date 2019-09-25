using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Occluder : MonoBehaviour
{
    [SerializeField] bool draw = false;

    CompositeCollider2D compositeCollider;
    List<Vector2[]> paths = new List<Vector2[]>();

    private void Awake()
    {
        compositeCollider = GetComponent<CompositeCollider2D>();

        for (int i = 0; i < compositeCollider.pathCount; i++)
        {
            paths.Add(new Vector2[compositeCollider.GetPathPointCount(i)]);
            compositeCollider.GetPath(i, paths[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetEdges(List<Vector3> edges)
    {
        /*
         * Guarda en la lista los vertices que componen las aristas
         * de todos los obstaculos, de tal forma que, para almacenar un cubo
         * cuyos vertices son {v1,v2,v3,v4} en la lista guardará,
         * {v1,v2,v2,v3,v3,v4,v4,v1}
         */

        for (int indexPath = 0; indexPath < paths.Count; indexPath++)
        {
            for (int indexVertex = 0; indexVertex < paths[indexPath].Length; indexVertex++)
            {
                int nextIndexVertex = indexVertex + 1;
                nextIndexVertex = (nextIndexVertex == paths[indexPath].Length) ? 0 : nextIndexVertex;

                edges.Add(paths[indexPath][indexVertex]);
                edges.Add(paths[indexPath][nextIndexVertex]);
            }
        }
    }
}
