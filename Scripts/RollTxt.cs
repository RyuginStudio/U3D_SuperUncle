/*
 * 来源：https://www.cnblogs.com/vital/archive/2013/09/23/3334533.html
 * 功能：文字滚动
 * 修改：vszed
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RollTxt : MonoBehaviour
{
    //支持中文
    public string txt;
    public string showTxt;
    public int showLength = 8;
    public int txtLength;
    public float rollSpeed = 0.1f;
    public int indexMax = 0;
    private float currentIdx;

    // Use this for initialization
    void Start()
    {
        txtLength = txt.Length;
        showTxt = txt.Substring(0, showLength);
        indexMax = txtLength - showLength + 1;
    }

    // Update is called once per frame
    void Update()
    {
        GetShowTxt();
    }

    void GetShowTxt()
    {
        if (showLength >= txtLength)
        {
            showTxt = txt;
        }
        else if (showLength < txtLength)
        {
            int startIndex = 0;

            //往返
            //startIndex = (int)(Mathf.PingPong(Time.time * rollSpeed, 1) * indexMax);

            //单向
            startIndex = (int)(currentIdx += rollSpeed);

            if (startIndex >= txtLength - showLength)
            {
                startIndex = 0;
                currentIdx = 0;
            }

            showTxt = txt.Substring(startIndex, showLength);
        }

        GetComponent<Text>().text = showTxt;
    }
}

/*
 * Mathf.PingPong 乒乓
 * 让数值t在 0到length之间往返。t值永远不会大于length的值，也永远不会小于0
 * 返回值将在0和length之间来回移动
*/
