using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy cận chiến: Ko biết bắn, chạy tới p[layer
/// Player
/// 
/// 
///                 Enemy
/// </summary>
public class Enemy : MonoBehaviour
{
    public float speed;
    //cần tìm Player trên game world => API FindObject
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player= FindObjectOfType<Player>();   
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    bool Movement()
    {
        //tính vector move là hướng đến Player
        Vector3 direction = (player.transform.position - this.transform.position).normalized;//chuẩn hóa thành vector hướng
        Vector3 move = direction * Time.deltaTime * speed;
        //kiểm tra camera kiểm 1
        this.transform.position = CheckPositionWithCamera_Approach3(move);

        //tính góc theo vector di chuyển nhập vào
        float angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angel - 90f);

        return true;
    }
    Vector3 CheckPositionWithCamera_Approach3(Vector3 move)
    {
        //Dùng API convert của Camera để tính giới hạn camera
        Vector2 screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        //vị trí mới, có thể gán hay ko?
        Vector3 newPostion = this.transform.position + move * Time.deltaTime * speed;

        //API Mathf.Clamp
        newPostion.x = Mathf.Clamp(newPostion.x, -screenBound.x + 0.5f, screenBound.x - 0.5f);
        newPostion.y = Mathf.Clamp(newPostion.y, -screenBound.y + 0.5f, screenBound.y - 0.5f);

        Debug.Log(newPostion);
        return newPostion;
    }
    ///Hàm tương tác va chạm: Chết/Gây sát thương/Thu thập máu/ Thu thập bình...
    ///Hàm chỉ viết ở 1 trong 2 componet: A hoặc B:
    ///Ví dụ: Đạn va chạm Enemy -> Hoặc là viết trong Bullet hoặc trong Enemy

    ///Task: Nhận diện va chạm khi Bullet va chạm với Enemy
    ///API nhận diện va chạm
    ///Nếu IsTrigger tắt
    ///Bạn muốn đạn đẩy được enemy và không đi xuyên qua được -> Wall
    /// <summary>
    /// Nếu IsTrigger true
    /// Tick true khi muốn chỉ tương tác chứ ko đẩy -> Bullet, bình máu, item...
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision là vật đang va chạm với this (Enemy) -> Bullet
        Debug.Log("Enemy " + this.name + " OnTrigger " + collision.gameObject.name);
        //va chạm với đạn -> chết
        //cách 1: check tag
        //Ưu điểm: nhanh hơn cách 2
        //Nhược điểm: không lấy được tham số của đạn -> damage
        //if(collision.tag == "Bullet")
        //{
        //    Debug.Log("Va chạm với đạn => chết");
        //    ///Api hủy 1 vật khỏi game-> Destroy(vật muốn hủy.gameObject), ko phải this
        //    ///Cách gọi Destroy đúng
        //    Destroy(collision.gameObject);
        //    Destroy(this.gameObject);

        //    ///Cách gọi destroy sai
        //    //Destroy(this);
        //    //Destroy(collision);
        //}

        //cách 2: API GetComponent-> collision chính là viên đạn -> nó có 1 component Bullet đi kèm
        Bullet dan = collision.GetComponent<Bullet>();
        if(dan != null) //lấy được component đạn
        {
            Debug.Log("Va chạm với đạn => chết");
            ///Api hủy 1 vật khỏi game-> Destroy(vật muốn hủy.gameObject), ko phải this
            ///Cách gọi Destroy đúng
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            //Không làm gì cả
            Debug.Log("Va chạm với vật không phải đạn");
        }
    }
}
