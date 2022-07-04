using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private Transform _bulletHitVfxPrefab;
    private Vector3 _targetPosition;

    private const float MoveSpeed = 150f;
    
    public void SetUp(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    private void Update()
    {
        Vector3 moveDirection = (_targetPosition - transform.position).normalized;
        float distanceBeforeMoving = Vector3.Distance(transform.position, _targetPosition);
        transform.position += moveDirection * MoveSpeed * Time.deltaTime;
        float distanceAfterMoving = Vector3.Distance(transform.position, _targetPosition);

        if (distanceBeforeMoving < distanceAfterMoving)
        {
            transform.position = _targetPosition;
            _trailRenderer.transform.parent = null;
            Destroy(gameObject);
            Instantiate(_bulletHitVfxPrefab, _targetPosition, Quaternion.identity);
        }
        
    }
}
