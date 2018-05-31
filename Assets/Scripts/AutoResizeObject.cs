using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AutoResizeObject : MonoBehaviour
{

    public float TargetSize = 0.5f;

    [SerializeField] float scale = 0.5f;
    [SerializeField] Bounds bounds;
    [SerializeField] float max;

    private void Start()
    {
        StartCoroutine(CalculateBoundsAsync(() =>
        {
            if (Application.isPlaying)
            {
                Debug.Log("Setting scale to... " + scale);

                transform.localScale = new Vector3(scale, scale, scale);
            }
        }));
    }

    IEnumerator CalculateBoundsAsync(System.Action callback)
    {
        var this_mf = GetComponent<MeshRenderer>();
        if (this_mf == null)
            bounds = new Bounds(Vector3.zero, Vector3.zero);
        else
            bounds = this_mf.bounds;

        var child_meshes = GetComponentsInChildren<MeshRenderer>();
        foreach (var mr in child_meshes)
        {
            var child_bounds = mr.bounds;
            bounds.center += mr.transform.position;

            bounds.Encapsulate(child_bounds);
        }

        var v3 = bounds.size;
        max = Mathf.Max(Mathf.Max(v3.x, v3.y), v3.z);
        //max = (v3.x + v3.y + v3.z) / 3.0f;

        if (max == 0)
            scale = 1;
        else
            scale = 1 / max;

        callback();

        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}


