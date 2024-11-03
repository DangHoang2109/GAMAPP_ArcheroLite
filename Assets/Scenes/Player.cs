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
        //Di chuyển
        Movement();

    }

    /// <summary>
    /// code di chuyển mới: biến đổi đều
    /// Cách 1: Dùng API của Unity: Input.GetAxis -> 2 chiều horizontal và vertical, return 1 số từ -1 đến 1 tùy theo chiều user nhấn và thời gian nhấn
    /// Cách 2: Dùng vật lý di chuyển biến đổi đều với gia tốc: v = v0 + a.deltaT -> speed' = speed0 + a.delta
    /// 
    /// code di chuyển bound bởi camera màn hình
    /// Cách 1: Kiểm tra bằng tay: thấy X trong khoảng [-9.8, 9.8] y trong khoảng [-4.3,4.3]
    /// Cách 2: Buổi 2 khi học về vật lý + va chạm: setup 4 bức tường ở rìa camera
    /// Cách 3: Camera.main để tính screenBound -> không cho position hơn screen bound
    /// </summary>
    void Movement()
    {
        //Cách 1: "Horizontal" hoặc "Vertical"
        //return -1 nếu đi về bên trái
        //return 1 nếu đi về bên phải
        //return 0 nếu không nhấn
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x: horizontal, y: vertical, z: 0) * Time.deltaTime * speed;
        //kiểm tra camera kiểm 1
        this.transform.position = CheckPositionWithCamera_Approach3(horizontal, vertical);

        //xoay nhân vật: keyword: Quaternion và Eular API
        if(horizontal != 0 || vertical != 0)
        {
            //tính góc theo vector di chuyển nhập vào
            float angel = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angel - 90f);
        }
    }

    Vector3 CheckPositionWithCamera_Approach3(float horizontal, float vertical)
    {
        //Dùng API convert của Camera để tính giới hạn camera
        Vector2 screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        //vị trí mới, có thể gán hay ko?
        Vector3 newPostion = this.transform.position + new Vector3(x: horizontal, y: vertical, z: 0) * Time.deltaTime * speed;

        //API Mathf.Clamp
        newPostion.x = Mathf.Clamp(newPostion.x, -screenBound.x+0.5f, screenBound.x-0.5f);
        newPostion.y = Mathf.Clamp(newPostion.y, -screenBound.y + 0.5f, screenBound.y-0.5f);

        return newPostion;
    }

    Vector3 CheckPositionWithCamera_Approach1(float horizontal, float vertical)
    {
        //vị trí mới, có thể gán hay ko?
        Vector3 newPostion = this.transform.position + new Vector3(x: horizontal, y: vertical, z: 0) * Time.deltaTime * speed;
        //kiểm tra position mới

        //API Mathf.Clamp
        newPostion.x = Mathf.Clamp(newPostion.x, -9.8f, 9.8f);
        newPostion.y = Mathf.Clamp(newPostion.y, -4.3f, 4.3f);

        //bad code
        //if(newPostion.x < -9.8)
        //    newPostion = new Vector3(-9.8f, newPostion.y, newPostion.z);
        //if (newPostion.x > 9.8)
        //    newPostion = new Vector3(9.8f, newPostion.y, newPostion.z);
        //if (newPostion.y < -4.3)
        //    newPostion = new Vector3(newPostion.x, -4.3f, newPostion.z);
        //if (newPostion.x > 4.3)
        //    newPostion = new Vector3(newPostion.x, 4.3f, newPostion.z);

        return newPostion;
    }

    //code di chuyển cũ: đi đều,
    void OldMovement()
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
