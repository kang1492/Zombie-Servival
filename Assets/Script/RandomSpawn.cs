using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField] GameObject zombie;

    void Start()
    {
        //for(int i = 0; i < 10; i++) // �ߵǴ��� Ȯ�� 10���� ������
        // ��ġ ����ֱ�.
        Instantiate(zombie, RandomPosition(), Quaternion.identity);
    }
    
    // ����3�� ��ȯ�ϴ� �Լ�
    public Vector3 RandomPosition() // ���� �����
    {
        // ���� ������
        /*
        // x^2 + y^2 <= r^2
        // ���� �����Ŀ��� ������ x�� z�� �ش��ϴ� ���� ������ r�� �� �ȿ� �����ϴ� �����Դϴ�.
        // x z �̿��ؼ� ������ �׸�. y������

        // �������� ���� 

        // x ������ �������� ������ ���� ������ -���������� +�������� ���̱��� �Դϴ�.
        // x 0.3^2  + z^2 = 1
        // z = ��Ʈ 1^2 -0.3^2
        // z = ��Ʈ 1 - 0.09
        // z = ��Ʈ 0.91 
        // z = 0.95~~ �ٻ簪

        // ������ 1�� ���� ������ (0.3, 0.95)
        */

        // ���� ������Ʈ�� �߽����� ���� ������ 100�� ���� �����մϴ�.
        float radius = 100f;

        // õ ��°�� x���� ����մϴ�.
        // ������ �������� -50, 50 ������ ���� ���� �����մϴ�.
        float x = Random.Range(-radius, radius);

        // ������            //������ �Լ�
        float z = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x, 2));
                                  //������

        // �������� 0�� 1�����ǳ������� �����ϰ� 0�� ������ ���� ������ z�� z ������
        // �־��ָ� �˴ϴ�.

        if(Random.Range(0,2) == 0)
        {
            z = -z;
        }

        return new Vector3(x,0,z); // �� ���� ������ ����
    }
  
}
