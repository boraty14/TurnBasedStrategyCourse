using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Transform _bulletProjectilePrefab;
    [SerializeField] private Transform _shootPointTransform;
    [SerializeField] private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Shoot = Animator.StringToHash("Shoot");

    private void Start()
    {
        if (TryGetComponent<MoveAction>(out var moveAction))
        {
            moveAction.onStartMoving += OnStartMoving;
            moveAction.onStopMoving += OnStopMoving;
        }
        if (TryGetComponent<ShootAction>(out var shootAction))
        {
            shootAction.onShoot += OnShoot;
        }
    }

    private void OnShoot(ShootAction.OnShootEventArgs onShootEventArgs)
    {
        _animator.SetTrigger(Shoot);
        Transform bulletProjectileTransform = Instantiate(_bulletProjectilePrefab, _shootPointTransform.position, Quaternion.identity);
        var bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();

        Vector3 targetUnitShootAtPosition = onShootEventArgs.targetUnit.GetWorldPosition();
        targetUnitShootAtPosition.y = _shootPointTransform.position.y;
        
        bulletProjectile.SetUp(targetUnitShootAtPosition);
        
    }

    private void OnStartMoving()
    { 
        _animator.SetBool(IsWalking, true);
    }

    private void OnStopMoving()
    {
        _animator.SetBool(IsWalking, false);
    }
}