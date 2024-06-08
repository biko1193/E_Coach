using UnityEngine;
using UnityEngine.UI;

public class MiniMapPlayerMarker : MonoBehaviour
{
    public Transform player;       // 플레이어의 Transform
    public RectTransform marker;   // 미니맵 마커의 RectTransform
    public RectTransform miniMapRect; // 미니맵의 RectTransform

    private Vector2 miniMapSize;   // 미니맵의 크기

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
        // 월드 좌표계를 미니맵 좌표계로 변환
        Vector2 miniMapPosition = WorldToMiniMapPosition(player.position);

        // 마커 위치 업데이트
        marker.anchoredPosition = miniMapPosition;
    }

    Vector2 WorldToMiniMapPosition(Vector3 worldPosition)
    {
        // 월드 좌표를 미니맵 좌표로 변환하는 로직을 구현
        // 예시로, 월드 좌표를 미니맵의 크기에 맞게 비율로 변환하는 방법을 사용

        float mapWidth = miniMapSize.x;
        float mapHeight = miniMapSize.y;

        float xRatio = worldPosition.x / mapWidth;
        float yRatio = worldPosition.z / mapHeight;

        return new Vector2(xRatio * miniMapSize.x, yRatio * miniMapSize.y);
    }
}
