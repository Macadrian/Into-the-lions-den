using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    StateController stateControler;

    [Header("Patrol points")]
    public List<Transform> wayPointList;

    // Start is called before the first frame update
    void Awake()
    {
        stateControler = GetComponent<StateController>();
        stateControler.SetupAI(true, wayPointList);
    }

}
