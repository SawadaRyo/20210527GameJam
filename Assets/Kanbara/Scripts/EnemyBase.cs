using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public class EnemyBase : MonoBehaviour, IDamageble
{
    [SerializeField]
    [Header("表示するスプライト")]
    Sprite _sprite;

    [SerializeField]
    [Header("倒されたときに加算するスコア")]
    float _score = 1;

    [SerializeField]
    [Header("Enemyの活動時間")]
    float _lifeTime = 15;

    [SerializeField]
    [Header("Enemyの強さ")]
    EnemyLevel _enemyLevel = EnemyLevel.First;

    [SerializeField]
    [Header("Enemyのスピード")]
    float _speed = 5;

    [SerializeField]
    [Header("Enemyが弾を撃つ時のクールタイム(ミリ秒)")]
    int _coolTime = 100;

    [SerializeField]
    [Header("弾を撃つかどうか")]
    bool _isAttack = true;

    [SerializeField]
    [Header("Enemyが撃つ弾")]
    GameObject _bullet;

    [SerializeField]
    [Header("Enemyの動く向き")]
    EnemyDirection _direction = EnemyDirection.Up;

    [SerializeField]
    [Header("弾の発射位置")]
    Transform _muzzle;

    [SerializeField]
    [Header("アイテムを落とす確率のしきい値(この数値以下だとアイテムを落とす)")]
    float _dropNum;

    [SerializeField]
    [Header("落とすアイテムの数")]
    int _itemDropNum = 1;

    [SerializeField]
    [Header("落とすアイテム")]
    GameObject _item;

    Rigidbody2D _rb;
    SpriteRenderer _sp;
    int _life;
    float _time = 0;
    bool _canAttack = true;

    protected virtual void OnEnable()
    {
        _sp = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _sp.sprite = _sprite;
        _life = LevelMatch();
    }

    protected virtual void Update()
    {
        Move();
        if (_isAttack)
        {
            Attack();
        }
        if (_time > _lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnDisable()
    {
        if (transform.parent.TryGetComponent(out INotice notice))
        {
            notice.Notice();
        }
        DropAnItem();
        GameManager.Instance.PlusScore(_score);
    }

    public enum EnemyLevel
    {
        First,
        Second,
        Third
    }

    public enum EnemyDirection
    {
        Up,
        Down,
        Left,
        Right,
        Automatic
    }

    protected virtual int LevelMatch()
    {
        switch (_enemyLevel)
        {
            case EnemyLevel.First:
                return 1;
            case EnemyLevel.Second:
                return 2;
            case EnemyLevel.Third:
                return 3;
            default:
                return 1;
        }
    }

    protected virtual async void Attack()
    {
        _time += Time.deltaTime;
        if (_canAttack)
        {
            Shoot();
            await Task.Delay(_coolTime);
            _canAttack = true;
        }
    }

    protected virtual void Shoot()
    {
        if (_bullet != null)
        {
            Instantiate(_bullet, _muzzle);
        }
        _canAttack = false;
    }

    protected virtual void Move()
    {
        _rb.velocity = DirectionDecision(_direction).normalized * _speed;
    }

    private Vector2 DirectionDecision(EnemyDirection enemyDirection) => enemyDirection switch
    {
        EnemyDirection.Up => Vector2.up,
        EnemyDirection.Down => Vector2.down,
        EnemyDirection.Left => Vector2.left,
        EnemyDirection.Right => Vector2.right,
        EnemyDirection.Automatic => transform.position - PlayerScript.Instance.transform.position,
        _ => throw new System.NotImplementedException()
    };

    protected virtual void DropAnItem()
    {
        if (Random.Range(0f, 100f) < _dropNum)
        {
            for (int i = 0; i < _itemDropNum; i++)
            {
                Instantiate(_item, this.transform);
            }
            Debug.Log(_item.name + "を" + _itemDropNum + "個落としました");
        }
    }

    public void AddDamage(int damage)
    {
        _life -= damage;
        if (_life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
