using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxController : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
