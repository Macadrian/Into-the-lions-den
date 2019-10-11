using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;

    public Sprite spriteOpen;

    protected bool inDoor = false;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        UpdateDoor();
    }

    protected virtual void UpdateDoor()
    {
        if (inDoor && Input.GetKey(KeyCode.Space))
        {
            spriteRenderer.sprite = spriteOpen;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inDoor = true;
            collision.GetComponent<PlayerMovement>().action_btn.gameObject.SetActive(true);
            Debug.Log("El jugador está en la puerta");
            
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            Debug.Log("El jugador se ha alejado de la puerta");
            inDoor = false;
            collision.GetComponent<PlayerMovement>().action_btn.gameObject.SetActive(false);
        }
    }
}
