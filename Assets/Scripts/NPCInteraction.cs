using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public PopupController popupController; // Inspector에서 할당할 PopupController 컴포넌트
    public GameObject Player;

    private bool isPlayerInRange = false;

    void Update()
    {
        // 플레이어가 범위 내에 있고, 'F' 키를 누르면
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            // 팝업창의 활성화 상태를 토글
            if (popupController.popupPanel.activeSelf)
            {
                //popupController.ClosePopup();
            }
            else
            {
                popupController.NPC = gameObject;
                popupController.Player = Player;
                popupController.OpenPopup();
            }
            
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
            popupController.ClosePopup(); // 플레이어가 범위를 벗어나면 팝업창 비활성화
        }
    }
}
