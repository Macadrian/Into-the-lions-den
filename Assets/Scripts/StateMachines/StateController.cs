using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{

    public State currentState;
    public State remainState;

    public EnemyController enemyController;
    public EyeController eyeController;

    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public VisionSensor vision;  

    [HideInInspector] public Transform playerTransform;
    
    public GameObject grass;

    private bool aiActive;

    void Awake()
    {
        gameManager = GameManager.Instance;
        vision = GetComponentInChildren<VisionSensor>();
        playerTransform = gameManager.playerTransform;
    }

    public void SetupAI(bool aiActivationFromEnemyController, List<Transform> wayPointsFromEnmeyController)
    {
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

    public bool PlayerInGrass()
    {
        return grass.GetComponent<grassTiles>().playerHided;
    }
}