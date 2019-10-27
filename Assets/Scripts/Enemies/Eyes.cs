using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    public GameObject myVision;
    VisionSensor vision;

    public grassTiles grassTiles;

    Animator myAnimator;
    public GameObject target;
    public GameManager manager;

    private int estado;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        estado = 0;
        InvokeRepeating("changeState", 4, 3);
        vision = GetComponentInChildren<VisionSensor>();
        grassTiles.GetComponent<grassTiles>();
    }

    // Update is called once per frame
    void Update()
    {
        myAnimator.SetInteger("Estado", estado);

        if (estado == 3)
        {
            myVision.SetActive(false);
        }
        else {
            myVision.SetActive(true);
        }

        if (vision.IsTargetVisible(target.transform) && myVision.active && !grassTiles.playerHided)
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
        manager.awakeGhosts = true;
    }

    private void toSleepPhantomes() {
        manager.awakeGhosts = false;
    }
}
