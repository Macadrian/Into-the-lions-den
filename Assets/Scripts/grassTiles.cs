using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class grassTiles : MonoBehaviour
{
    private Tilemap myTilemap;
    private Color colorin;

    // Start is called before the first frame update
    void Start()
    {
        myTilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Escondido");
            colorin = myTilemap.color;
            colorin.a = 0.35f;
            myTilemap.color = colorin;

        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Escondido");
            colorin = myTilemap.color;
            colorin.a = 1f;
            myTilemap.color = colorin;

        }
    }

}
