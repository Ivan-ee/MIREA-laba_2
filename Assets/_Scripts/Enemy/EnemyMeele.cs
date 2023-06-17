using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeele : Enemy
{
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
    }
    protected override void Attack(PlayerController player)
    {
        if (_currentCooldown <= 0)
        {
            player.TakeDamage(_damage);
            
            _currentCooldown = _attackCooldown;
        }
        else
        {
            _currentCooldown -= Time.deltaTime;
        }
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
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Attack(col.gameObject.GetComponent<PlayerController>());
        }
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
