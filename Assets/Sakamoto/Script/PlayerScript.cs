using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D _rb;

    //ステータス
    [SerializeField]private int _player_MaxHp;
    private int _player_Hp;
    [SerializeField]private int _player_Attack;

    //攻撃力アップアイテムに使う変数
    private bool _powerAttackBool;
    private int _powerAttackNum;
    private float _powerItemTime;
    private float _powerItemTimeCount;

    //無敵担うアイテムをとった時に使う変数
    private bool _InvincibleBool;
    private float _InvincibleItemTime;
    private float _InvincibleItemCount;

    //プレイヤーの移動速度
    [SerializeField] private float _speed;
    //移動変数
    private float _movex;
    private float _movey;
    //プレイヤーのTransform
    private Vector3 _player_Position;
    //呼び出す弾とマズル
    [SerializeField] private GameObject _ShotBulletPrefab;
    [SerializeField] private GameObject _shotMazzule;
    //弾を出す間隔
    [SerializeField] private float _shotSetInterval;
    private float _shotCountTime;

    void Start()
    {
        _player_Position = GetComponent<Transform>().position;

        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //弾を出す処理
        Shot();
        //プレイヤーを動かす処理
        Movement();

        //パワーアップアイテムをとった時
        if (_powerAttackBool) 
        {
           _powerItemTimeCount += Time.deltaTime;

            if (_powerItemTime < _powerItemTimeCount) 
            {
                //攻撃力を下げる
                _player_Attack -= _powerAttackNum;
                _powerAttackBool = false;
            }
        }

        _shotCountTime += Time.deltaTime;
        
    }

    /// <summary>
    /// プレイヤーを動かす処理
    /// </summary>
    private void Movement() 
    {

        _movex = Input.GetAxisRaw("Horizontal");
        _movey = Input.GetAxisRaw("Vertical");

        if (_movey >= 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (_movey <= -1)
        {
            transform.rotation = Quaternion.Euler(-180, 0, 0);
        }

        if (_movex >= 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        else if (_movex <= -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        _rb.velocity = new Vector2(_movex * _speed, _movey * _speed);
    }

    /// <summary>
    /// プレイヤーが弾を出す処理
    /// </summary>
    private void Shot() 
    {
        if (Input.GetKey(KeyCode.Mouse0) && _shotSetInterval < _shotCountTime)
        {
            Instantiate(_ShotBulletPrefab, _shotMazzule.transform.position, _shotMazzule.transform.rotation);
            _shotCountTime = 0;
        }
    }


    private void AddDamage(int damage) 
    {
        if (!_InvincibleBool)
        {
            SetHp(GetHp() - damage);
        }
        else 
        {
            _InvincibleBool = true;
        }
        
        if (GetHp() <= 0) 
        {
            //ゲームオーバー
        }
    }

    private void OnTiggerEnter2D(Collision collision)
    {
        //パワーアップアイテムをとった時
        if (collision.gameObject.tag == "PowerItem")
        {
            _powerItemTimeCount = 0;
            //攻撃力を上げる
            _player_Attack += _powerAttackNum;
            _powerAttackBool = true;

        }

        //無敵アイテムをとった時
        if (collision.gameObject.tag == "InvincibleItem") 
        {
            
        }
    }

    /// <summary>
    /// PlayerStatus
    /// </summary>
    /// <param name="Status"></param>
    #region
    public void SetMaxHp(int hp)
    {
        _player_MaxHp = hp;
    }

    public int GetMaxHp() => _player_MaxHp;

    public void SetHp(int hp)
    {
       _player_Hp = Mathf.Max(0, Mathf.Min(GetMaxHp(), hp));
    }

    public int GetHp() => _player_Hp;

    public void SetAttack(int attack)
    {
        _player_Attack = attack;
    }

    public int GetAttack() => _player_Attack;
    #endregion
}
