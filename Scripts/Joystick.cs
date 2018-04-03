/*
 * 时间：2018年4月3日17:05:22
 * 作者：雨凇momo
 * 修改：vszed
 * 来源：http://www.xuanyusong.com/archives/3924
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Joystick : ScrollRect
{
    protected float mRadius = 0f;
    public bool isBeginDrag;

    private static Joystick instance;
    public static Joystick getInstance()
    {
        return instance;
    }

    protected override void Start()
    {
        base.Start();
        instance = this;

        //计算摇杆块的半径
        mRadius = (transform as RectTransform).sizeDelta.x * 0.5f;
    }

    public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnDrag(eventData);
        var contentPostion = this.content.anchoredPosition;
        if (contentPostion.magnitude > mRadius)
        {
            contentPostion = contentPostion.normalized * mRadius;
            SetContentAnchoredPosition(contentPostion);
        }

        isBeginDrag = true;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin");
        base.OnBeginDrag(eventData);
        isBeginDrag = true;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End");
        base.OnEndDrag(eventData);
        isBeginDrag = false;
    }
}