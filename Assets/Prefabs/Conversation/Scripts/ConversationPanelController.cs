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

    // ��ȭâ�� �÷��̾��� ��ȭ ����, ���ڿ� ����(�ٹٲ�, ��ũ�ѹ� ��)�� �ڵ����� ����ǹǷ� ���� ó���� �ʿ� ����
    public void CreateText(string prefix, string s)
    {
        //if (InputField.text == "") return;
        CurrentTextBox = Instantiate(TextBox, Content.transform).gameObject.GetComponent<TMP_Text>();
        CurrentTextBox.text = prefix + ":\n" + InputField.text;
    }

    // Send ��ư�� ������ ��
    public void OnSendButtonClicked()
    {
        if (GetPlayerText() == "") return; //�Է�ĭ�� �ƹ��͵� ������ �������� �ʽ��ϴ�
        CreateText("You", GetPlayerText());
        SetPlayerText("");
    }

    // Rec ��ư�� ������ ��
    public void OnRecButtonClicked()
    {
        // TODO, ����ũ ���� �۾�
    }

    public void GetNPCText()
    {
        // TODO, �����κ��� NPC�� ������ �޴� �۾�
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
