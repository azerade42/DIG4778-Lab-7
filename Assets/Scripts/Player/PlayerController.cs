using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController), typeof(ProjectileSpawner))]
public class PlayerController : MonoBehaviour, IDamageable, ISaveable
{
    private PlayerInputController inputController;

    // Movement
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveBoundsX;

    private float moveDir;
    private float startYPos = -4.5f;

    // Shooting
    private ProjectileSpawner projectileSpawner;
    [SerializeField] private float shootCooldown; 

    // Lives
    [SerializeField] private int startingLives;
    private int lives;

    private void OnEnable()
    {
        inputController = GetComponent<PlayerInputController>();

        inputController.OnMoveInput += UpdateMoveDir;
        inputController.OnShootInput += Shoot;

        projectileSpawner = GetComponent<ProjectileSpawner>();

        EventManager.OnTargetMiss += DamageOnMiss;

        SaveManager.Instance.SaveableObjects.Add(this);
    }

    private void OnDisable()
    {
        if (inputController != null)
        {
            inputController.OnMoveInput -= UpdateMoveDir;
            inputController.OnShootInput -= Shoot;
        }

        EventManager.OnTargetMiss -= DamageOnMiss;
    }

    private void Start()
    {
        lives = startingLives;
        EventManager.PlayerLivesUpdated(lives);
    }

    public void SaveData()
    {
        SaveManager.SaveData.PlayerPositionX = transform.position.x;
    }

    public void LoadData()
    {
        transform.position = new Vector3(SaveManager.SaveData.PlayerPositionX, startYPos, 0);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void UpdateMoveDir(float inputAxis) => moveDir = inputAxis;

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

    private void DamageOnMiss(Target target) => TakeDamage();

    public void TakeDamage()
    {
        if (--lives <= 0)
        {
            EventManager.PlayerDied();
        }

        EventManager.PlayerLivesUpdated(lives);
    }
}
