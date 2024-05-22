using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public PopupController popupController; // Inspector���� �Ҵ��� PopupController ������Ʈ

    public GameObject Player;
    private Animator animator; // Animator ������Ʈ
    private bool isPlayerInRange = false;

    void Start()
    {
        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // �÷��̾ ���� ���� �ְ�, 'F' Ű�� ������
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            // �˾�â�� Ȱ��ȭ ���¸� ���
            if (popupController.popupPanel.activeSelf)
            {

                ClosePopup();
            }
            else
            {
                OpenPopup();
            }
        }

        // ��ȭâ�� ���� ���¿��� 'K' Ű�� ������
        if (popupController.popupPanel.activeSelf && Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("isSurprised");
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
            ClosePopup(); // �÷��̾ ������ ����� �˾�â ��Ȱ��ȭ
        }
    }

    // ��ȭâ�� ���� �޼���
    private void OpenPopup()
    {
        popupController.NPC = gameObject;
        popupController.Player = Player;
        popupController.OpenPopup();
        Player.SetActive(false); // ��ȭ ���� �� �÷��̾� ��Ȱ��ȭ
        Cursor.lockState = CursorLockMode.None; // ���콺 Ŀ�� ��� ����
        Cursor.visible = true; // ���콺 Ŀ�� ���̱�
    }

    // ��ȭâ�� �ݴ� �޼���
    private void ClosePopup()
    {
        popupController.ClosePopup();
        Player.SetActive(true); // ��ȭ ���� �� �÷��̾� Ȱ��ȭ
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ���
        Cursor.visible = false; // ���콺 Ŀ�� �����
    }
}
