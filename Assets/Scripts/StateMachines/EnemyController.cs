using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    StateController stateControler;

    [HideInInspector] public Unit unit;
    [HideInInspector] private Collider2D collider;
    
    [HideInInspector] public int nextWayPoint;

    [HideInInspector] public float timeWaited;
    [HideInInspector] public float waitTime;
    [HideInInspector] public bool wait;

    public GameObject excalmation;
    public GameObject interrogation;

    private Animator myAnimator;

    [Header("Patrol points")]
    public List<Transform> wayPointList;

    // Start is called before the first frame update
    void Awake()
    {
        stateControler = GetComponent<StateController>();
        collider = GetComponent<Collider2D>();
        unit = GetComponent<Unit>();

        waitTime = 2f;

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
            collider.enabled = true;
        }
        else
        {
            excalmation.SetActive(false);
            collider.enabled = false;
        }
            

        if (stateControler.currentState.name == "WaitingState") {
            interrogation.SetActive(true);
            if (!unit.followingPath)
                myAnimator.SetBool("walking", false);
        }
        else
            interrogation.SetActive(false);

        if (wait)
        {
            timeWaited += Time.deltaTime;
        }
    }


}
