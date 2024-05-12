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

    public void CreatePlayerText()
    {
        if (InputField.text == "") return;
        CurrentTextBox = Instantiate(TextBox, Content.transform).gameObject.GetComponent<TMP_Text>();
        CurrentTextBox.text = "You:\n" + InputField.text;
        InputField.text = "";
    }
    public void CreateNPCText(string s)
    {
        CurrentTextBox = Instantiate(TextBox, Content.transform).gameObject.GetComponent<TMP_Text>();
        CurrentTextBox.text = "NPC:\n" + s;
    }

    public void SendPlayerText()
    {
        // TODO
    }

    public void GetNPCText()
    {
        // TODO
    }

}
