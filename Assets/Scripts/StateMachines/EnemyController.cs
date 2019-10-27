using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    StateController stateControler;

    public GameObject excalmation;
    public GameObject interrogation;

    private Animator myAnimator;

    [Header("Patrol points")]
    public List<Transform> wayPointList;

    // Start is called before the first frame update
    void Awake()
    {
        stateControler = GetComponent<StateController>();
        stateControler.SetupAI(true, wayPointList);
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (stateControler.currentState.name == "Patrullar")
        {
            myAnimator.SetBool("walking", true);
        }

        if (stateControler.currentState.name == "FollowPlayer")
        {
            excalmation.SetActive(true);
            myAnimator.SetBool("walking", true);
        }
        else
            excalmation.SetActive(false);

        if (stateControler.currentState.name == "WaitingState") {
            interrogation.SetActive(true);
            if (!stateControler.unit.followingPath)
                myAnimator.SetBool("walking", false);
        }
        else
            interrogation.SetActive(false);
    }

}
