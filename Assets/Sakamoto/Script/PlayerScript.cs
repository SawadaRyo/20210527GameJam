using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : SingletonBehaviour<PlayerScript>, IDamageble, IItemInterface
{
    public enum PlayerType 
    {
        Standard,
        powerUp,
        Invincible,
    }
    private Rigidbody2D _rb;

    //ステータス
    [Header("ステータス")]
    [SerializeField]private int _player_MaxHp;
    private int _player_Hp;
    public int Player_Attack;

    //攻撃力アップアイテムに使う変数
    private bool _powerAttackBool;
    private int _powerAttackNum = 5;
    [Header("パワーアップアイテムの効果時間")] 

    [SerializeField]private float _powerItemTime;
    private float _powerItemTimeCount;
    [Header("パワーアップパーティクル")]

    [SerializeField] private GameObject _powerParticleObj;

    //無敵担うアイテムをとった時に使う変数
    private bool _InvincibleBool;

    //プレイヤーの移動速度
    [Header("プレイヤーの移動速度")]
    [SerializeField] private float _speed;
    //移動変数
    private float _movex;
    private float _movey;

    //プレイヤーのTransform
    private Vector3 _player_Position;

    [Header("呼び出す弾とマズル")]
    [SerializeField] private GameObject _shotBulletPrefab;
    [SerializeField] private GameObject _shotMuzzule;

    [Header("弾を出す間隔")]
    [SerializeField] private float _shotSetInterval;

    //オーディオ
    private AudioSource _audioSource;
    private AudioClip _shotSound;

    [Header("バリアのイメージのオブジェクト")]
    [SerializeField] private GameObject _barrier;

    private float _shotCountTime;

    void Start()
    {
        _player_Position = GetComponent<Transform>().position;
        _audioSource = GetComponent<AudioSource>();
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
                Player_Attack -= _powerAttackNum;
                _powerParticleObj.SetActive(false);
                _powerAttackBool = false;
            }
        }


        _shotCountTime += Time.deltaTime;
        
    }

    /// <summary> プレイヤーを動かす処理 </summary>
    private void Movement() 
    {
        var velo = _rb.velocity.normalized;
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

        velo.x = _movex * _speed;
        velo.y = _movey * _speed;
        _rb.velocity = new Vector2(velo.x, velo.y);
    }

    /// <summary> プレイヤーが弾を出す処理 </summary>
    private void Shot() 
    {
        if (Input.GetKey(KeyCode.Mouse0) && _shotSetInterval < _shotCountTime)
        {
            _audioSource.PlayOneShot(_shotSound);
            Instantiate(_shotBulletPrefab, _shotMuzzule.transform.position, _shotMuzzule.transform.rotation);
            _shotCountTime = 0;
        }
    }

    /// <summary> ダメージを受ける処理 </summary>
    /// <param name="damage"></param>
    public void AddDamage(int damage) 
    {
        //無敵の時はダメージを受けない
        if (!_InvincibleBool)
        {
            SetHp(GetHp() - damage);
        }
        else 
        {
            _InvincibleBool = false;
            _barrier.SetActive(false);
        }
        
        if (GetHp() <= 0) 
        {
            //ゲームオーバー
        }
    }

    

    /// <summary>パワーアップアイテムをとった時の処理</summary>
    public void PowerUp() 
    {
        _powerItemTimeCount = 0;
        //攻撃力を上げる
        Player_Attack += _powerAttackNum;
        _powerParticleObj.SetActive(true);
        _powerAttackBool = true;
    }

    /// <summary>無敵アイテムをとった時の処理</summary>
    public void InvincibleItem() 
    {
        _InvincibleBool = true;
        _barrier.SetActive(true);
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
        Player_Attack = attack;
    }

    public int GetAttack() => Player_Attack;
    #endregion
}
