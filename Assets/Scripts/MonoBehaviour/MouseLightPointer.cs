using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Common.Utility;

public class MouseLightPointer : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private float _distance = 10f;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Vector3 hitPoint;
    

    void Update()
    {
        var hitPosition = RaycastUtility.GetMouseHitPosition();
        if (hitPosition.Item1)
        {
            _light.transform.position = hitPosition.Item2;
            hitPoint = hitPosition.Item2;
        }
        else
            _light.transform.position = hitPoint;

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.Instance.LoadScene("GamePlay");
        }

    }
}
