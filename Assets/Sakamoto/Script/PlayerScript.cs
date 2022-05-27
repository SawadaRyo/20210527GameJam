using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D _rb;

    //�X�e�[�^�X
    [SerializeField]private int _player_MaxHp;
    private int _player_Hp;
    [SerializeField]private int _player_Attack;

    //�U���̓A�b�v�A�C�e���Ɏg���ϐ�
    private bool _powerAttackBool;
    private int _powerAttackNum;
    private float _powerItemTime;
    private float _powerItemTimeCount;

    //���G�S���A�C�e�����Ƃ������Ɏg���ϐ�
    private bool _InvincibleBool;
    private float _InvincibleItemTime;
    private float _InvincibleItemCount;

    //�v���C���[�̈ړ����x
    [SerializeField] private float _speed;
    //�ړ��ϐ�
    private float _movex;
    private float _movey;
    //�v���C���[��Transform
    private Vector3 _player_Position;
    //�Ăяo���e�ƃ}�Y��
    [SerializeField] private GameObject _ShotBulletPrefab;
    [SerializeField] private GameObject _shotMazzule;
    //�e���o���Ԋu
    [SerializeField] private float _shotSetInterval;
    private float _shotCountTime;

    void Start()
    {
        _player_Position = GetComponent<Transform>().position;

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
                _player_Attack -= _powerAttackNum;
                _powerAttackBool = false;
            }
        }

        _shotCountTime += Time.deltaTime;
        
    }

    /// <summary>
    /// �v���C���[�𓮂�������
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
    /// �v���C���[���e���o������
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
            //�Q�[���I�[�o�[
        }
    }

    private void OnTiggerEnter2D(Collision collision)
    {
        //�p���[�A�b�v�A�C�e�����Ƃ�����
        if (collision.gameObject.tag == "PowerItem")
        {
            _powerItemTimeCount = 0;
            //�U���͂��グ��
            _player_Attack += _powerAttackNum;
            _powerAttackBool = true;

        }

        //���G�A�C�e�����Ƃ�����
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
