using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{

    public State currentState;
    public State remainState;

    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public VisionSensor vision;

    [HideInInspector] public Unit unit;

    [HideInInspector] public Transform playerTransform;

    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;   
    [HideInInspector] public float waitTime;

    private bool aiActive;

    void Awake()
    {
        gameManager = GameManager.Instance;
        vision = GetComponent<VisionSensor>();
        unit = GetComponent<Unit>();
        playerTransform = gameManager.playerTransform;
    }

    public void SetupAI(bool aiActivationFromEnemyController, List<Transform> wayPointsFromEnmeyController)
    {
        wayPointList = wayPointsFromEnmeyController;
        aiActive = aiActivationFromEnemyController;
    }

    void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }
}