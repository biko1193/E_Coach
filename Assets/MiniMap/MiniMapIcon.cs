using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    public Transform player;  // 플레이어 Transform을 연결할 변수
    public RectTransform miniMapIcon;  // 플레이어 아이콘 UI의 RectTransform
    public RectTransform miniMapRectTransform;  // 미니맵의 RectTransform
    public Vector2 mapBoundsMin = new Vector2(-200, -400);  // 맵의 최소 경계 (월드 좌표)
    public Vector2 mapBoundsMax = new Vector2(1000, 400);  // 맵의 최대 경계 (월드 좌표)

    void Update()
    {
        // 플레이어의 월드 좌표를 미니맵 캔버스 좌표로 변환
        float normalizedX = Mathf.InverseLerp(mapBoundsMin.x, mapBoundsMax.x, player.position.x);
        float normalizedY = Mathf.InverseLerp(mapBoundsMin.y, mapBoundsMax.y, player.position.z); // Y와 Z 좌표의 맵핑 확인

        float miniMapWidth = miniMapRectTransform.rect.width;
        float miniMapHeight = miniMapRectTransform.rect.height;

        Vector2 miniMapIconPosition = new Vector2(
            normalizedX * miniMapWidth,
            normalizedY * miniMapHeight
        );

        miniMapIcon.anchoredPosition = miniMapIconPosition;
    }
}
