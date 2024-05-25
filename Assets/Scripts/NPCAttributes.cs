using UnityEngine;
using UnityEngine.UI;

public class NPCAttribute : MonoBehaviour
{
    // NPC의 이름과 역할을 정의하는 변수
    public string NPCName;
    public string NPCRole;

    // UI Text 컴포넌트를 위한 변수
    public Text nameTextPrefab;
    public Text roleTextPrefab;

    // 생성될 Text 오브젝트를 위한 부모 Canvas
    public Canvas NPCNameTagCanvas { get; private set; } // public으로 변경하여 외부 접근 가능

    void Start()
    {
        // World Space Canvas 생성 및 설정
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
        canvasGameObject.transform.localPosition = new Vector3(0, 2, 0); // NPC의 머리 위에 배치되도록 조정

        // 이름과 역할을 위한 Text 오브젝트 생성 및 설정
        if (nameTextPrefab && roleTextPrefab)
        {
            // 이름 텍스트 생성 및 설정
            Text nameText = Instantiate(nameTextPrefab, canvasGameObject.transform);
            RectTransform nameTextRectTransform = nameText.GetComponent<RectTransform>();
            nameTextRectTransform.localPosition = new Vector3(0, 0.4f, 0);
            nameTextRectTransform.sizeDelta = new Vector2(100, 20); // 이름 텍스트의 크기 조정
            nameTextRectTransform.localScale = new Vector3(0.025f, 0.025f, 0.025f); // 이름 텍스트의 스케일 조정
            nameText.text = NPCName;

            // 역할 텍스트 생성 및 설정
            Text roleText = Instantiate(roleTextPrefab, canvasGameObject.transform);
            RectTransform roleTextRectTransform = roleText.GetComponent<RectTransform>();
            roleTextRectTransform.localPosition = new Vector3(0, 0.2f, 0);
            roleTextRectTransform.sizeDelta = new Vector2(100, 20); // 역할 텍스트의 크기 조정
            roleTextRectTransform.localScale = new Vector3(0.025f, 0.025f, 0.025f); // 역할 텍스트의 스케일 조정
            roleText.text = NPCRole;
        }
    }

    void Update()
    {
        if (NPCNameTagCanvas && Camera.main && NPCNameTagCanvas.gameObject.activeInHierarchy && Camera.main.gameObject.activeInHierarchy)
        {
            NPCNameTagCanvas.transform.LookAt(Camera.main.transform);
            NPCNameTagCanvas.transform.Rotate(0, 180, 0); // 텍스트가 거꾸로 보이지 않도록 회전
        }
    }
}
