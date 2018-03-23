/*
 * 时间：2018年1月14日22:32:53
 * 作者：vszed
 * 功能：地图编辑器
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapEditor : MonoBehaviour
{
    private static MapEditor instance;

    public static MapEditor getInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        instance = this;

        createMask(64, 244);// row, column);

        MapStructList = new List<MapStruct>();

        //初始化
        init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Transform mapEditor;
    public Transform mapMaskGrid;

    public GameObject PrefabMask;           //图块遮罩Prefab

    public ToggleGroup ToggleGroupBlock;    //存放toggle的group
    public GameObject[] PrefabMapBlock;     //存放地图图块Prefab

    public GameObject PrefabToggle;         //实例化该预制体组成ToggleGroup
    public Transform ToggleGroup;           //存放上述预制体
    public int toggleChoice;                //被选中的toggle

    public List<MapStruct> MapStructList;   //存放地图图块的容器

    public int row;     //行
    public int column;  //列


    public void drawBlock(Vector3 pos, int type, string blockEvent, int doEventTimes)
    {
        if (GameObject.Find(pos.ToString()))
        {
            if (GameObject.Find(pos.ToString()).GetComponent<MapBlock>().type != type)
            {
                //覆盖掉同位置的GameObject
                Destroy(GameObject.Find(pos.ToString()));
            }
            else
            {
                //调出事件编辑菜单
                //Debug.Log("EventEditorMenu");
                eventEditor(pos.ToString());
                return;
            }

        }

        var prefabBlock = Instantiate(PrefabMapBlock[type], new Vector3(pos.x, pos.y, 0), new Quaternion(), mapEditor);
        prefabBlock.name = pos.ToString();
        prefabBlock.GetComponent<MapBlock>().type = type;

        switch (blockEvent)
        {
            case "Coin":
                {
                    prefabBlock.GetComponent<MapBlock>().BlockEvent = MapBlock.EventType.Coin;
                    break;
                }
            case "Goomba":
                {
                    prefabBlock.GetComponent<MapBlock>().BlockEvent = MapBlock.EventType.Goomba;
                    break;
                }

            default:
                break;
        }

        prefabBlock.GetComponent<MapBlock>().canDoEventTimes = doEventTimes;
    }

    public void createMapBlock2MapStruct()
    {
        var mapBlockGroup = GameObject.FindGameObjectsWithTag("MapBlock");
        MapStructList.Clear();  //fix bug 否则容器图块数将随保存按键的次数叠加

        foreach (var item in mapBlockGroup)
        {
            //创建地图图块数据结构
            MapStruct mapBlock = new MapStruct(item.transform.position.x, item.transform.position.y, item.GetComponent<MapBlock>().type, item.GetComponent<MapBlock>().BlockEvent.ToString(), item.GetComponent<MapBlock>().canDoEventTimes);

            MapStructList.Add(mapBlock);
            //Debug.Log("MapStructList.Length: " + MapStructList.Count); 
        }

    }

    public void createMask(int row, int column)  //创建网格遮罩
    {
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                //Instantiate(PrefabMask, new Vector3(0.64f * i, 0.64f * j, 0), new Quaternion(), mapMaskGrid);
                Instantiate(PrefabMask, new Vector3(0.639f * i, 0.639f * j, 0), new Quaternion(), mapMaskGrid);  //.64会导致拼接缝隙
            }
        }

        //坐标写死，将坐标系移至左下角
        mapMaskGrid.transform.position = new Vector3(-8.568f, -4.679f, 0);
    }

    public void init()
    {
        //图块prefab数组初始化（从prefab文件夹加载）
        PrefabMapBlock = new GameObject[1000];
        for (int i = 0; i < PrefabMapBlock.Length; i++)
        {
            PrefabMapBlock.SetValue(Resources.Load(@"Prefab/BlockPrefab/Ground_" + i.ToString()), i);
        }

        //图块toggleGroup初始化
        for (int i = 0; i < 1000; i++)
        {
            var toggle = Instantiate(PrefabToggle, ToggleGroup);

            toggle.GetComponent<Toggle>().group = ToggleGroupBlock;

            toggle.GetComponentInChildren<Image>().sprite = Resources.Load("Pictures/Block/Ground/Ground_" + i.ToString(), typeof(Sprite)) as Sprite;

            if (i == 0)
                toggle.GetComponent<Toggle>().isOn = true;

            toggle.name = i.ToString();
        }


    }

    //地图图块事件编辑
    public void eventEditor(string ob)
    {
        GameObject.Destroy(GameObject.Find("EventEditor(Clone)"));

        var editor = Instantiate(Resources.Load("Prefab/UI/EventEditor"), Input.mousePosition, new Quaternion(), GameObject.Find("Canvas").transform);

        //注册“确定”“取消”监听事件
        foreach (var btn in ((GameObject)editor).GetComponentsInChildren<Button>())
        {
            if (btn.name == "BtnEventConfirm")
            {
                btn.onClick.AddListener(delegate ()
                {
                    onBtnEventConfirm(ob, (GameObject)editor);
                });
            }
            else
            {
                btn.onClick.AddListener(delegate ()
                {
                    onBtnEventCancel();
                });
            }
        }

        //显示图块可执行事件次数
        ((GameObject)editor).GetComponentInChildren<InputField>().text = GameObject.Find(ob).GetComponent<MapBlock>().canDoEventTimes.ToString();

        //读取图块并改变预制体toggle
        foreach (var toggle in ((GameObject)editor).GetComponentsInChildren<Toggle>())
        {
            if (toggle.name == GameObject.Find(ob).GetComponent<MapBlock>().BlockEvent.ToString())
                toggle.isOn = true;
        }
    }

    //事件编辑UI确定按钮回调
    public void onBtnEventConfirm(string ob, GameObject editor)
    {
        //写入次数
        int times = 0;
        int.TryParse(editor.GetComponentInChildren<InputField>().text, out times);
        GameObject.Find(ob).GetComponent<MapBlock>().canDoEventTimes = times;

        //改变事件类型
        foreach (var toggle in editor.GetComponentsInChildren<Toggle>())
        {
            if (toggle.isOn)
            {
                switch (toggle.name)
                {
                    case "Coin":
                        {
                            GameObject.Find(ob).GetComponent<MapBlock>().BlockEvent = MapBlock.EventType.Coin;
                            break;
                        }
                    case "Goomba":
                        {
                            GameObject.Find(ob).GetComponent<MapBlock>().BlockEvent = MapBlock.EventType.Goomba;
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        GameObject.Destroy(editor);
    }
    public void onBtnEventCancel()
    {
        GameObject.Destroy(GameObject.Find("EventEditor(Clone)"));
    }
}
