using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using System;

public class LogTextBox
{
    public string Type
    {
        get; private set;
    }
    private string text;
    private AudioSource aud = null;
    private GameObject textBox = null;

    public LogTextBox(GameObject Txtbx, string typ, string txt, AudioSource audsrc = null)
    {
        Type = typ;
        text = txt;
        textBox = Txtbx;
        aud = textBox.GetComponent<AudioSource>();
        if (audsrc != null)
        {
            aud.clip = audsrc.clip;
            aud.clip.name = DateTime.Now.ToString(("HH:mm:ss"));
            textBox.transform.Find("Button").gameObject.SetActive(true);
        }
    }

    public void Destroy()
    {
        UnityEngine.Object.Destroy(textBox);
        if(aud!=null)UnityEngine.Object.Destroy(aud);
    }

    public void Hide()
    {
        if (textBox != null) textBox.SetActive(false);
    }

    public void Show()
    {
        if (textBox != null) textBox.SetActive(true);
    }
}

public class RingBuffer<T>
{
    public readonly int Size;

    public Action<T> OnOverflow;

    public int Count
    {
        get;
        private set;
    }

    public int TotalCount
    {
        get;
        private set;
    }

    private T[] buffer;
    private int position;

    public RingBuffer(int size)
    {
        this.Size = size;
        this.buffer = new T[size];
        this.Count = 0;
        this.position = 0;
    }

    public void Push(T item)
    {
        this.position = (this.position + 1) % this.Size;
        if (this.buffer[this.position] != null && this.OnOverflow != null)
        {
            this.OnOverflow(this.buffer[this.position]);
        }
        this.buffer[this.position] = item;
        this.Count++;
        if (this.Count > this.Size)
        {
            this.Count = this.Size;
        }
        this.TotalCount++;
    }

    public T Peek(int idx = 0)
    {
        if (this.Count == 0)
        {
            throw new System.InvalidOperationException();
        }
        if (idx > this.Count)
        {
            throw new System.InvalidOperationException();
        }
        int pos = this.position - idx;
        if (pos < 0) pos += this.Size;
        return this.buffer[pos];
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new System.InvalidOperationException();
        }
        int last = (this.position - (this.Count - 1));
        if(last < 0) last += (this.Size);
        T result = this.buffer[last];
        this.buffer[last] = default(T);

        //this.position = (this.position + this.Size - 1) % this.Size;
        this.Count--;
        this.TotalCount--;

        return result;
    }

    public bool Any()
    {
        return this.Count != 0;
    }

    public T[] All()
    {
        return buffer;
    }

}

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
    public Toggle UserToggle;
    public Toggle ServerToggle;
    private GameObject CurrentTextBox;
    private int timerTime;
    public static int LogMax = 30;

    private bool isRecording = false;
    private RingBuffer<LogTextBox> logbuf = new RingBuffer<LogTextBox>(LogMax);



    // 대화창에 플레이어의 대화 생성, 문자열 정렬(줄바꿈, 스크롤바 등)은 자동으로 수행되므로 따로 처리할 필요 없음
    public void CreateText(string type, string s, AudioSource aud = null)
    {
        //if (InputField.text == "") return;
        CurrentTextBox = Instantiate(TextBox, Content.transform);
        CurrentTextBox.GetComponentInChildren<TMP_Text>().text =
            '['+DateTime.Now.ToString(("HH:mm:ss")) +"] " +'\n'
            //+type+'\n'
            +s;
        LogTextBox log = new LogTextBox(CurrentTextBox, type, s, aud);
        logbuf.Push(log);
        if(logbuf.Count == LogMax)
        {
            logbuf.Pop().Destroy();
        }
    }

    // Send 버튼을 눌렀을 때
    public void OnSendButtonClicked()
    {
        if (isRecording)
        {
            ChangeRecStatus();
            SetTimer();
        }
    }

    public void OnPlayButtonClicked()
    {
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
            CreateText(
                "user", 
                "User record (" + TimeToString(timerTime)+')',
                MicrophoneManager.Instance.GetComponent<AudioSource>()
                );
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

    private void Hide(LogTextBox tb)
    {
        tb.Hide();
    }

    private void Show(LogTextBox tb)
    {
        tb.Show();
    }

    public void OnUserToggleChanged()
    {
        Debug.Log("User Toggle: " + UserToggle.isOn.ToString());
        LogTextBox[] buf = logbuf.All();
        if (UserToggle.isOn)
        {
            for(int i = 0; i < logbuf.Count; i++)
            {
                LogTextBox tmp = logbuf.Peek(i);
                if (tmp.Type == "user") tmp.Show();
            }
        }
        else
        {
            for (int i = 0; i < logbuf.Count; i++)
            {
                LogTextBox tmp = logbuf.Peek(i);
                if (tmp.Type == "user") tmp.Hide();
            }
        }
    }

    public void OnServerToggleChanged()
    {
        Debug.Log("Server Toggle: " + ServerToggle.isOn.ToString());
        if (ServerToggle.isOn)
        {
            for (int i = 0; i < logbuf.Count; i++)
            {
                LogTextBox tmp = logbuf.Peek(i);
                if (tmp.Type == "server") tmp.Show();
            }
        }
        else
        {
            for (int i = 0; i < logbuf.Count; i++)
            {
                LogTextBox tmp = logbuf.Peek(i);
                if (tmp.Type == "server") tmp.Hide();
            }
        }
    }

    public void GetNPCText()
    {
        // TODO, 서버로부터 NPC의 응답을 받는 작업
    }


}
