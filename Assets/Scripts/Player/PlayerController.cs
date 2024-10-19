using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController), typeof(ProjectileSpawner))]
public class PlayerController : MonoBehaviour, IDamageable
{
    // Events
    public Action OnPlayerDied;

    // Movement
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveBoundsX;

    private PlayerInputController inputController;
    private float moveDir;
    private float startYPos;

    // Shooting
    private ProjectileSpawner projectileSpawner;
    [SerializeField] private float shootCooldown; 

    // Lives
    private int lives;

    private void OnEnable()
    {
        inputController = GetComponent<PlayerInputController>();

        inputController.OnMoveInput += (float inputAxis) => moveDir = inputAxis;
        inputController.OnShootInput += Shoot;

        projectileSpawner = GetComponent<ProjectileSpawner>();
    }

    private void Start()
    {
        startYPos = transform.position.y;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float newXPos = transform.position.x + moveDir * moveSpeed * Time.deltaTime;
        float clampedXPos = Mathf.Clamp(newXPos, -moveBoundsX, moveBoundsX);
        transform.position = new Vector3(clampedXPos, startYPos, 0);
    }

    private void Shoot()
    {
        projectileSpawner.SpawnProjectile(Vector2.up, shootCooldown);
    }

    public void TakeDamage()
    {
        if (--lives <= 0)
        {
            OnPlayerDied?.Invoke();
        }
    }
}
