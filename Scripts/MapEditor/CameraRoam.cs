/*
 * 时间：2018年3月23日22:50:08
 * 作者：vszed
 * 功能：地图编辑器下的镜头漫游
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoam : MonoBehaviour
{
    public Camera currtCamera;
    public float cameraMoveSpeed = 7;
    public float cameraZoomSpeed = 800;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        doRoam();
    }

    private void doRoam()
    {
        var pos = currtCamera.transform.position;
        var rotation = currtCamera.transform.rotation;

        currtCamera.transform.SetPositionAndRotation(new Vector3(pos.x + Input.GetAxis("Horizontal") * Time.deltaTime * cameraMoveSpeed, pos.y + Input.GetAxis("Vertical") * Time.deltaTime * cameraMoveSpeed, pos.z), rotation);

        currtCamera.orthographicSize = currtCamera.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * cameraZoomSpeed >= 1 ? currtCamera.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * cameraZoomSpeed : 1;
    }
}
