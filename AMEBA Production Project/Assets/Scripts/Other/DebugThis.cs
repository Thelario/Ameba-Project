using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugThis : MonoBehaviour
{
    void Start()
    {
        Debug.Log(gameObject.name + transform.position);
    }
}
