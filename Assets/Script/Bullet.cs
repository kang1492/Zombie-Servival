using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject origin;
    //[SerializeField] ParticleSystem effect;
    [SerializeField] int speed = 5;

    private void Start()
    {   // 3�� �Ѿ� �����.
        Destroy(this.gameObject, 3);
    }

    void Update()
    {   // �Ѿ� ����
        transform.Translate(origin.transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�浹");
        Instantiate(Resources.Load<GameObject>("WFX_Explosion Simple"),
            transform.position,
            transform.rotation
            );


        //effect.Play();
        Destroy(this.gameObject);//0.1�� ������ ���������
    }

}
