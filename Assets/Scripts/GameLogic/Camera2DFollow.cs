/*
 * 时间：2018年3月21日14:26:46
 * 作者：vszed
 * 功能：
 * 1.摄像机追随角色
 * 2.阻尼
 * 3.防黑边
 */

using System;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    public Transform target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;

    // Use this for initialization
    private void Start()
    {
        m_LastTargetPosition = target.position;
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }


    // Update is called once per frame
    private void Update()
    {
        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (target.position - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else
        {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

        //U3D摄像机理解：https://blog.csdn.net/n5/article/details/50083205
        float orthographicSize = GetComponent<Camera>().orthographicSize;
        float aspectRatio = Screen.width * 1.0f / Screen.height;
        float cameraHeight = orthographicSize * 2;
        float cameraWidth = cameraHeight * aspectRatio;

        //镜头偏移值
        var offset = 0.35f;  //=>该偏移量只在图块比城堡右侧宽出来一个格子时显示效果最佳

        //边界值
        newPos.x = newPos.x > AirWall.getInstance().max_X - cameraWidth / 2 - offset ? AirWall.getInstance().max_X - cameraWidth / 2 - offset : newPos.x;
        //newPos.x = newPos.x < AirWall.getInstance().min_X + cameraWidth / 2 - offset ? AirWall.getInstance().min_X + cameraWidth / 2 - offset : newPos.x;
        newPos.x = newPos.x < 0 ? 0 : newPos.x;
        newPos.y = newPos.y < 0 ? 0 : newPos.y;

        transform.position = newPos;

        m_LastTargetPosition = target.position;
    }
}