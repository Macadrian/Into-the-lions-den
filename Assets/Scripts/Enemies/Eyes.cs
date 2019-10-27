using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    VisionSensor myVision;
    Animator myAnimator;
    public GameObject target;
    GameManager manager;

    private int estado;



    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        estado = 0;
        InvokeRepeating("changeState", 4, 3);
        myVision = GetComponent<VisionSensor>();

    }

    // Update is called once per frame
    void Update()
    {
        myAnimator.SetInteger("Estado", estado);

        if (estado == 3)
        {
            myVision.enabled = false;
            Debug.Log("VisionSensor off");
        }
        else {
            myVision.enabled = true;
            Debug.Log("VisionSensor on");
        }

        if (myVision.IsTargetVisible(target.transform))
        {
            awakePhantoms();
        }
        else {
            toSleepPhantomes();
        }
    }

    private void changeState()
    {
        if (estado == 4)
        {
            estado = 0;
        }
        else {
            estado++;
        }
    }

    private void awakePhantoms() {
        manager.awakePhantomes = true;
    }

    private void toSleepPhantomes() {
        manager.awakePhantomes = false;
    }
}
