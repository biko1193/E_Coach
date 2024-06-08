using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public PopupController popupController; // Inspector에서 할당할 PopupController 컴포넌트

    public GameObject Player;
    private Animator animator; // Animator 컴포넌트
    private bool isPlayerInRange = false;
    private NPCAttribute npcAttribute;
    void Start()
    {
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();
        npcAttribute = GetComponent<NPCAttribute>();
    }

    void Update()
    {
        // 플레이어가 범위 내에 있고, 'F' 키를 누르면
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            // 팝업창의 활성화 상태를 토글
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

        // 대화창이 열린 상태에서 'f1' 키를 누르면
        if (popupController.popupPanel.activeSelf && Input.GetKeyDown(KeyCode.F1))
        {
            animator.SetTrigger("isSurprised");

        }
    }

    // 플레이어가 NPC의 Collider 범위 안으로 들어올 때
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Player 태그를 사용하여 플레이어 구별
        {
            isPlayerInRange = true;
        }
    }

    // 플레이어가 NPC의 Collider 범위를 벗어날 때
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            ClosePopup(); // 플레이어가 범위를 벗어나면 팝업창 비활성화
        }
    }

    // 대화창을 여는 메서드
    private void OpenPopup()
    {
        popupController.NPC = gameObject;
        popupController.Player = Player;
        popupController.OpenPopup();
        Player.SetActive(false); // 대화 시작 시 플레이어 비활성화
        npcAttribute.NPCNameTagCanvas.gameObject.SetActive(false); // 대화 시작 시 NPC 이름표 비활성화
        Cursor.lockState = CursorLockMode.None; // 마우스 커서 잠금 해제
        Cursor.visible = true; // 마우스 커서 보이기


    }

    // 대화창을 닫는 메서드
    private void ClosePopup()
    {
        Player.SetActive(true); // 대화 종료 시 플레이어 활성화
        popupController.ClosePopup();
        npcAttribute.NPCNameTagCanvas.gameObject.SetActive(true);  // 대화 종료 시 NPC 이름표 활성화
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 잠금
        Cursor.visible = false; // 마우스 커서 숨기기
    }
}
