using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject origin;
    //[SerializeField] ParticleSystem effect;
    [SerializeField] int speed = 5;

    private void Start()
    {   // 3초 총알 사라짐.
        Destroy(this.gameObject, 3);
    }

    void Update()
    {   // 총알 방향
        transform.Translate(origin.transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 데미지 주기
        if(other.CompareTag("Enemy")) //8 -11
        {
            //Debug.Log("충돌");
            Instantiate
                (
                Resources.Load<GameObject>("WFX_Explosion Simple"),
                transform.position,
                transform.rotation
                 );
                                        // 부모껄 찾기
            
            //8-12
            other.transform.GetComponentInParent<AIControl>().health -= 20;
            other.transform.GetComponentInParent<AIControl>().Death();


              //effect.Play();
            Destroy(this.gameObject);//0.1초 보였다 사라질것임
        }
        
    }

}
