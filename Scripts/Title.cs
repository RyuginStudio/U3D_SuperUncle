using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private GameObject blackMask;

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
        changeAlpha();

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

        yield return new WaitForSeconds(2);

        if (btnName == "Story")
            SceneManager.LoadScene("MainScene");
        else
            Application.Quit();
    }

    //点选完按钮后按钮移动
    public void btnMoveAnim()
    {
        btn_Story.transform.position = Vector2.MoveTowards(btn_Story.transform.position, new Vector2(btn_Story.transform.position.x - 1, btn_Story.transform.position.y), 1);
        btn_Quit.transform.position = Vector2.MoveTowards(btn_Quit.transform.position, new Vector2(btn_Quit.transform.position.x + 1, btn_Quit.transform.position.y), 1);

        blackMask.SetActive(true);
    }

    //更改画面不透明度
    public void changeAlpha()
    {
        if (blackMask.GetComponent<Image>().IsActive())
        {
            var color = blackMask.GetComponent<Image>().color;
            blackMask.GetComponent<Image>().color = new Color(color.r, color.g, color.b, color.a + 0.01f);

            //顺便改一下bgm淡出
            AudioControler.getInstance().BGM_Title.volume -= 0.0085f;
        }
    }
}
