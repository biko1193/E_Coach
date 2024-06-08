using UnityEngine;

public class MiniMapToggle : MonoBehaviour
{
    public GameObject miniMapCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            miniMapCanvas.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            miniMapCanvas.SetActive(false);
        }
    }
}

