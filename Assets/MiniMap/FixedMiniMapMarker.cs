using UnityEngine;

public class FixedMiniMapMarker : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform�� �����ϱ� ���� public���� ����

    void LateUpdate()
    {
        // ��Ŀ�� ��ġ�� �÷��̾��� ��ġ�� ����, y ���� ������ ���̷� ����
        Vector3 newPosition = player.position;
        newPosition.y += 200; // ��Ŀ�� ������ ����
        transform.position = newPosition;

        // �÷��̾��� ȸ������ ������� �ʵ��� ��Ŀ�� ȸ������ ����
        transform.rotation = Quaternion.Euler(90, 0, 0); // y���� �������� ȸ������ �ʰ� ����
    }
}
