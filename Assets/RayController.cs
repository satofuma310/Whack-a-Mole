using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.5f);
            if (Physics.Raycast(ray,out hit))
            {
                if(hit.collider.transform.parent.TryGetComponent<HoleObject>(out var c))
                {
                    c.Hit();
                }
            }
        }
    }
}
