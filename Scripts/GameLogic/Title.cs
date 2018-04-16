using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public Button btn_Story;
    public Button btn_Quit;
    public bool btn_move_switch;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (btn_move_switch)
            btnMoveAnim();
    }

    public void onBtnCallBack(string btnName)
    {
        StartCoroutine(doEvent(btnName));
    }

    public IEnumerator doEvent(string btnName)
    {
        btn_move_switch = true;
        btn_Story.GetComponent<Button>().interactable = false;
        btn_Quit.GetComponent<Button>().interactable = false;

        AudioControler.getInstance().SE_Confirm.Play();

        if (btnName == "Story")
        {
            StartCoroutine(SceneTransition.getInstance().loadScene("MissionViewScene", 0, 2));
            GameData.dataUpdate();
        }
        else
        {
            SceneTransition.getInstance().IncreaseSwitch = true;
            yield return new WaitForSeconds(2);
            Application.Quit();
        }

    }

    //点选完按钮后按钮移动
    public void btnMoveAnim()
    {
        btn_Story.transform.position = Vector2.MoveTowards(btn_Story.transform.position, new Vector2(btn_Story.transform.position.x - 1, btn_Story.transform.position.y), 1);
        btn_Quit.transform.position = Vector2.MoveTowards(btn_Quit.transform.position, new Vector2(btn_Quit.transform.position.x + 1, btn_Quit.transform.position.y), 1);
    }

}
