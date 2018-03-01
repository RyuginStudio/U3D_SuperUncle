using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStruct
{
    public float x;
    public float y;
    public int type;
    public string blockEvent;
    public int doEventTimes;

    public MapStruct(float x, float y, int type, string blockEvent, int doEventTimes)
    {
        this.x = x;
        this.y = y;
        this.type = type;
        this.blockEvent = blockEvent;
        this.doEventTimes = doEventTimes;
    }
}
