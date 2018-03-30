/*
 * 时间：2018年3月27日01:34:45
 * 作者：vszed
 * 功能：挂在到旗子图块下
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    bool playAnimSwitch;
    Collider2D ob;  //取旗的角色

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGroundedSetStatic();
        goToCastleAnim(ob);
    }

    //落地后移除刚体
    public void isGroundedSetStatic()
    {
        //在编辑地图中会直接删掉刚体
        if (GetComponentInParent<Rigidbody2D>() && GetComponentInParent<Rigidbody2D>().velocity.y == 0)
            Destroy(GetComponentInParent<Rigidbody2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>() && !collision.GetComponentInChildren<Animator>().GetBool("isGetFlag"))
        {
            StartCoroutine(getFlag(collision));
        }
    }

    //取得旗子
    public IEnumerator getFlag(Collider2D collision)
    {
        GameControler.getInstance().GameOver = true;
        collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        collision.GetComponentInChildren<Animator>().SetBool("isGetFlag", true);

        yield return new WaitForSeconds(0.15f);
        if (AudioControler.getInstance().BGM_Ground.isPlaying)
            AudioControler.getInstance().BGM_Ground.Stop();
        if (AudioControler.getInstance().BGM_Ground_Hurry.isPlaying)
            AudioControler.getInstance().BGM_Ground_Hurry.Stop();
        collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        AudioControler.getInstance().SE_SYS_GOAL_FLAG.Play();

        yield return new WaitForSeconds(1);
        AudioControler.getInstance().SE_course_clear.Play();
        playAnimSwitch = true;
        ob = collision;

        yield return new WaitForSeconds(1.5f);
        AudioControler.getInstance().SE_VOC_MA_CLEAR_NORMAL.Play();
    }

    //取旗后去城堡动画
    public void goToCastleAnim(Collider2D ob)
    {
        if (playAnimSwitch)
        {
            ob.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            var character = ob.gameObject;
            character.GetComponentInChildren<Animator>().SetBool("isGetFlag", false);
            character.GetComponentInChildren<Animator>().SetBool("goToCastle", true);
            //确保朝向右
            character.transform.rotation = Quaternion.Euler(0, 0, 0);
            character.transform.position = Vector2.MoveTowards(character.transform.position, new Vector2(character.transform.position.x + 1, character.transform.position.y), character.GetComponent<Character>().MoveSpeed / 2 * Time.deltaTime);
        }
    }
}
