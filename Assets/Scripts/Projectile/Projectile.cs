using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    private Vector2 moveDir = new Vector2(0, -1);
    private float destroyHeight;
    [SerializeField] private float moveSpeed;
    

    public void Init(Vector2 moveDir)
    {
        this.moveDir = moveDir;
        destroyHeight = moveDir.normalized.y * 50f;
    }

    void FixedUpdate()
    {
        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.y) > destroyHeight)
            Destroy(gameObject); // replace with Release() function in object pool
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage();
            Destroy(gameObject);
        }
    }
}
