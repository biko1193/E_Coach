using UnityEngine;

public class FixedMinimapMarker : MonoBehaviour
{
    private Vector3 initialLocalPosition;

    void Start()
    {
        // �ʱ� ���� ��ġ�� �����մϴ�.
        initialLocalPosition = transform.localPosition;
    }

    void LateUpdate()
    {
        // ���� ��ġ�� �ʱ� ��ġ�� ������ŵ�ϴ�.
        transform.localPosition = initialLocalPosition;
    }
}