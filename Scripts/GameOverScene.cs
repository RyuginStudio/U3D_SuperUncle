using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] private GameObject diedMarioUI;

    [SerializeField] private GameObject dropPoint;

    private float dropSpeed = 2000;

    private bool dropSwitch;

    // Use this for initialization
    void Start()
    {
        Invoke("beginDrop", 1);
        StartCoroutine(playSE());
        jumpScene();
    }

    // Update is called once per frame
    void Update()
    {
        dropMario();
    }

    private void beginDrop()
    {
        dropSwitch = true;
    }
    private void dropMario()
    {
        if (dropSwitch)
        {
            diedMarioUI.transform.position = Vector3.MoveTowards(diedMarioUI.transform.position, dropPoint.transform.position, ++dropSpeed * Time.deltaTime);
        }
    }

    private IEnumerator playSE()
    {
        yield return new WaitForSeconds(1.3f);
        AudioControler.getInstance().SE_GameOver.Play();
    }

    void jumpScene()
    {
        StartCoroutine(SceneTransition.getInstance().loadScene("TitleScene", 5, 3));
    }
}
