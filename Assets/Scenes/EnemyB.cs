using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Vì con này có timer Bắn, mà khi bắn, mỗi viên đạn cách nhau 1 khoảng thời gian B' nữa
/// Nên con này sẽ có 2 bộ đếm thời gian lồng trong nhau
/// -- Dem thoi gian ban: 2s
///     -- Nếu đang bắn
///         -- Đếm thời gian cho ra đạn 0.2s
///     --
/// --
/// </summary>
public class EnemyB : MonoBehaviour
{
    public int _amountDan = 6;

    public Bullet _dan;

    public float _thoigianBan = 2f;//s
    public float _timerTGBan = 0f;

    public float _thoigianRaDan = 0.2f;//s
    public float _timerTGRaDan = 0f;

    public bool _isShooting; //đã vào trạng thái bắn hay chưa
    public int _indexDanShooting; //đang bắn viên thứ mấy trong trạng thái bắn đạn
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _timerTGBan += Time.deltaTime;
        if(_timerTGBan >= _thoigianBan)
        {
            _isShooting = true;
        }

        //nếu đang bắn
        if (_isShooting)
        {
            _timerTGRaDan += Time.deltaTime;
            if (_timerTGRaDan >= _thoigianRaDan)
            {
                _timerTGRaDan = 0;
                Bullet _danVuaBanRa = Instantiate(_dan, this.transform.position, Quaternion.identity);
                //sửa góc viên đạn vừa bắn 
                _danVuaBanRa.angle = _indexDanShooting * (360f / _amountDan);
                _indexDanShooting++;
            }

            if(_indexDanShooting >= _amountDan)
            {
                _indexDanShooting = 0;
                _timerTGBan = 0f;
                _isShooting = false;
            }
        }
    }
}
