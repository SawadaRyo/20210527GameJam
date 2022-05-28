using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] int m_damage = 5;
    //public GameObject damageEffectPrefab; //エフェクト格納用


    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerScript.Instance.AddDamage(m_damage); //PlayerスクリプトのAddDamageメソッドを呼び出す

        if (other.gameObject.tag == "player") //playerに当たったら、障害物を破壊する
        {
            //GameObject damageEffect = Instantiate(damageEffectPrefab, transform.position, Quaternion.identity); //破壊時のエフェクト
            Destroy(gameObject, 0.1f);
        }
    }
}

