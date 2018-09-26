using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleType : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        int.TryParse(name, out type);  //针对图块
        enemy_type = name.ToString();  //针对敌人
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int type;

    public string enemy_type;

    public void showType()
    {
        if (int.TryParse(name, out type))
        {
            //确定选择toggle的种类
            if (GetComponent<Toggle>().isOn)
            {
                //Debug.Log(type);
                MapEditor.getInstance().toggleChoice = type;
            }
        }
        else
        {
            if (GetComponent<Toggle>().isOn)
                EnemyEditor.getInstance().toggle_type = enemy_type;
        }
    }
}
