using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common.Utility 
{
    public static class RaycastUtility
    {
        public static (bool, RaycastHit hit) IsHitFromMousePosition(LayerMask layerMask, RaycastHit hit)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask), hit);
        }

        public static Vector3 GetHitPosition(Vector3 origin, Vector3 direction, float distance, LayerMask layerMask)
        {
            RaycastHit hit;
            if (Physics.Raycast(origin, direction, out hit, distance, layerMask))
            {
                return hit.point;
            }
            return origin + direction * distance;
        }

        public static Vector3 GetHitPosition(Vector3 origin, Vector3 direction, float distance)
        {
            RaycastHit hit;
            if (Physics.Raycast(origin, direction, out hit, distance))
            {
                return hit.point;
            }
            return origin + direction * distance;
        }

        public static (bool, Vector3) GetMouseHitPosition()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                return (true, hit.point);
            }

            return (false, Vector3.zero);

        }

        public static (bool, GameObject) GetGameObject(LayerMask mask)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                return (true, hit.collider.gameObject);
            }

            return (false, null);
        }

        public static Vector2 GetMouseScreenToWorldPosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

    }
}


