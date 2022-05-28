using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController2 : MonoBehaviour@//Player‚©‚ç“G‚Ö‚Ì’e
{
    //public GameObject damageEffectPrefab; //ƒGƒtƒFƒNƒgŠi”[—p
    public float speed = 2.0f;
    public Rigidbody2D rb;
    [SerializeField] int m_damage = 3;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamageble enemyBase)) //enemy‚ğ’Ê‰ß‚µ‚½‚çAenemy‚Æ’e‚ğ”j‰ó‚·‚é
        {
            Destroy(other.gameObject, 0.1f);
            Destroy(gameObject, 0.1f);
            enemyBase.AddDamage(m_damage);
        }
    }

}


