using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel; // 팝업창으로 사용할 패널 오브젝트
    public FirstPersonController playerController; // 플레이어 컨트롤러 스크립트 참조
    public GameObject Player;
    public GameObject NPC;

    // 상호작용시에 플레이어가 보이지 않도록 설정
    private void HidePlayer(bool b)
    {
        b = !b;
        foreach (Transform child in Player.transform)
        {
            child.gameObject.SetActive(b);
        }
    }

    // 팝업창을 닫는 메서드
    public void ClosePopup()
    {
        popupPanel.SetActive(false); // 팝업창을 비활성화
        playerController.isPopupOpen = false; // 플레이어 이동 제한 해제
        NPC.transform.Find("CamNPC").gameObject.SetActive(false);// NPC시점카메라 비활성화
        HidePlayer(false); //플레이어 숨김 해제
    }

    // 팝업창을 열기 위한 메서드
    public void OpenPopup()
    {
        popupPanel.SetActive(true); // 팝업창을 활성화
        playerController.isPopupOpen = true; // 플레이어 이동 제한
        NPC.transform.Find("CamNPC").gameObject.SetActive(true);// NPC시점카메라 활성화
        HidePlayer(true); //플레이어 숨김
    }
}
