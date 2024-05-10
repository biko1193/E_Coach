using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel; // �˾�â���� ����� �г� ������Ʈ
    public FirstPersonController playerController; // �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ ����

    // �˾�â�� �ݴ� �޼���
    public void ClosePopup()
    {
        popupPanel.SetActive(false); // �˾�â�� ��Ȱ��ȭ
        playerController.isPopupOpen = false; // �÷��̾� �̵� ���� ����
    }

    // �˾�â�� ���� ���� �޼���
    public void OpenPopup()
    {
        popupPanel.SetActive(true); // �˾�â�� Ȱ��ȭ
        playerController.isPopupOpen = true; // �÷��̾� �̵� ����
    }
}
