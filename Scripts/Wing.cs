using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour
{
    public bool ownRotateSwitch = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ownRotateSwitch)
        {
            ownRotate(20);
        }

    }

    private void ownRotate(int angle)
    {
        if (ownRotateSwitch)
        {
            transform.Rotate(Vector3.forward, angle);
        }
    }
}
