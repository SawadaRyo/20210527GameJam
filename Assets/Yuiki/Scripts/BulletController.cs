using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour //敵からPlayerへの弾・爆弾から出る弾

{
    //public GameObject damageEffectPrefab; //エフェクト格納用プレハブ
    public float speed = 2.0f;
    public Rigidbody2D rb;
    [SerializeField] int m_damage = 3;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }


    private void OnColligionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "player") //playerに当たったら、弾を破壊しプレイヤーのHPを削る
        {
            //GameObject damageEffect = Instantiate(damageEffectPrefab, transform.position, Quaternion.identity); //破壊時のエフェクト
            Destroy(gameObject, 0.1f);
            PlayerScript.Instance.AddDamage(m_damage);            
        }
    }
}

