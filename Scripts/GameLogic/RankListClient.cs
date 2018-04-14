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
    public GameObject TipsLabel;

    public GameObject UpLoadModule;
    public GameObject RankListModule;

    public Text NameInputText;
    public Text TipsInputText;

    private string url;

    // Use this for initialization
    void Start()
    {
        url = "47.75.2.153/hello.aspx";
    }

    // Update is called once per frame
    void Update()
    {
        rotateLoadingAnim();
    }

    public IEnumerator load(string url)
    {
        yield return new WaitForSeconds(2);

        Destroy(RotatePic);

        //发送请求：U3D的WWW基于Http通讯 => 适合短连接
        WWW httpGet = new WWW(url);

        //接收请求
        yield return httpGet;

        //判断请求是否有错误：空为没错误
        if (string.IsNullOrEmpty(httpGet.error))
        {
            RankListService service = new RankListService();
            service.updateList("vszed所向披靡");
            Debug.Log(service.getRankList());
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
        StartCoroutine(SceneTransition.getInstance().loadScene("MissionViewScene", 0, 2));
    }
}
