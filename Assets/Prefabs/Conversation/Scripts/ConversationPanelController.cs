using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using System;

public class ConversationPanelController : MonoBehaviour
{
    // Preload prefabs here
    public GameObject TextBox;
    public GameObject Content;
    public GameObject RedKnob;
    public GameObject BlackBox;
    public GameObject Log;
    public TMP_Text TimerText;
    public TMP_InputField InputField;
    public Toggle LogToggle;
    private GameObject CurrentTextBox;
    private int timerTime;

    private bool isRecording = false;

    // ��ȭâ�� �÷��̾��� ��ȭ ����, ���ڿ� ����(�ٹٲ�, ��ũ�ѹ� ��)�� �ڵ����� ����ǹǷ� ���� ó���� �ʿ� ����
    public void CreateText(string s)
    {
        //if (InputField.text == "") return;
        CurrentTextBox = Instantiate(TextBox, Content.transform);
        CurrentTextBox.GetComponentInChildren<TMP_Text>().text =
            '['+DateTime.Now.ToString(("HH:mm:ss")) +"] "+s;
    }

    // Send ��ư�� ������ ��
    public void OnSendButtonClicked()
    {
        if (isRecording)
        {
            ChangeRecStatus();
            SetTimer();
        }

        MicrophoneManager.Instance.PlayRecord();
    }

    private string TimeToString(int time)
    {
        string s;
        string minute = (time / 60).ToString();
        string second = (time % 60).ToString();
        if (minute.Length < 2) minute = "0" + minute;
        if (second.Length < 2) second = "0" + second;
        s = minute + ':' + second;
        return s;
    }

    IEnumerator Timer()
    {
        while (true)
        {
        yield return new WaitForSeconds(1f);
            timerTime += 1;
            TimerText.text = TimeToString(timerTime);
            //Debug.Log(timerTime);
        }
    }

    private void SetTimer()
    {
        if (isRecording)
        {
            timerTime = 0;
            TimerText.text = "00:00";
            StartCoroutine("Timer");
            Debug.Log("Timer started");
        }
        else
        {
            try
            {
                StopCoroutine("Timer");
                Debug.Log("Timer suspended");
            }
            catch
            {
                Debug.Log("Timer not activated");
            }
        }
    }

    private void ChangeRecStatus()
    {
        if (isRecording) // ���� �����
        {
            BlackBox.SetActive(false);
            RedKnob.SetActive(true);
            MicrophoneManager.Instance.StopRecording();
            CreateText("Record " + TimeToString(timerTime));
            isRecording = !isRecording;
        }
        else // ���� ���۽�
        {
            BlackBox.SetActive(true);
            RedKnob.SetActive(false);
            MicrophoneManager.Instance.StartRecording();
            isRecording = !isRecording;
        }
    }

    // Rec ��ư�� ������ ��
    public void OnRecButtonClicked()
    {
        // TODO, ����ũ ���� �۾�
        ChangeRecStatus();
        SetTimer();
    }

    // Log��� ��ư�� ������ ��
    public void OnLogToggleChanged()
    {
        if (LogToggle.isOn)
        {
            Log.SetActive(true);
        }
        else
        {
            Log.SetActive(false);
        }
    }

    public void GetNPCText()
    {
        // TODO, �����κ��� NPC�� ������ �޴� �۾�
    }


}
