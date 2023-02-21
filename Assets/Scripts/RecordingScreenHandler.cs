using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordingScreenHandler : MonoBehaviour
{
    #region Singleton
    public static RecordingScreenHandler Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion 
    [SerializeField] private Image recImage;
    [SerializeField] private Sprite recSprite;
    [SerializeField] private Sprite nonRecSprite;
    public void StartRecording()
    {
        recImage.gameObject.SetActive(true);
        StartCoroutine(Recording());
    }

    public void StopRecording()
    {
        StopCoroutine(Recording());
        recImage.gameObject.SetActive(false);
    }

    private IEnumerator Recording()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            recImage.sprite = (recImage.sprite == recSprite) ? nonRecSprite : recSprite;
        }
    }
}
