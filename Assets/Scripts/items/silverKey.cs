using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class silverKey : MonoBehaviour
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
            Debug.Log("El jugador ha cogido una llave plateada");
            manager.silverKey = true;
            Destroy(me);
        }
    }
}
