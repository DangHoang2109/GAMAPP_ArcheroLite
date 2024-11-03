using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Player : MonoBehaviour
{
    public int counter;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("đây là start");
    }
    // Update is called once per frame
    void Update()
    {
        //Lấy thông tin nhập từ bàn phím
        //đi qua phải
        bool isPressRight = Input.GetKey(KeyCode.RightArrow);
        if (isPressRight)
        {
            this.transform.position += new Vector3(x: 1, y: 0, z: 0) * Time.deltaTime * speed;
        }
        //đi qua trái
        bool isPressLeft = Input.GetKey(KeyCode.LeftArrow);
        if (isPressLeft)
        {
            this.transform.position += new Vector3(x: -1, y: 0, z: 0) * Time.deltaTime * speed;
        }
        //đi lên trên
        bool isPressUp = Input.GetKey(KeyCode.UpArrow);
        if (isPressUp)
        {
            this.transform.position += new Vector3(x: 0, y: 1, z: 0) * Time.deltaTime * speed;
        }
        //đi qua trái
        bool isPressDown = Input.GetKey(KeyCode.DownArrow);
        if (isPressDown)
        {
            this.transform.position += new Vector3(x: 0, y: -1, z: 0) * Time.deltaTime * speed;
        }
    }
}
