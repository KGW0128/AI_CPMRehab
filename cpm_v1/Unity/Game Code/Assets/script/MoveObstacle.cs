using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MoveObstacle : MonoBehaviour
{
    public float moveSpeed = 2f;  // �� �̵� �ӵ�

    private ObstacleSpawner Managerobj;


    private void Start()
    {
        // "ObstacleSpawner"��� �̸��� ������Ʈ�� ã�Ƽ� ��ũ��Ʈ ��������
        GameObject Manager = GameObject.Find("Manager");

        if (Manager != null)
        {
            Managerobj = Manager.GetComponent<ObstacleSpawner>();
        }
        else
        {
            Debug.LogError("Manager ������Ʈ�� ã�� �� �����ϴ�!");
        }
    }



    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime; // �������� �̵�

        // ȭ�� ������ ������ ����
        if (transform.position.x < -10f)
        {
            Debug.Log("ȸ�� ����");

            Managerobj.IncreaseScore();


            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject); // �θ� ������ �ڱ� �ڽ� ����
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("���� �浹");

        Managerobj.minusScore();


        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject); // �θ� ������ �ڱ� �ڽ� ����
        }

    }

   
}
