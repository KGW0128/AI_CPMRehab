using UnityEngine;
using System.IO.Ports;

public class stm32_test : MonoBehaviour
{
    SerialPort serial;
    public GameObject Cube; // ȸ����ų ť�� ������Ʈ

    private float midpoint = 2048.0f; // �������� ���� ������ (40 ~ 4085 ���ؿ��� �߰���)
    private float maxADC = 4085.0f;  // �������� ���� �ִ밪
    private float speedMultiplier = 500.0f; // ȸ�� �ӵ� ��� (���� ũ�� �ؼ� �ӵ� ����)

    private void Awake()
    {
        serial = new SerialPort("COM6", 115200); // STM32 ��Ʈ�� ������ ����
        serial.Open();
    }

    void Update()
    {
        if (serial.IsOpen)
        {
            try
            {
                // STM32���� �����͸� �о�ɴϴ�
                string data = serial.ReadLine();
                Debug.Log("Received: " + data);

                // �����͸� float ������ ��ȯ
                if (float.TryParse(data, out float adcValue))
                {
                    RotateCube(adcValue); // ť�� ȸ�� �Լ� ȣ��
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }

    /// <summary>
    /// ť�긦 �������� ���� ���� ȸ����ŵ�ϴ�.
    /// </summary>
    /// <param name="adcValue">�������� �� (40 ~ 4085)</param>
    private void RotateCube(float adcValue)
    {
        // ���������� �Ÿ� ���
        float distanceFromMidpoint = Mathf.Abs(adcValue - midpoint);

        // ȸ�� �ӵ� ��� (���������� �־������� ������ ȸ��)
        float rotationSpeed = (distanceFromMidpoint / maxADC) * speedMultiplier;

        // ȸ�� ���� ���� (���������� ������ ����, ũ�� ������)
        if (adcValue < midpoint)
        {
            rotationSpeed = -rotationSpeed; // ���� ȸ��
        }

        // ť�� ȸ�� ����
        Cube.transform.Rotate( rotationSpeed * Time.deltaTime, 0, 0);
    }

    private void OnApplicationQuit()
    {
        if (serial.IsOpen)
            serial.Close();
    }
}
