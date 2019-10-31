using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{

    StateController stateControler;

    private Animator myAnimator;
    private int estado;
    public GameObject myVision;

    // Start is called before the first frame update
    void Start()
    {
        stateControler = GetComponent<StateController>();
        myAnimator = GetComponent<Animator>();
        estado = 0;
        InvokeRepeating("changeState", 4, 3);
    }

    // Update is called once per frame
    void Update()
    {
        myAnimator.SetInteger("Estado", estado);

        if (estado == 3)
        {
            myVision.SetActive(false);
        }
        else
        {
            myVision.SetActive(true);
        }

    }

    private void changeState()
    {
        if (estado == 4)
        {
            estado = 0;
        }
        else
        {
            estado++;
        }
    }
}
