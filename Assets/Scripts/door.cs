using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;

    public GameObject me;

    public Sprite spriteOpen;

    protected GameManager manager;

    protected bool inDoor = false;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        me = GetComponent<GameObject>();
        manager = GameManager.Instance;
    }

    protected virtual void Update()
    {
        UpdateDoor();
    }

    protected virtual void UpdateDoor()
    {
        if (inDoor && Input.GetKey(KeyCode.Space))
        {
            if (manager.goldenKey)
            {
                spriteRenderer.sprite = spriteOpen;
                manager.goldenKey = false;
                manager.CambiarEscena();
            }
            
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inDoor = true;
            collision.GetComponent<PlayerMovement>().action_btn.gameObject.SetActive(true);
            //Debug.Log("El jugador está en la puerta");
            
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            //Debug.Log("El jugador se ha alejado de la puerta");
            inDoor = false;
            collision.GetComponent<PlayerMovement>().action_btn.gameObject.SetActive(false);
        }
    }
}
