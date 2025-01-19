using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
// BGM和SFX的Clip数组
    public AudioClip[] BGMClips;  // 管理背景音乐
    public AudioClip[] SFXClips;  // 管理音效
      public  GameObject bgmAudioSourceOBJ;

         public  GameObject sfxAudioSourceOBJ;
    // 音频播放器
    private AudioSource bgmAudioSource;
    private AudioSource sfxAudioSource;

    public GameManager  GameManagerStart;

  public float bgmVolume = 0.8f;  // 背景音乐音量
 public float sfxVolume = 0.8f;  // 音效音量

    // 当前的背景音乐索引
    private int currentBGMIndex = 0;

    void Start()
    {
        // 初始化音频播放器
        bgmAudioSource = bgmAudioSourceOBJ.AddComponent<AudioSource>();
        sfxAudioSource = sfxAudioSourceOBJ.AddComponent<AudioSource>();

        // 设置音量
        bgmAudioSource.volume = bgmVolume;
        sfxAudioSource.volume = sfxVolume;


          GameManagerStart = gameObject.GetComponent<GameManager>();

        // 播放开始BG（假设是第一个BGM）
        PlayBGM(currentBGMIndex);
    }

    void Update()
    {
        bgmVolume=   GameManagerStart.bgmAudioSourceValue;
         sfxVolume=   GameManagerStart.sfxAudioSourceValue;
        
        // 可以根据需要实现动态音量调节
        bgmAudioSource.volume = bgmVolume;
        sfxAudioSource.volume = sfxVolume;
    }

    // 播放指定的BGM
    public void PlayBGM(int index)
    {
        if (BGMClips != null && BGMClips.Length > 0 && index >= 0 && index < BGMClips.Length)
        {
            bgmAudioSource.clip = BGMClips[index];
            bgmAudioSource.Play();
        }
    }

    // 根据当前的BGM索引切换BGM
    public void SwitchBGM()
    {
        currentBGMIndex = (currentBGMIndex + 1) % BGMClips.Length; // 循环切换BGM
        PlayBGM(currentBGMIndex);
    }

    // 播放音效（例如点击按钮时）
    public void PlaySFX(int clipIndex)
    {
        if (SFXClips != null && SFXClips.Length > 0 && clipIndex >= 0 && clipIndex < SFXClips.Length)
        {
            sfxAudioSource.PlayOneShot(SFXClips[clipIndex]);
        }
    }

    
    public void PlayCollisionSFX()
    {
        PlaySFX(1); // 播放音效数组中的碰撞音效
    }

    // 触发事件来切换BGM（此函数示例）
    private void ChangeBGM(int Range)
    {



        //PlayCollisionSFX();  // 当触发碰撞时播放音效
        SwitchBGM();          // 切换BGM
    }

    // UI按钮点击时触发播放音效
    public void OnButtonClick()
    {
        PlaySFX(0); // 播放第一个音效
    }




}
