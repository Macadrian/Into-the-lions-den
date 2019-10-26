using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    StateController stateControler;

    public GameObject excalmation;
    public GameObject interrogation;

    [Header("Patrol points")]
    public List<Transform> wayPointList;

    // Start is called before the first frame update
    void Awake()
    {
        stateControler = GetComponent<StateController>();
        stateControler.SetupAI(true, wayPointList);
    }

    private void Update()
    {
        if (stateControler.currentState.name == "FollowPlayer")
            excalmation.SetActive(true);
        else
            excalmation.SetActive(false);

        if (stateControler.currentState.name == "WaitingState")
            interrogation.SetActive(true);
        else
            interrogation.SetActive(false);
    }

}
