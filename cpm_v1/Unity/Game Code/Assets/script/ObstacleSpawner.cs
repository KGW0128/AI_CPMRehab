using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;  // �� ������ ���� ���ӽ����̽� �߰�

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject wallPrefab;  // �� ������ (WallPrefab)
    public float spawnRate = 5f;   // �� ���� ���� (�� ����)
    public float minY = -3f;       // ���� �ּ� Y ��ġ
    public float maxY = 3f;        // ���� �ִ� Y ��ġ
    public float gap = 0.5f;       // Y ��ġ ���� (0.5 �������� ����)

    public TMP_Text tmpText;  // TMP �ؽ�Ʈ�� ǥ���� UI �ؽ�Ʈ
    private int score = 0;  // ���� ����

    void Start()
    {
        InvokeRepeating("SpawnWall", 1f, spawnRate);  // ���� �ð����� �� ����
        UpdateScoreUI(); // ���� UI �ʱ�ȭ
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC Ű ����
        {
            Application.Quit(); // ���� ����
        }

        if (Input.GetKeyDown(KeyCode.R)) // R Ű ����
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� �� �ٽ� �ε�
        }
    }

    void SpawnWall()
    {
        float holePosition = Mathf.Round(Random.Range(minY, maxY) / gap) * gap;
        GameObject wall = Instantiate(wallPrefab, new Vector3(10f, holePosition, 0), Quaternion.identity);
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreUI();
    }

    public void minusScore()
    {
        score--;
        if (score <= 0) score = 0;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (tmpText != null)
        {
            tmpText.text = "" + score;
        }
    }
}
