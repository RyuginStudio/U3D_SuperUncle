using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveScene : MonoBehaviour
{
    public Text livesNum;

    // Use this for initialization
    void Start()
    {
        livesNum.text = GameData.MarioLives.ToString();
        Invoke("jumpScene",3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void jumpScene()
    {
        StartCoroutine(SceneTransition.getInstance().loadScene("MainScene", 2));
    }
}
