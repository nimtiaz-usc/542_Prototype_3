using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalStrafe : MonoBehaviour
{
    [SerializeField] Transform leftPos;
    [SerializeField] Transform rightPos;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            // code goes here
        }
    }
}
