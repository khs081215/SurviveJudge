using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * 특정 이름을 가진 사운드를 재생합니다.
 *
 * SoundManager.Instance.PlaySound(string SoundName, GameObject calledGameObject, float volume)의 형태로 호출할 수 있습니다.
 * 예를 들어, SoundManager.Instance.PlaySound("Thirdscene,gameObject,0.1f) 로 사용 가능합니다.
 * 세번째 파라미터 volume은 입력하지 않을 수 있으며, 그 경우 기본적으로 0.5f가 적용됩니다.
 */
public class SoundManager : GenericSingleton<SoundManager>
{
    private Dictionary<string, AudioClip> audioMapping;

    private enum AudioAssetName
    {
        Title,
        Firstscene,
        Secondscene,
        Thirdscene,
        Fire,
        DDring,
        Break,
        Complete
    };

    protected override void InitReference()
    {
        audioMapping = new Dictionary<string, AudioClip>();
        foreach (string audioName in Enum.GetNames(typeof(AudioAssetName)))
        {
            audioMapping.Add(audioName, (Resources.Load<AudioClip>("SoundAssets/" + audioName)));
            UnityEngine.Debug.Log(audioMapping.Count);
        }
    }

    public void PlaySound(string soundName, GameObject gameobject, float volume = 0.5f)
    {
        if (audioMapping.TryGetValue(soundName, out var clip))
        {
            AudioSource source = gameobject.GetComponent<AudioSource>();
            if (source == null)
            {
                source = gameobject.AddComponent<AudioSource>();
            }

            source.clip = clip;
            source.loop = false;
            source.volume = volume;
            source.Play();
        }
        else
        {
            Debug.Log("SoundError");
        }
    }
}