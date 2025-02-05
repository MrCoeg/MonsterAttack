using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Common.Utility;

public class FogController : MonoBehaviour
{


    [SerializeField] private float delay = 0.1f;
    [SerializeField] private float difference = 0.1f;
    [SerializeField] private float maxFogEndDistance = 1f;

    private void FixedUpdate()
    {
        FogControllerUtility.IncreaseFogEndDistance(difference);
        if (FogControllerUtility.GetFogEndDistance() > maxFogEndDistance)
        {
            Destroy(this);
        }
    }
}
