using UnityEngine;
using UnityEngine.UI;

public class NPCAttribute : MonoBehaviour
{
    // NPC�� �̸��� ������ �����ϴ� ����
    public string NPCName;
    public string NPCRole;

    // UI Text ������Ʈ�� ���� ����
    public Text nameTextPrefab;
    public Text roleTextPrefab;

    // ������ Text ������Ʈ�� ���� �θ� Canvas
    public Canvas NPCNameTagCanvas { get; private set; } // public���� �����Ͽ� �ܺ� ���� ����

    void Start()
    {
        // World Space Canvas ���� �� ����
        GameObject canvasGameObject = new GameObject("NPCNameTagCanvas");
        NPCNameTagCanvas = canvasGameObject.AddComponent<Canvas>();
        NPCNameTagCanvas.renderMode = RenderMode.WorldSpace;
        CanvasScaler cs = canvasGameObject.AddComponent<CanvasScaler>();
        cs.scaleFactor = 10.0f;
        cs.dynamicPixelsPerUnit = 10f;
        RectTransform canvasRectTransform = NPCNameTagCanvas.GetComponent<RectTransform>();
        canvasRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
        canvasRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50f);
        canvasGameObject.transform.SetParent(this.transform);
        canvasGameObject.transform.localPosition = new Vector3(0, 2, 0); // NPC�� �Ӹ� ���� ��ġ�ǵ��� ����

        // �̸��� ������ ���� Text ������Ʈ ���� �� ����
        if (nameTextPrefab && roleTextPrefab)
        {
            // �̸� �ؽ�Ʈ ���� �� ����
            Text nameText = Instantiate(nameTextPrefab, canvasGameObject.transform);
            RectTransform nameTextRectTransform = nameText.GetComponent<RectTransform>();
            nameTextRectTransform.localPosition = new Vector3(0, 0.4f, 0);
            nameTextRectTransform.sizeDelta = new Vector2(100, 20); // �̸� �ؽ�Ʈ�� ũ�� ����
            nameTextRectTransform.localScale = new Vector3(0.025f, 0.025f, 0.025f); // �̸� �ؽ�Ʈ�� ������ ����
            nameText.text = NPCName;

            // ���� �ؽ�Ʈ ���� �� ����
            Text roleText = Instantiate(roleTextPrefab, canvasGameObject.transform);
            RectTransform roleTextRectTransform = roleText.GetComponent<RectTransform>();
            roleTextRectTransform.localPosition = new Vector3(0, 0.2f, 0);
            roleTextRectTransform.sizeDelta = new Vector2(100, 20); // ���� �ؽ�Ʈ�� ũ�� ����
            roleTextRectTransform.localScale = new Vector3(0.025f, 0.025f, 0.025f); // ���� �ؽ�Ʈ�� ������ ����
            roleText.text = NPCRole;
        }
    }

    void Update()
    {
        if (NPCNameTagCanvas && Camera.main && NPCNameTagCanvas.gameObject.activeInHierarchy && Camera.main.gameObject.activeInHierarchy)
        {
            NPCNameTagCanvas.transform.LookAt(Camera.main.transform);
            NPCNameTagCanvas.transform.Rotate(0, 180, 0); // �ؽ�Ʈ�� �Ųٷ� ������ �ʵ��� ȸ��
        }
    }
}
