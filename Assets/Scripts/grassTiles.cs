using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class grassTiles : MonoBehaviour
{
    private Tilemap myTilemap;
    private Color colorin;

    [HideInInspector] public bool playerHided;

    // Start is called before the first frame update
    void Start()
    {
        myTilemap = GetComponent<Tilemap>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("GrassMovement");
            colorin = myTilemap.color;
            colorin.a = 0.35f;
            myTilemap.color = colorin;
            playerHided = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            FindObjectOfType<AudioManager>().Play("GrassMovement");
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            colorin = myTilemap.color;
            colorin.a = 1f;
            myTilemap.color = colorin;
            playerHided = false;
        }
    }

}
