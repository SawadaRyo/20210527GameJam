using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyGenerator : MonoBehaviour, INotice
{
    public GameObject Player { get; private set; }

    [SerializeField]
    [Header("生成するEnemy")]
    GameObject _enemy;

    [SerializeField]
    [Header("インターバル(ミリ秒)")]
    int _interval = 100;

    [SerializeField]
    [Header("同時に出現可能なEnemyの数")]
    int _enemyNumLimit;

    [SerializeField]
    [Header("Playerの名前")]
    string _playerName = "playerPefab";

    [SerializeField]
    [Header("スポーン位置を変えるかどうか")]
    bool _isChangeSpawnPosition = false;

    [SerializeField]
    [Header("移動スピード")]
    float _speed;

    [SerializeField]
    [Header("動く方向")]
    Direction _direction = Direction.Vertical;

    [SerializeField]
    [Header("可動域の上限")]
    float _rangeOfMotionUpper;

    [SerializeField]
    [Header("可動域の下限")]
    float _rangeOfMotionLower;

    [SerializeField]
    [Header("可動域の左端")]
    float _rangeOfMotionLeft;

    [SerializeField]
    [Header("可動域の右端")]
    float _rangeOfMotionRight;

    Rigidbody2D _rb;
    float _time;
    bool _canGanerate = true;
    bool _isDefault = true;
    int _enemyNum = 0;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Player = GameObject.Find(_playerName);
    }

    private async void Update()
    {
        _time += Time.deltaTime;
        if (_canGanerate)
        {
            Ganerate();
            await Task.Delay(_interval);
            _canGanerate = true;
        }
        if (_isChangeSpawnPosition)
        {
            Move();
        }
    }

    enum Direction
    {
        Vertical,
        Horizontal
    }

    private void Ganerate()
    {
        if(_enemyNumLimit <= _enemyNum)
        {
            return;
        }
        if (_enemy != null)
        {
            Instantiate(_enemy, this.transform);
            _enemyNum++;
        }
        _canGanerate = false;
    }

    private void Move()
    {
        float x = 0f;
        float y = 0f;
        switch (_direction)
        {
            case Direction.Vertical:
                y = _speed;
                break;
            case Direction.Horizontal:
                x = _speed;
                break;
        }
        _rb.velocity = new Vector2(x, y);
        CheckPosition();
    }

    private void CheckPosition()
    {
        if (_direction == Direction.Vertical)
        {
            if (transform.position.y >= _rangeOfMotionUpper && _isDefault)
            {
                _speed *= -1;
                _isDefault = false;
            }
            else if(transform.position.y <= _rangeOfMotionLower && !_isDefault)
            {
                _speed *= -1;
                _isDefault = true;
            }
        }
        else if (_direction == Direction.Horizontal)
        {
            if (transform.position.x >= _rangeOfMotionRight && _isDefault)
            {
                _speed *= -1;
                _isDefault = false;
            }
            else if(transform.position.x <= _rangeOfMotionLeft && !_isDefault)
            {
                _speed *= -1;
                _isDefault = true;
            }
        }
    }

    public void Notice()
    {
        _enemyNum--;
    }
}
