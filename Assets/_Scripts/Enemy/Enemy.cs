using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    public static UnityEvent<Enemy> OnEnemyDestroyedEvent = new UnityEvent<Enemy>();
    
    [SerializeField] [Range(0, 10)] protected int _health;
    [SerializeField] [Range(0, 5)] protected float _speed;
    [SerializeField] [Range(1, 10)] protected int _damage;
    [SerializeField] protected GameObject _damageEffect;
    [SerializeField] protected float _attackCooldown ;

    protected GameObject _player;
    protected NavMeshAgent _agent;
    protected Animator _animator;
    
    protected float _currentCooldown;
    protected abstract void Attack(PlayerController player);
    public abstract void TakeDamage(int damage);
    protected abstract void SetAnimation();
    protected abstract void CheckHealth();
    protected abstract IEnumerator Dead();
    protected abstract  void OnDestroy();
}
