using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    private float currentTime;
    private float animationUpdate;

    private float logoAlpha = 0;  //图片alpha值
    [SerializeField] private GameObject vszedLogo;

    // Use this for initialization
    void Start()
    {
        Invoke("playSound", 0.5f);
        Invoke("jumpNextScene", 5);

        currentTime = Time.time;
        animationUpdate = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        playAnimation();
    }

    void playSound()
    {
        //抖动摄像机
        GetComponent<ShakeCamera>().isshakeCamera = true;

        AudioControler.getInstance().SE_vszed.Play();
    }

    void playAnimation()
    {
        var LogoColor = vszedLogo.GetComponent<SpriteRenderer>().color;

        if (currentTime - animationUpdate >= 0.1f)
        {
            vszedLogo.GetComponent<SpriteRenderer>().color = new Color(LogoColor.r, LogoColor.g, LogoColor.b, logoAlpha += 0.02f);
        }
    }

    void jumpNextScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
