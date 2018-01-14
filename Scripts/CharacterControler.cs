using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControler : MonoBehaviour
{

    public float speed = 1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        keyboardControl();
    }

    void keyboardControl()
    {
        //if (Input.GetKey(KeyCode.A))
        //{
        //    var pos = transform.position;
        //    this.transform.position = Vector3.MoveTowards(pos, new Vector3(pos.x - 10, pos.y, pos.z), speed * Time.deltaTime);
        //}
        //else if(Input.GetKey(KeyCode.D))
        //{
        //    var pos = transform.position;
        //    this.transform.position = Vector3.MoveTowards(pos, new Vector3(pos.x + 10, pos.y, pos.z), speed * Time.deltaTime);
        //}

        var horizontal = Input.GetAxis("Horizontal");
        var verticle = Input.GetAxis("Vertical");

        Debug.Log(horizontal);
        Debug.Log(verticle);

        transform.Translate(new Vector2(horizontal * 0.1f, verticle * 0.1f), Space.World);

    }
}
