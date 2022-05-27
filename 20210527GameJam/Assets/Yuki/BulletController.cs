using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //public GameObject damageEffectPrefab; //弾が当たったときのエフェクト格納用
    public float speed = 2.0f;
    public Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        rb.AddForce(Vector3.forward * speed * Time.deltaTime); //弾が前に進む
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "enemy"　|| other.gameObject.tag == "player") //enemyかplayerをタグに持つオブジェクトに当たったら、弾を破壊する
        {
            //GameObject damageEffect = Instantiate(damageEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.1f);
        }
    }
}

