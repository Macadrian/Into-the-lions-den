using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celldoor : door
{
    public Sprite spriteOpenShadow;
    public SpriteRenderer spriteRendererShadow;
    [Header("Colliders")]
    public BoxCollider2D collider2DFront;
    public BoxCollider2D collider2DUnder;
    public BoxCollider2D collider2DHalfFront;
    public BoxCollider2D collider2DHalfUnder;
    [Header("Triggers")]
    public BoxCollider2D colliderTriggerOpen;
    [Space()]
    public bool isUnder = true;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isUnder)
        {
            collider2DUnder.enabled = true;
            collider2DFront.enabled = false;
            spriteRenderer.sortingOrder = 11;
        }
        else
        {
            collider2DUnder.enabled = false;
            collider2DFront.enabled = true;
            spriteRenderer.sortingOrder = 9;
        }
    }

    protected override void UpdateDoor()
    {
        if (inDoor && Input.GetKey(KeyCode.Space))
        {
            spriteRenderer.sprite = spriteOpen;
            spriteRendererShadow.sprite = spriteOpenShadow;
            colliderTriggerOpen.enabled = false;
            collider2DFront.enabled = false;
            collider2DUnder.enabled = false;

            if (isUnder)
            {
                collider2DHalfUnder.enabled = true;
            }
            else
            {
                collider2DHalfFront.enabled = true;
            }
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        UpdateDoor();
    }
}
