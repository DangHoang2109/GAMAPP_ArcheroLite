using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{
    public int _amountDan = 6;
    public float _thoigianBan = 1f;//s
    public float _timerTGBan = 0f;
    public Bullet _dan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timerTGBan += Time.deltaTime;
        if (_timerTGBan >= _thoigianBan)
        {
            Fire();
            _timerTGBan = 0;
        }
    }

    //Bắn 6 viên xung quanh
    void Fire()
    {
        for (int i = 0; i < _amountDan; i++)
        {
            //_dan = this._dan = viên đạn gốc
            Bullet _danVuaBanRa = Instantiate(_dan, this.transform.position, Quaternion.identity);
            //sửa góc viên đạn vừa bắn 
            _danVuaBanRa.angle = i * (360f / _amountDan);
        }
    }
}
