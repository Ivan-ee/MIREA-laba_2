using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] [Range(1, 20)] private float _shootForce;
    [SerializeField] [Range(1, 10)] private float _shootRange;
    [SerializeField] private LayerMask _enemyLayer;
    public void FindEnemy()
    {
        Collider2D nearestEnemy = Physics2D.OverlapCircle(transform.position, _shootRange, _enemyLayer);

        if (nearestEnemy != null)
        {
            Enemy enemy = nearestEnemy.GetComponent<Enemy>();
            Shoot(enemy);
        }
    }
    void Shoot(Enemy enemy)
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        Vector2 shootDirection = (enemy.transform.position - transform.position).normalized;
        bulletRigidbody.AddForce(shootDirection * _shootForce, ForceMode2D.Impulse);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _shootRange);
    }
}
