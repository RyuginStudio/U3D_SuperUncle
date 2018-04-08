/*
 * 作者： joi http://forum.china.unity3d.com/thread-292-1-1.html
 * 使用方法：将该脚本附在镜头下面，然后切换场景时镜头不销毁
 * 要调用的时候获得该脚本，然后调用StartSplash（）
 * 修改：vszed
 * 暂时弃用
 */

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    private static SceneLoad instance;
    public static SceneLoad getInstance()
    {
        return instance;
    }

    public int guiDepth = 0;

    public string levelToLoad = "";
    //将要加载的场景名称
    public string sceneName;
    //切换场景的纹理
    public Texture2D splashLogo;
    //淡入淡出的速度
    float fadeSpeed = 0.8f;
    //保持纹理最高透明度的时间
    float waitTime = 0f;
    //是否要等待输入
    public bool waitForInput = false;

    public bool startAutomatically = false;
    public bool IsDealPlayer = false;
    private float timeFadingInFinished = 0.0f;
    //处理切换场景后需要实现的事件
    public delegate void EventHandler();


    public event EventHandler trigger;
    public delegate void EventHandler2nd();


    public event EventHandler2nd triggerAtLoading;
    //淡入淡出方式
    public enum SplashType
    {
        LoadNextLevelThenFadeOut,
        FadeOutThenLoadNextLevel
    }
    public SplashType splashType;

    private float alpha = 0.0f;
    //纹理的状态
    private enum FadeStatus
    {
        Paused,
        FadeIn,
        FadeWaiting,
        FadeOut
    }
    private FadeStatus status = FadeStatus.Paused;

    private Rect splashLogoPos = new Rect();
    //是否要自适应屏幕大小
    public enum LogoPositioning
    {
        Centered,
        Stretched
    }
    public LogoPositioning logoPositioning;

    public bool loadingNextLevel = false;

    void Start()
    {
        instance = this;

        //      if(startAutomatically)
        //      {
        //          status = FadeStatus.FadeIn;
        //      }
        //      else
        //      {
        //          status = FadeStatus.Paused;
        //      }
        if (logoPositioning == LogoPositioning.Centered)
        {
            splashLogoPos.x = (Screen.width * 0.5f) - (splashLogo.width * 0.5f);
            splashLogoPos.y = (Screen.height * 0.5f) - (splashLogo.height * 0.5f);

            splashLogoPos.width = splashLogo.width;
            splashLogoPos.height = splashLogo.height;
        }
        else
        {
            splashLogoPos.x = 0;
            splashLogoPos.y = 0;

            splashLogoPos.width = Screen.width;
            splashLogoPos.height = Screen.height;
        }

        if (splashType == SplashType.LoadNextLevelThenFadeOut)
        {
            DontDestroyOnLoad(this);

        }

        if (SceneManager.sceneCount <= 1)
        {
            //Debug.LogWarning("Invalid levelToLoad value.");
        }
    }

    //开始切换，要跳转的场景和设置对应的切换速率
    public void StartSplash(string sceneName, int i)
    {
        status = FadeStatus.FadeIn;
        this.sceneName = sceneName;
        SetValue(i);

    }
    public void setIsDealPlayer(bool isDealPlayer)
    {
        IsDealPlayer = isDealPlayer;
    }
    public void SetValue(int i)
    {
        switch (i)
        {
            case 1:
                fadeSpeed = 0.8f;
                waitTime = 1.0f;
                break;
            case 2:
                fadeSpeed = 0.8f;
                waitTime = 1.0f;
                break;
            case 3:
                fadeSpeed = 0.8f;
                waitTime = 1.0f;
                break;
        }
    }
    void Update()
    {

        switch (status)
        {
            case FadeStatus.FadeIn:

                alpha += fadeSpeed * Time.deltaTime;
                break;
            case FadeStatus.FadeWaiting:
                if ((!waitForInput && Time.time >= timeFadingInFinished + waitTime) || (waitForInput && Input.anyKey))
                {
                    status = FadeStatus.FadeOut;

                }
                break;
            case FadeStatus.FadeOut:
                alpha += -fadeSpeed * Time.deltaTime * 2;
                if (alpha <= 0.0f)
                {
                    if (trigger != null)
                    {
                        trigger();
                    }

                    status = FadeStatus.Paused;

                }
                break;
        }
    }

    void OnGUI()
    {

        GUI.depth = guiDepth;
        if (splashLogo != null)
        {
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Mathf.Clamp01(alpha));
            GUI.DrawTexture(splashLogoPos, splashLogo);
        }
        if (alpha > 1.0f)
        {

            status = FadeStatus.FadeWaiting;

            timeFadingInFinished = Time.time;
            alpha = 1.0f;
            if (splashType == SplashType.LoadNextLevelThenFadeOut)
            {

                loadingNextLevel = true;
                if ((SceneManager.sceneCount) >= 1)
                {
                    //print(levelToLoadInt);
                    SceneManager.LoadScene(sceneName);
                    if (triggerAtLoading != null)
                        triggerAtLoading();



                }
            }
        }
        if (alpha < 0.0f)
        {
            if (splashType == SplashType.FadeOutThenLoadNextLevel)
            {
                if (SceneManager.sceneCount >= 1)
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
            else
            {


            }
        }
    }

}