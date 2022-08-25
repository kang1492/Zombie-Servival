using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public int health = 100; //8-23 
    [SerializeField] float axisSpeed = 5.0f; // ī�޶� x��� y���� ȸ�� �ӵ�
    [SerializeField] GameObject eye;


    private float eulerAngleX;
    private float eulerAngleY;

    private CharacterController characterControl;
    private Vector3 moveForce;
    [SerializeField] float distance = 100.0f; //8-18 ������ 100 ���� ���� �߻� �ȴ�.
    [SerializeField] float speed;
    [SerializeField] float gravity = 20.0f;
    [SerializeField] ParticleSystem effect;
    //[SerializeField] GameObject bullet; // �Ѿ� ����� 8-18 �Ѿ� ����
    [SerializeField] LayerMask layer; // 8-18
    

    void Start()
    {
        // Ŀ�� ��Ȱ��ȭ
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        characterControl = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        UpdateRotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if(Input.GetButtonDown("Fire1"))
        {
            effect.Play();
            SoundSystem.instance.Sound(0); // 8-12 �ѼҸ�
            TwoStepRay(); //8-18, ȣ���ϱ�
            //Instantiate(bullet, effect.transform.position, effect.transform.rotation); 8-18 ������ ������
            // �ѱ� ����
        }

        

        MoveTo
            (
                 new Vector3
                 (
                     Input.GetAxis("Horizontal"), 
                     0, 
                     Input.GetAxis("Vertical")
                  )
             );

        // �ٴڰ� �⵹���� �ʾҴٸ�
        if(characterControl.isGrounded == false)
        {
            // �߷��� �ް� ����
            moveForce.y -= gravity * Time.deltaTime;
        }
        //else ���� �ϱ� , �ٴڿ� ���� �ʾҴٸ� �߷��� �ް� ����.
        //{
            //moveForce.y = 0.1f;
        //}

        characterControl.Move(moveForce * Time.deltaTime);

        Jump(); //8-19 ����
    }

    public void Jump() // 8-19
    {
        // �����̽� ������ ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ���ٴڿ� �ٿ����� �����ϱ�
            if (characterControl.isGrounded)
            {
                //Debug.Log("����"); // Ȯ���ϱ�
                
                // ������ �� �� �ֵ��� �����մϴ�.
                moveForce.y = 7.5f;
                // y ������ 7.5 �̵�
            }
        }
    }

    public void MoveTo(Vector3 direction)
    {
        // ī�޶� ȸ������ ���� ������ ���ϱ� ������ ȸ�� ���� ���ؼ� �����մϴ�.
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        // ��/ �Ʒ��� �ٶ󺸰� �̵��� �� ĳ���� ������Ʈ�� �������� �̵��ϰų� �Ʒ��� ����ɱ� ������
        // direction�� �״�� ������� �ʰ�, moveForce ������ x�� z���� �־ ����մϴ�.
        moveForce = new Vector3(direction.x * speed, moveForce.y, direction.z * speed);
    }

    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * axisSpeed; // ���콺 ��/�� �̵����� ī�޶� y�� ȸ��

        // ���콺 �Ʒ��� ������ -�� �����δ� ������Ʈ�� x���� + �������� ȸ���ؾ� �Ʒ��� ���� �����Դϴ�.
        eulerAngleX -= mouseY * axisSpeed; // ���콺 ��/�Ʒ� �̵����� ī�޶� x�� ȸ��

        // ī�޶� x�� ȸ���� ��� ���� ������ �����մϴ�. ���� 360 ȸ����. �Ʒ� ���� ���� ����.
        eulerAngleX = ClampAngle(eulerAngleX, -80, 50);

        //transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
        transform.rotation = Quaternion.Euler(transform.rotation.x, eulerAngleY, 0); // ������ ó��

        eye.transform.rotation = Quaternion.Euler(eulerAngleX, transform.eulerAngles.y, 0);
    }

    public float ClampAngle(float angle, float min, float max)
    {// float ��ȯ�� �ʿ�
        return Mathf.Clamp(angle, min, max);
    }

    public void TwoStepRay() //8-18
    {   
        // ������ �־�� ��. ������
        Ray ray;

        // ���C�� �༮�� ������ �ʿ���
        RaycastHit hit;
        Vector3 target = Vector3.zero;

        // ȭ���� �߾� ��ǥ (Cross Hair�� �������� Raycast�� �����մϴ�.)

        // ray �ʿ���                        ������ ��� ������
        ray = Camera.main.ViewportPointToRay(Vector2.one * 0.5f);

        // �Ѿ��� ����� �߻� �Ǿ����� ���� ó�� �ʿ�
        // ���� ��Ÿ� �ȿ� �ε����� ������Ʈ�� ������ target�� ������ �ε��� ��ġ�� �����մϴ�.
                        // ���� �־��ְ�  ������ 100���� ���� �߻� �ȴ�
        if(Physics.Raycast(ray, out hit, distance))
        {
            // hit = ��ü�� ��ġ�� �˼� ����
            target = hit.point;
            Instantiate(effect, hit.point, hit.transform.rotation); // 8-22
            //          ����Ʈ   ����. ��ġ / ��ֹ����� �ѽ�� ����Ʈ ����

        }

        // ���� ��Ÿ� �ȿ� �ε����� ������Ʈ�� ������ targer �����ʹ� �ִ� ��Ÿ��� ��ġ�� �����մϴ�.
        else // ����� ������
        {
                    // ������ .   ����..           ��ġ����
            target = ray.origin + ray.direction * distance;
        }

        // Ȯ���ϱ�
        //Debug.Log(target); Ȯ���� �����

        // ù ��° Raycast �������� ����� targer�� ������ ��ǥ�������� �����ϰ�,
        // �ѱ� �Ա����� Raycast�� �߻��մϴ�.

        //                           - �ѱ� ��ġ ���� (��ƼŬ ��ġ ��������)
        Vector3 direction = (target - effect.transform.position).normalized;

        //         �����ֱ�( ���� ��ġ                 ����                           ���⿡ �߰�          
        if(Physics.Raycast(effect.transform.position, direction, out hit, distance, layer))
        {
            //if(hit.collider == null) // �ƹ��볪 ��� ���� ���� ���� ó��
            //{
            //    return;
            //}

            // �浹�� ��ü��
            hit.collider.GetComponentInParent<Zombie>().health -=20;

            //hit.collider.GetComponentInParent<Zombie>().Death(); //8-25 �����

            // ���ӽ� ���� Ȯ�� ����                                         ���� ����  , �� 10�ʰ� ���̰�
            //Debug.DrawLine(effect.transform.position, direction * distance, Color.red, 10); Ȯ���� �����

            //Instantiate(effect, hit.transform.position, hit.transform.rotation);// �ѱ� ����Ʈ ����
            // �� �� ��ġ���� ��ƼŬ ������.

        }
    }



}
