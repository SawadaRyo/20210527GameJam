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

    //�X�e�[�^�X
    [Header("�X�e�[�^�X")]
    [SerializeField]private int _player_MaxHp;
    private int _player_Hp;
    public int Player_Attack;

    //�U���̓A�b�v�A�C�e���Ɏg���ϐ�
    private bool _powerAttackBool;
    private int _powerAttackNum = 5;
    [Header("�p���[�A�b�v�A�C�e���̌��ʎ���")] 

    [SerializeField]private float _powerItemTime;
    private float _powerItemTimeCount;
    [Header("�p���[�A�b�v�p�[�e�B�N��")]

    [SerializeField] private GameObject _powerParticleObj;

    //���G�S���A�C�e�����Ƃ������Ɏg���ϐ�
    private bool _InvincibleBool;

    //�v���C���[�̈ړ����x
    [Header("�v���C���[�̈ړ����x")]
    [SerializeField] private float _speed;
    //�ړ��ϐ�
    private float _movex;
    private float _movey;

    //�v���C���[��Transform
    private Vector3 _player_Position;

    [Header("�Ăяo���e�ƃ}�Y��")]
    [SerializeField] private GameObject _shotBulletPrefab;
    [SerializeField] private GameObject _shotMuzzule;

    [Header("�e���o���Ԋu")]
    [SerializeField] private float _shotSetInterval;

    //�I�[�f�B�I
    private AudioSource _audioSource;
    private AudioClip _shotSound;

    [Header("�o���A�̃C���[�W�̃I�u�W�F�N�g")]
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
        //�e���o������
        Shot();
        //�v���C���[�𓮂�������
        Movement();

        //�p���[�A�b�v�A�C�e�����Ƃ�����
        if (_powerAttackBool) 
        {
           _powerItemTimeCount += Time.deltaTime;

            if (_powerItemTime < _powerItemTimeCount) 
            {
                //�U���͂�������
                Player_Attack -= _powerAttackNum;
                _powerParticleObj.SetActive(false);
                _powerAttackBool = false;
            }
        }


        _shotCountTime += Time.deltaTime;
        
    }

    /// <summary> �v���C���[�𓮂������� </summary>
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

    /// <summary> �v���C���[���e���o������ </summary>
    private void Shot() 
    {
        if (Input.GetKey(KeyCode.Mouse0) && _shotSetInterval < _shotCountTime)
        {
            _audioSource.PlayOneShot(_shotSound);
            Instantiate(_shotBulletPrefab, _shotMuzzule.transform.position, _shotMuzzule.transform.rotation);
            _shotCountTime = 0;
        }
    }

    /// <summary> �_���[�W���󂯂鏈�� </summary>
    /// <param name="damage"></param>
    public void AddDamage(int damage) 
    {
        //���G�̎��̓_���[�W���󂯂Ȃ�
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
            //�Q�[���I�[�o�[
        }
    }

    

    /// <summary>�p���[�A�b�v�A�C�e�����Ƃ������̏���</summary>
    public void PowerUp() 
    {
        _powerItemTimeCount = 0;
        //�U���͂��グ��
        Player_Attack += _powerAttackNum;
        _powerParticleObj.SetActive(true);
        _powerAttackBool = true;
    }

    /// <summary>���G�A�C�e�����Ƃ������̏���</summary>
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
