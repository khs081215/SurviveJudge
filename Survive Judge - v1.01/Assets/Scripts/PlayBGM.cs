using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * BGM을 재생합니다.
 */
public class PlayBGM : MonoBehaviour
{
    [SerializeField]
    private string bgmName;
    [SerializeField]
    private float volume=0.5f;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlaySound(bgmName,gameObject,volume);
    }

    public void ChangeBGM(string changeBGMName, float changeVolume)
    {
        SoundManager.Instance.PlaySound(changeBGMName,gameObject,changeVolume);
    }
}
