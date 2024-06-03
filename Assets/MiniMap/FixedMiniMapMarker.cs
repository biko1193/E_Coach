using UnityEngine;

public class FixedMiniMapMarker : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 참조하기 위해 public으로 선언

    void LateUpdate()
    {
        // 마커의 위치를 플레이어의 위치로 설정, y 축은 고정된 높이로 설정
        Vector3 newPosition = player.position;
        newPosition.y += 200; // 마커를 고정할 높이
        transform.position = newPosition;

        // 플레이어의 회전값을 사용하지 않도록 마커의 회전값을 고정
        transform.rotation = Quaternion.Euler(90, 0, 0); // y축을 기준으로 회전하지 않게 고정
    }
}
