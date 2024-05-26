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

    // 대화창에 플레이어의 대화 생성, 문자열 정렬(줄바꿈, 스크롤바 등)은 자동으로 수행되므로 따로 처리할 필요 없음
    public void CreateText(string s)
    {
        //if (InputField.text == "") return;
        CurrentTextBox = Instantiate(TextBox, Content.transform);
        CurrentTextBox.GetComponentInChildren<TMP_Text>().text =
            '['+DateTime.Now.ToString(("HH:mm:ss")) +"] "+s;
    }

    // Send 버튼을 눌렀을 때
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
        if (isRecording) // 녹음 종료시
        {
            BlackBox.SetActive(false);
            RedKnob.SetActive(true);
            MicrophoneManager.Instance.StopRecording();
            CreateText("Record " + TimeToString(timerTime));
            isRecording = !isRecording;
        }
        else // 녹음 시작시
        {
            BlackBox.SetActive(true);
            RedKnob.SetActive(false);
            MicrophoneManager.Instance.StartRecording();
            isRecording = !isRecording;
        }
    }

    // Rec 버튼을 눌렀을 때
    public void OnRecButtonClicked()
    {
        // TODO, 마이크 관련 작업
        ChangeRecStatus();
        SetTimer();
    }

    // Log토글 버튼을 눌렀을 때
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
        // TODO, 서버로부터 NPC의 응답을 받는 작업
    }


}
