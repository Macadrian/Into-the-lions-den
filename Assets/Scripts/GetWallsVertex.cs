using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWallsVertex : MonoBehaviour
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

    private void OnDrawGizmos()
    {
        if (paths.Count != 0 && draw)
        {
            for (int indexPath = 0; indexPath < paths.Count; indexPath++)
            {
               
                if (indexPath == 0) { Gizmos.color = Color.red; }
                else { Gizmos.color = Color.grey; }

                for (int indexVertex = 0; indexVertex < paths[indexPath].Length; indexVertex++)
                {
                    Gizmos.DrawSphere(paths[indexPath][indexVertex], 0.1f);
                }
            }
        }
    }
}
