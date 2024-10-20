using System;
using System.Collections;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] Projectile projectileToSpawn; // replace with object pool
    [SerializeField] Vector3 spawnOffset;

    private bool onCooldown;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void SpawnProjectile(Vector2 moveDir, float cooldownTime)
    {
        if (onCooldown) return;

        Projectile projectile = Instantiate(projectileToSpawn, transform.position + spawnOffset, Quaternion.identity).GetComponent<Projectile>(); // replace with Get() object pool
        projectile.Init(moveDir);

        StartCoroutine(Countdown(cooldownTime, null));
    }

    private void SpawnProjectileRepeating(Vector2 moveDir, float repeatRate)
    {
        Projectile projectile = Instantiate(projectileToSpawn, transform.position + spawnOffset, Quaternion.identity).GetComponent<Projectile>(); // replace with Get() object pool
        projectile.Init(moveDir);

        StartCoroutine(Countdown(repeatRate, () => SpawnProjectileRepeating(moveDir, repeatRate)));
    }

    private IEnumerator Countdown(float countdownTime, Action endAction)
    {
        onCooldown = true;

        yield return new WaitForSeconds(countdownTime);
        endAction?.Invoke();

        onCooldown = false;
    }

}
