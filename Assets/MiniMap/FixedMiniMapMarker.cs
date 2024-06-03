using UnityEngine;

public class FixedMinimapMarker : MonoBehaviour
{
    private Vector3 initialLocalPosition;

    void Start()
    {
        // 초기 로컬 위치를 저장합니다.
        initialLocalPosition = transform.localPosition;
    }

    void LateUpdate()
    {
        // 로컬 위치를 초기 위치로 고정시킵니다.
        transform.localPosition = initialLocalPosition;
    }
}