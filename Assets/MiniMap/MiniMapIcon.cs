using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    public Transform player;  // �÷��̾� Transform�� ������ ����
    public RectTransform miniMapIcon;  // �÷��̾� ������ UI�� RectTransform
    public RectTransform miniMapRectTransform;  // �̴ϸ��� RectTransform
    public Vector2 mapBoundsMin = new Vector2(-200, -400);  // ���� �ּ� ��� (���� ��ǥ)
    public Vector2 mapBoundsMax = new Vector2(1000, 400);  // ���� �ִ� ��� (���� ��ǥ)

    void Update()
    {
        // �÷��̾��� ���� ��ǥ�� �̴ϸ� ĵ���� ��ǥ�� ��ȯ
        float normalizedX = Mathf.InverseLerp(mapBoundsMin.x, mapBoundsMax.x, player.position.x);
        float normalizedY = Mathf.InverseLerp(mapBoundsMin.y, mapBoundsMax.y, player.position.z); // Y�� Z ��ǥ�� ���� Ȯ��

        float miniMapWidth = miniMapRectTransform.rect.width;
        float miniMapHeight = miniMapRectTransform.rect.height;

        Vector2 miniMapIconPosition = new Vector2(
            normalizedX * miniMapWidth,
            normalizedY * miniMapHeight
        );

        miniMapIcon.anchoredPosition = miniMapIconPosition;
    }
}
