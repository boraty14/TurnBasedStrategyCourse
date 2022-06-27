using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    [SerializeField] private LayerMask mousePlaneLayerMask;
    private static MouseWorld _instance;

    private void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        // transform.position = GetPosition();
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _instance.mousePlaneLayerMask);
        return hit.point;

    }
}
