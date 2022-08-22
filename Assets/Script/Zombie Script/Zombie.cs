using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public int health; // 8-22
    //private RandomSpawn spawn; //8-22 ��������;
    private Animator animator; //8-22
    private GameObject Character;
    private NavMeshAgent agent;

    void Start()
    {
        // ĳ���� �̸� ã��
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Character = GameObject.Find("Character"); //8-22
        //spawn = GameObject.Find("Random Spawn").GetComponent<RandomSpawn>();// 8-22
            // ���� ���� ���� 
    }

    // ���� ������Ʈ�� ��Ȱ��ȭ�Ǿ��� �� ȣ��Ǵ� �Լ��Դϴ�.
    //private void OnEnable() // 8-22
    //{
        // ��ġ�� �ٽ� ���
        //transform.position = spawn.RandomPosition();
        
    //} // ��� �ɷ��� ����


    void Update()
    {
        // ���� ���� ����
        agent.SetDestination(Character.transform.position);
    }

    public void Death() // 8-12 �̵�
    {
        if (health <= 0)
        {
            //CancelInvoke(); //8-12
            agent.speed = 0; //8-12

            animator.Play("Death"); //8-12
                                    //Destroy(gameObject, 3); // ���ӿ�����Ʈ �ı� 8-12

            transform.position = ObjectPool.instance.ActivePostion(); //8-22

            // �ִϸ����� ��Ʈ�ѷ����� ���� �ִϸ������� ������ �̸��̡�close���� �� 
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                // ���� �ִϸ��̼��� ���൵�� 1���� ũ�ų� ���ٸ� User Interface�� ��Ȱ��ȭ�մϴ�.
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    ObjectPool.instance.InsertQueue(gameObject); //8-22
                }
            }
            // �״� ��� , �ٷ� �޸𸮿� �ݳ��ȵǰ�. ����� �ݳ��ǰ� ��

            
            
        }
    }
}
