using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneManager : MonoBehaviour
{
    private static MicrophoneManager instance = null;
    // Start is called before the first frame update
    void Awake()
    {
        if (null == instance)
        {
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;

            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
            //gameObject�����ε� �� ��ũ��Ʈ�� ������Ʈ�μ� �پ��ִ� Hierarchy���� ���ӿ�����Ʈ��� ��������, 
            //���� �򰥸� ������ ���� this�� �ٿ��ֱ⵵ �Ѵ�.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� GameMgr�� ������ ���� �ִ�.
            //�׷� ��쿣 ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ���� �� ����.
            //�׷��� �̹� ���������� instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� GameMgr)�� �������ش�.
            Destroy(this.gameObject);
        }
    }

    public static MicrophoneManager Instance // �̱��� ���� ����
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Start()
    {
        //��ǻ�Ϳ� ��ϵ� ����ũ�� �迭�� ���
        string[] myMic = Microphone.devices;
        for (int i = 0; i < myMic.Length; i++)
        {
            Debug.Log(Microphone.devices[i].ToString());
        }
    }

    public void StartRecording()
    {
        AudioSource rec = GetComponent<AudioSource>();
        rec.clip = Microphone.Start(Microphone.devices[0].ToString(),
            true, 300, 44100);
        Debug.Log(Microphone.devices[0].ToString() + "Start recording");
    }

    public void StopRecording()
    {
        Microphone.End(Microphone.devices[0].ToString());
        Debug.Log(Microphone.devices[0].ToString() + "Stop recording");
    }

    public AudioSource GetRecord()
    {
        AudioSource rec = GetComponent<AudioSource>();
        return rec;
    }

    public void PlayRecord()
    {
        AudioSource rec = GetComponent<AudioSource>();
        rec.Play();
    }
}
