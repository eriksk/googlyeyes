using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    void Update()
    { 
        var mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        var targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPosition.z = Camera.main.transform.position.z;
        transform.LookAt(targetPosition);
    }
}
