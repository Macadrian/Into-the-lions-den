using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite spriteOpen;

    private bool inDoor = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(inDoor && Input.GetKey(KeyCode.Space))
        {
            spriteRenderer.sprite = spriteOpen;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inDoor = true;
            collision.GetComponent<PlayerMovement>().action_btn.gameObject.SetActive(true);
            Debug.Log("El jugador está en la puerta");
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            Debug.Log("El jugador se ha alejado de la puerta");
            inDoor = false;
            collision.GetComponent<PlayerMovement>().action_btn.gameObject.SetActive(false);
        }
    }
}
