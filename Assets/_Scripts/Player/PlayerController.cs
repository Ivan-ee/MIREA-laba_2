using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
   public static UnityEvent OnPlayerDeadEvent = new UnityEvent();
   
   [SerializeField] [Range(0,20)] private float _speed;
   [SerializeField] [Range(1,20)] private int _health;
   [SerializeField] private GameObject _damageEffect;
   [SerializeField] private float _attackCooldown;
   [SerializeField] private Joystick _joystick;
   
   private float _currentCooldown;
   
   private Animator _animator;
   private Gun _gun;
   private void Awake()
   {
      _animator = GetComponent<Animator>();
      _gun = GetComponentInChildren<Gun>();
   }
   private void FixedUpdate()
   {
      Move();
      Attack();
   }
   void Move()
   {
      float xMoveInput = _joystick.Horizontal;
      float yMoveInput = _joystick.Vertical;

      SetAnimation(xMoveInput, yMoveInput, new Vector2(xMoveInput, yMoveInput));
      
      transform.position += new Vector3(xMoveInput, yMoveInput, 0) * (_speed * Time.deltaTime);
   }
   void SetAnimation(float horizontal, float vertical, Vector2 direction)
   {
      _animator.SetFloat("Horizontal", horizontal);
      _animator.SetFloat("Vertical", vertical);
      _animator.SetFloat("Speed", direction.magnitude);
   }

   void Attack()
   {
      if (_currentCooldown <= 0)
      {
         _gun.FindEnemy();
            
         _currentCooldown = _attackCooldown;
      }
      else
      {
         _currentCooldown -= Time.deltaTime;
      }
   }
   public void TakeDamage(int damage)
   {
      PlayDamageEffect();
      
      _health -= damage;
      
      CheckHealth();
   }
   void CheckHealth()
   {
      if (_health <= 0)
      {
         StartCoroutine(Dead());
      }
   }
   void PlayDamageEffect()
   {
      var transformPos = transform.position;
      Instantiate(_damageEffect, new Vector3(transformPos.x, transformPos.y, transformPos.z - 15), Quaternion.identity);
   }
   IEnumerator Dead()
   {
      _animator.SetBool("Is Dead", true);
      
      yield return new WaitForSeconds(0.6f);
      
      OnPlayerDeadEvent?.Invoke();
   }
}
