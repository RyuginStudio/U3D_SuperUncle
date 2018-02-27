/*
 * 时间：2018年2月28日00:48:40
 * 功能：实现UI可拖拽
 * 修改：vszed
 * 来源：http://blog.csdn.net/u013452440/article/details/70473899
 */


using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragUI : MonoBehaviour
{
    private static Vector3 ON_DRAG_SCALE = new Vector3(1.2f, 1.2f, 1.2f);
    private static Vector3 NORMAL_SCALE = Vector3.one;
    private static Vector2 ON_DRAG_PIVOT = new Vector2(0.5f, 0.5f);
    private RectTransform mRectTransform;
    void Awake()
    {
        mRectTransform = GetComponent<RectTransform>();
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();

        UnityAction<BaseEventData> pointerdownClick = new UnityAction<BaseEventData>(OnPointerDown);
        EventTrigger.Entry myclickDown = new EventTrigger.Entry();
        myclickDown.eventID = EventTriggerType.PointerDown;
        myclickDown.callback.AddListener(pointerdownClick);
        trigger.triggers.Add(myclickDown);

        UnityAction<BaseEventData> pointerupClick = new UnityAction<BaseEventData>(OnPointerUp);
        EventTrigger.Entry myclickUp = new EventTrigger.Entry();
        myclickUp.eventID = EventTriggerType.PointerUp;
        myclickUp.callback.AddListener(pointerupClick);
        trigger.triggers.Add(myclickUp);

        UnityAction<BaseEventData> pointerdragClick = new UnityAction<BaseEventData>(OnDrag);
        EventTrigger.Entry myclickDrag = new EventTrigger.Entry();
        myclickDrag.eventID = EventTriggerType.Drag;
        myclickDrag.callback.AddListener(pointerdragClick);
        trigger.triggers.Add(myclickDrag);
    }
    public void OnPointerDown(BaseEventData data)
    {
        transform.GetComponent<Image>().color = Color.gray;
        mRectTransform.pivot = ON_DRAG_PIVOT;
        transform.position = Input.mousePosition;
        this.transform.localScale = ON_DRAG_SCALE;
    }
    public void OnPointerUp(BaseEventData data)
    {
        transform.GetComponent<Image>().color = Color.white;
        this.transform.localScale = NORMAL_SCALE;
        transform.position = Input.mousePosition;
    }
    public void OnDrag(BaseEventData data)
    {
        transform.position = Input.mousePosition;
    }
}