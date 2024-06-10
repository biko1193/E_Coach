using UnityEngine;
using UnityEngine.UI;

public class MiniMapPlayerMarker : MonoBehaviour
{
    public Transform player;       // �÷��̾��� Transform
    public RectTransform marker;   // �̴ϸ� ��Ŀ�� RectTransform
    public RectTransform miniMapRect; // �̴ϸ��� RectTransform

    private Vector2 miniMapSize;   // �̴ϸ��� ũ��

    void Start()
    {
        miniMapSize = miniMapRect.sizeDelta;
    }

    void Update()
    {
        UpdateMarkerPosition();
    }

    void UpdateMarkerPosition()
    {
        // ���� ��ǥ�踦 �̴ϸ� ��ǥ��� ��ȯ
        Vector2 miniMapPosition = WorldToMiniMapPosition(player.position);

        // ��Ŀ ��ġ ������Ʈ
        marker.anchoredPosition = miniMapPosition;
    }

    Vector2 WorldToMiniMapPosition(Vector3 worldPosition)
    {
        // ���� ��ǥ�� �̴ϸ� ��ǥ�� ��ȯ�ϴ� ������ ����
        // ���÷�, ���� ��ǥ�� �̴ϸ��� ũ�⿡ �°� ������ ��ȯ�ϴ� ����� ���

        float mapWidth = miniMapSize.x;
        float mapHeight = miniMapSize.y;

        float xRatio = worldPosition.x / mapWidth;
        float yRatio = worldPosition.z / mapHeight;

        return new Vector2(xRatio * miniMapSize.x, yRatio * miniMapSize.y);
    }
}
