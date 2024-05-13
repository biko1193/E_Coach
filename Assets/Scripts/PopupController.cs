using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel; // �˾�â���� ����� �г� ������Ʈ
    public FirstPersonController playerController; // �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ ����
    public GameObject Player;
    public GameObject NPC;

    // ��ȣ�ۿ�ÿ� �÷��̾ ������ �ʵ��� ����
    private void HidePlayer(bool b)
    {
        b = !b;
        foreach (Transform child in Player.transform)
        {
            child.gameObject.SetActive(b);
        }
    }

    // �˾�â�� �ݴ� �޼���
    public void ClosePopup()
    {
        popupPanel.SetActive(false); // �˾�â�� ��Ȱ��ȭ
        playerController.isPopupOpen = false; // �÷��̾� �̵� ���� ����
        NPC.transform.Find("CamNPC").gameObject.SetActive(false);// NPC����ī�޶� ��Ȱ��ȭ
        HidePlayer(false); //�÷��̾� ���� ����
    }

    // �˾�â�� ���� ���� �޼���
    public void OpenPopup()
    {
        popupPanel.SetActive(true); // �˾�â�� Ȱ��ȭ
        playerController.isPopupOpen = true; // �÷��̾� �̵� ����
        NPC.transform.Find("CamNPC").gameObject.SetActive(true);// NPC����ī�޶� Ȱ��ȭ
        HidePlayer(true); //�÷��̾� ����
    }
}
