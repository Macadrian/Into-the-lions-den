using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keys : MonoBehaviour
{
    public GameObject me;
    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
      manager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("PickedUpObject");
            Debug.Log("El jugador ha cogido una llave dorada");
            manager.goldenKey = true;
            Destroy(me);
        }
    }
}
