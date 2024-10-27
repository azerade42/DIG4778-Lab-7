using System;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Collider2D))]
public class Target : MonoBehaviour, IDamageable
{
    [HideInInspector] public int PointValue;
    [HideInInspector] public float Size;
    [HideInInspector] public float MoveSpeed;
    [HideInInspector] public Color SpriteColor;

    private TargetController controller;
    private float maxXPos;

    private bool canMove = true;

    private void OnEnable()
    {
        EventManager.OnGameEnded += ToggleMovement;
        EventManager.OnGameRestarted += ToggleMovement;

    }

    private void OnDisable()
    {
        EventManager.OnGameEnded -= ToggleMovement;
        EventManager.OnGameRestarted -= ToggleMovement;
    }

    private void Start()
    {
        transform.localScale *= Size;
        GetComponent<SpriteRenderer>().color = SpriteColor;
    }

    public void Init(TargetController controller)
    {
        this.controller = controller;
        maxXPos = Mathf.Abs(controller.TargetStart.position.x);
    }

    private void ToggleMovement() => canMove = !canMove;

    private void FixedUpdate()
    {
        if (!canMove) return;

        transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);

        // Move target down and flip its movement if offscreen
        if (Mathf.Abs(transform.position.x) > maxXPos)
        {
            transform.position -= Vector3.up * controller.TargetMoveDownDist;
            MoveSpeed = -MoveSpeed;

            if (transform.position.y < 0)
                MissTarget();
        }
    }

    public virtual void TakeDamage()
    {
        // give the player points
        controller.Release(this);
        controller.TargetHit(this);
    }

    public void MissTarget()
    {
        EventManager.TargetMissed(this);
        controller.Release(this); // replace with Release() from object pool
    }
}
