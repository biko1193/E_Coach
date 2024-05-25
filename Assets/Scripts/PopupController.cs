using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel; // �˾�â���� ����� �г� ������Ʈ
    public FirstPersonController playerController; // �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ ����
    public GameObject Player;
    public GameObject NPC;

    // �˾�â�� �ݴ� �޼���
    public void ClosePopup()
    {
        Player.SetActive(true); // ��ȭ ���� �� �÷��̾� Ȱ��ȭ
        popupPanel.SetActive(false); // �˾�â�� ��Ȱ��ȭ
        playerController.isPopupOpen = false; // �÷��̾� �̵� ���� ����
        NPC.transform.Find("CamNPC").gameObject.SetActive(false);
        Player.transform.Find("Camera").gameObject.SetActive(true);
    }

    // �˾�â�� ���� ���� �޼���
    public void OpenPopup()
    {
        popupPanel.SetActive(true); // �˾�â�� Ȱ��ȭ
        playerController.isPopupOpen = true; // �÷��̾� �̵� ����
        // ī�޶� ���� ��ȯ
        NPC.transform.Find("CamNPC").gameObject.SetActive(true);
        Player.transform.Find("Camera").gameObject.SetActive(false);
    }
}
