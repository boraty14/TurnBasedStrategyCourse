using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField][Range(0,20)] private float _moveSpeed;
    [SerializeField][Range(0,100)] private float _rotationSpeed;
    [SerializeField][Range(0,50)] private float _zoomAmount;
    [SerializeField][Range(0,20)] private float _zoomSpeed;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;

    private CinemachineTransposer _cinemachineTransposer;
    private Vector3 _targetFollowOffset;

    private void Start()
    {
        _cinemachineTransposer = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _targetFollowOffset = _cinemachineTransposer.m_FollowOffset;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        
        HandleMovement(deltaTime);
        HandleRotation(deltaTime);
        HandleZoom(deltaTime);
    }

    private void HandleZoom(float deltaTime)
    {
        _targetFollowOffset.y = Mathf.Clamp(_targetFollowOffset.y - Input.mouseScrollDelta.y * _zoomAmount,
            MIN_FOLLOW_Y_OFFSET,
            MAX_FOLLOW_Y_OFFSET);

        _cinemachineTransposer.m_FollowOffset = Vector3.Lerp(_cinemachineTransposer.m_FollowOffset, _targetFollowOffset,
            deltaTime * _zoomSpeed);
    }

    private void HandleRotation(float deltaTime)
    {
        Vector3 rotationVector = Vector3.zero;

        if (Input.GetKey(KeyCode.Q)) rotationVector.y -= 1f;
        if (Input.GetKey(KeyCode.E)) rotationVector.y += 1f;

        transform.eulerAngles += rotationVector * deltaTime * _rotationSpeed;
    }

    private void HandleMovement(float deltaTime)
    {
        Vector3 inputMoveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) inputMoveDirection.z += 1f;
        if (Input.GetKey(KeyCode.A)) inputMoveDirection.x -= 1f;
        if (Input.GetKey(KeyCode.S)) inputMoveDirection.z -= 1f;
        if (Input.GetKey(KeyCode.D)) inputMoveDirection.x += 1f;

        Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
        transform.position += moveVector * deltaTime * _moveSpeed;
    }
}
