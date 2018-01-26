using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleType : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        int.TryParse(name, out type);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int type;

    public void showType()
    {
        //确定选择toggle的种类
        if (GetComponent<Toggle>().isOn)
        {
            //Debug.Log(type);
            MapEditor.getInstance().toggleChoice = type;
        }

    }
}
