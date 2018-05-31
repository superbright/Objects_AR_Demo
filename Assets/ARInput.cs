using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityARInterface;

public class ARInput : ARBase
{

    [SerializeField]
    ARController _controller;

    public System.Action<RaycastHit> RaycastHit;

    // Use this for initialization
    void Start()
    {
        _controller = GetComponent<ARController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var camera = GetCamera();

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            int layerMask = 1 << LayerMask.NameToLayer("ARGameObject"); // Planes are in layer ARGameObject

            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, float.MaxValue, layerMask))
            {
                Debug.Log("Raycast Hit: " + rayHit.point);

                if (RaycastHit != null)
                    RaycastHit(rayHit);
            }
        }
    }
}
