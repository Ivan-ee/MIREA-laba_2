using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRanged : Enemy
{
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] [Range(1, 20)] private float _shootForce;
    [SerializeField] [Range(5, 30)] private float _attackDistance;
    
    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        
        _agent.speed = _speed;
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }
    void Update()
    {
        _agent.SetDestination(_player.transform.position);
        
        SetAnimation();
        
        FindPlayer();
    }
    void FindPlayer()
    {
        Collider2D unit = Physics2D.OverlapCircle(transform.position, _attackDistance, _playerMask);
        if (unit != null)
        {
            PlayerController player = unit.GetComponent<PlayerController>();
            if (_currentCooldown <= 0)
            {
                Attack(player);
            
                _currentCooldown = _attackCooldown;
            }
            else
            {
                _currentCooldown -= Time.deltaTime;
            }
        }
    }
    protected override void Attack(PlayerController player)
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        Vector2 shootDirection = (player.transform.position - transform.position).normalized;
        bulletRigidbody.AddForce(shootDirection * _shootForce, ForceMode2D.Impulse);
    }
    public override void TakeDamage(int damage)
    {
        Instantiate(_damageEffect, transform.position, Quaternion.identity);
        
        _health -= damage;
      
        CheckHealth();
    }
    protected override void SetAnimation()
    {
        _animator.SetFloat("Speed", _agent.speed);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
    }
    protected override void CheckHealth()
    {
        if (_health <= 0)
        {
            StartCoroutine(Dead());
        }
    }
    protected override IEnumerator Dead()
    {
        _animator.SetBool("Is Dead", true);
        
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
    protected override void OnDestroy()
    {
        OnEnemyDestroyedEvent?.Invoke(this);
    }
}
