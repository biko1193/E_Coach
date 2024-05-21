using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public PopupController popupController; // Inspector���� �Ҵ��� PopupController ������Ʈ

    private bool isPlayerInRange = false;

    void Update()
    {
        // �÷��̾ ���� ���� �ְ�, 'F' Ű�� ������
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            // �˾�â�� Ȱ��ȭ ���¸� ���
            if (popupController.popupPanel.activeSelf)
            {
                popupController.ClosePopup();
            }
            else
            {
                popupController.OpenPopup();
            }
        }
    }

    // �÷��̾ NPC�� Collider ���� ������ ���� ��
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Player �±׸� ����Ͽ� �÷��̾� ����
        {
            isPlayerInRange = true;
        }
    }

    // �÷��̾ NPC�� Collider ������ ��� ��
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            popupController.ClosePopup(); // �÷��̾ ������ ����� �˾�â ��Ȱ��ȭ
        }
    }
}
