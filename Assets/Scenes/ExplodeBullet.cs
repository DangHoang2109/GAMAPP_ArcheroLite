using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bay như một viên đạn bình thường
/// Bay được 2 giây thì nổ ra 4 viên đạn thường
/// </summary>
public class ExplodeBullet : Bullet
{
    //public float bulletSpeed = 15f;
    //public float angle = 0;

    public int _amountNo = 4; //So luong dan no
    public float _thoiGianNo = 2;//second
    public float _timerNo = 0f;

    public Bullet _smallerBullet;
    // Update is called once per frame
    void Update()
    {
        //Di chuyển
        Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
        this.transform.position += bulletSpeed * Time.deltaTime * direction; // direction;

        //Đếm giờ nổ
        _timerNo += Time.deltaTime;
        if (_timerNo >= _thoiGianNo)
        {
            Explode();
            _timerNo = 0;
        }
    }
    //Nổ ra 4 đạn nhỏ hơn
    void Explode()
    {
        for (int i = 0; i < _amountNo; i++)
        {
            //_dan = this._dan = viên đạn gốc
            Bullet _danVuaBanRa = Instantiate(_smallerBullet, this.transform.position, Quaternion.identity);
            //sửa góc viên đạn vừa bắn 
            _danVuaBanRa.angle = i*(360f/ _amountNo);
        }

        //Optional: Nổ mất viên đạn hiện tại
        Destroy(this.gameObject);
    }
}
