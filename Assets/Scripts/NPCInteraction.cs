using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public PopupController popupController; // Inspector���� �Ҵ��� PopupController ������Ʈ

    public GameObject Player;
    private Animator animator; // Animator ������Ʈ
    private bool isPlayerInRange = false;
    private NPCAttribute npcAttribute;
    void Start()
    {
        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();
        npcAttribute = GetComponent<NPCAttribute>();
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
                animator.SetTrigger("Greeting");
                //animator.ResetTrigger("Greeting");
            }
        }

        // ��ȭâ�� ���� ���¿��� 'f1' Ű�� ������
        if (popupController.popupPanel.activeSelf && Input.GetKeyDown(KeyCode.F1))
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
        npcAttribute.NPCNameTagCanvas.gameObject.SetActive(false); // ��ȭ ���� �� NPC �̸�ǥ ��Ȱ��ȭ
        Cursor.lockState = CursorLockMode.None; // ���콺 Ŀ�� ��� ����
        Cursor.visible = true; // ���콺 Ŀ�� ���̱�


    }

    // ��ȭâ�� �ݴ� �޼���
    private void ClosePopup()
    {
        Player.SetActive(true); // ��ȭ ���� �� �÷��̾� Ȱ��ȭ
        popupController.ClosePopup();
        npcAttribute.NPCNameTagCanvas.gameObject.SetActive(true);  // ��ȭ ���� �� NPC �̸�ǥ Ȱ��ȭ
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ���
        Cursor.visible = false; // ���콺 Ŀ�� �����
    }
}
