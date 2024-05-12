using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel; // 팝업창으로 사용할 패널 오브젝트
    public FirstPersonController playerController; // 플레이어 컨트롤러 스크립트 참조

    // 팝업창을 닫는 메서드
    public void ClosePopup()
    {
        popupPanel.SetActive(false); // 팝업창을 비활성화
        playerController.isPopupOpen = false; // 플레이어 이동 제한 해제
    }

    // 팝업창을 열기 위한 메서드
    public void OpenPopup()
    {
        popupPanel.SetActive(true); // 팝업창을 활성화
        playerController.isPopupOpen = true; // 플레이어 이동 제한
    }
}
