using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockType : MonoBehaviour
{

    private static BlockType instance;

    public static BlockType getInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        instance = this;
        //type = MapEditor.getInstance().getSingleBlockChoice();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int type;
}
