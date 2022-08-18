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
        // ������ �ֱ�
        if(other.CompareTag("Enemy")) //8 -11
        {
            //Debug.Log("�浹");
            Instantiate
                (
                Resources.Load<GameObject>("WFX_Explosion Simple"),
                transform.position,
                transform.rotation
                 );
                                        // �θ� ã��
            
            //8-12
            other.transform.GetComponentInParent<AIControl>().health -= 20;
            other.transform.GetComponentInParent<AIControl>().Death();


              //effect.Play();
            Destroy(this.gameObject);//0.1�� ������ ���������
        }
        
    }

}
