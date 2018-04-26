/*
 * 时间：2018年4月6日13:04:33
 * 作者：vszed
 * 功能：联网排行榜
 * 参考：https://blog.csdn.net/icrazyaaa/article/details/49909937
 * 注意：要访问webservice的方法，需要具备的是：webservice的wsdl、system.web.dll、system.web.services.dll，前者可以通过visual studio自带的命令提示访问wsdl工具，后俩个dll可以在D:\Program Files\Unity\Editor\Data\Mono\lib\mono\2.0中找到。
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankListClient : MonoBehaviour
{
    public GameObject RotatePic;
    public GameObject ErrorLabel;

    public GameObject UpLoadModule;
    public GameObject RankListModule;

    public Text NameInputText;
    public Text TipsInputText;

    private string url;

    // Use this for initialization
    void Start()
    {
        url = @"http://47.75.2.153/hello.aspx";  //安卓端不会自动添加http://...

        //Debug.Log(GameData.CostTime);
        //Debug.Log(GameData.GetScore);
    }

    // Update is called once per frame
    void Update()
    {
        rotateLoadingAnim();
    }

    public GameObject prefab_RankData;
    public Transform instateTransform;

    public IEnumerator load(string url)
    {
        //发送请求：U3D的WWW基于Http通讯 => 适合短连接
        WWW httpGet = new WWW(url);

        //接收请求
        yield return httpGet;

        //判断请求是否有错误：空为没错误
        if (string.IsNullOrEmpty(httpGet.error))
        {
            //上传至排行榜
            RankListService service = new RankListService();
            service.upLoadData("map" + GameData.currentMissionNum, NameInputText.text, GameData.CostTime, GameData.GetScore);

            //取排行榜内容
            yield return new WaitForSeconds(2);
            Destroy(RotatePic);
            var list = service.getRankList("map" + GameData.currentMissionNum);

            instateTransform.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(534, 50 * list.Length);

            foreach (var item in list)
            {
                var prefab = Instantiate(prefab_RankData, instateTransform);
                foreach (var dataInPrefab in prefab.GetComponentsInChildren<Text>())
                {
                    switch (dataInPrefab.name)
                    {
                        case "UserName":
                            dataInPrefab.GetComponent<Text>().text = item.UserName;
                            break;
                        case "CostTime":
                            dataInPrefab.GetComponent<Text>().text = item.CostTime.ToString() + "秒";
                            break;
                        case "Score":
                            dataInPrefab.GetComponent<Text>().text = item.Score.ToString() + "分";
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        else
        {
            //网络异常
            ErrorLabel.SetActive(true);
            Debug.Log(httpGet.error);
        }
    }

    void rotateLoadingAnim()
    {
        if (RotatePic)
            RotatePic.transform.Rotate(new Vector3(0, 0, -1));
    }

    public void onBtnUpLoad()
    {
        if (NameInputText.text != "")
        {
            AudioControler.getInstance().SE_FlyClapLong.Play();

            Destroy(UpLoadModule);
            RankListModule.SetActive(true);

            StartCoroutine(load(url));
        }
        else
        {
            TipsInputText.text = "你的名字不能为空......";
            if (!AudioControler.getInstance().SE_Invalid.isPlaying)
                AudioControler.getInstance().SE_Invalid.Play();
        }
    }

    public void onBtnQuitToTitle()
    {
        AudioControler.getInstance().SE_PlayExit.Play();
        StartCoroutine(SceneTransition.getInstance().loadScene("TitleScene", 0, 2));
    }

    public void onBtnPlayNext()
    {
        AudioControler.getInstance().SE_PlayNext.Play();

        ++GameData.currentMissionNum;
        PlayerPrefs.SetInt("currentMissionNum", GameData.currentMissionNum);  //存档

        if (GameData.currentMissionNum <= GameData.missionTotalNum)
        {
            StartCoroutine(SceneTransition.getInstance().loadScene("MissionViewScene", 0, 2));
        }
        else
        {
            //见公主场景
            PlayerPrefs.DeleteAll();  //TODO：清除存档 不要清除奖杯
        }

    }
}