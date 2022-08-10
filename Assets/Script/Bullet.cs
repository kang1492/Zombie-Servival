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
        Debug.Log("충돌");
        Instantiate(Resources.Load<GameObject>("WFX_Explosion Simple"),
            transform.position,
            transform.rotation
            );


        //effect.Play();
        Destroy(this.gameObject);//0.1초 보였다 사라질것임
    }

}
