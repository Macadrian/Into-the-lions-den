﻿using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{

    public State currentState;
    public State remainState;

    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public VisionSensor vision;

    [HideInInspector] public Unit unit;
    [HideInInspector] private Collider2D collider;

    [HideInInspector] public Transform playerTransform;

    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;  
    
    [HideInInspector] public float timeWaited;
    [HideInInspector] public float waitTime;
    [HideInInspector] public bool wait;

    public GameObject grass;

    private bool aiActive;

    void Awake()
    {
        gameManager = GameManager.Instance;
        vision = GetComponentInChildren<VisionSensor>();
        collider = GetComponent<Collider2D>();
        unit = GetComponent<Unit>();
        playerTransform = gameManager.playerTransform;

        waitTime = 2f;
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

        if (currentState.name != "FollowPlayer") collider.enabled = false;
        else collider.enabled = true;

        if (wait)
        {
            timeWaited += Time.deltaTime;
        }
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