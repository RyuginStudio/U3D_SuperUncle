using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStruct
{
    public float x;
    public float y;
    public int type;

    public MapStruct(float x, float y, int type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }
}
