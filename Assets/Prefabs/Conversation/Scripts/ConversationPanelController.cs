using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversationPanelController : MonoBehaviour
{
    // Preload prefabs here
    public GameObject TextBox;
    public GameObject Content;
    public TMP_InputField InputField;
    private TMP_Text CurrentTextBox;

    // 대화창에 플레이어의 대화 생성, 문자열 정렬(줄바꿈, 스크롤바 등)은 자동으로 수행되므로 따로 처리할 필요 없음
    public void CreatePlayerText(string s)
    {
        //if (InputField.text == "") return;
        CurrentTextBox = Instantiate(TextBox, Content.transform).gameObject.GetComponent<TMP_Text>();
        CurrentTextBox.text = "You:\n" + InputField.text;
    }

    // 대화창에 NPC의 대화 생성
    public void CreateNPCText(string s)
    {
        CurrentTextBox = Instantiate(TextBox, Content.transform).gameObject.GetComponent<TMP_Text>();
        CurrentTextBox.text = "NPC:\n" + s;
    }

    // Send 버튼을 눌렀을 때
    public void OnSendButtonClicked()
    {
        if (GetPlayerText() == "") return; //입력칸에 아무것도 없으면 수행하지 않습니다
        CreatePlayerText(GetPlayerText());
        SetPlayerText("");
    }

    // Rec 버튼을 눌렀을 때
    public void OnRecButtonClicked()
    {
        // TODO, 마이크 관련 작업
    }

    public void GetNPCText()
    {
        // TODO, 서버로부터 NPC의 응답을 받는 작업
    }

    public string GetPlayerText()
    {
        return InputField.text;
    }

    public void SetPlayerText(string s)
    {
        InputField.text = s;
    }
}
