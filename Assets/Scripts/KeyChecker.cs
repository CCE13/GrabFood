using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyChecker : MonoBehaviour
{
    public List<string> keysAround;
    public LayerMask keyMask;
    // Start is called before the first frame update
    void Start()
    {
        AddKeyAround(Vector3.forward);
        AddKeyAround(Vector3.back);
        AddKeyAround(Vector3.left);
        AddKeyAround(Vector3.right);
    }
    private void AddKeyAround(Vector3 dir)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position,dir, out hit,1,keyMask);
        if(hit.collider != null)
        {
            keysAround.Add(hit.collider.name);
        }
    }
}
