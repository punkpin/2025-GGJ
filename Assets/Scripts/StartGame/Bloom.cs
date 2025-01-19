using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Blooms : MonoBehaviour
{
    public VolumeProfile globalVolume; // Global Volume����
    private Bloom bloom; // Bloom����
    private Light2D light2D;

    public GameObject Lighteffet; // 发放的道具Prefab
    public Color[] colors;
    private int currentIndex = 0;


    public Color startTint; // ��ʼTint��ɫ
    public Color endTint; // ����Tint��ɫ
    public float transitionDuration = 5f; // �仯����ʱ�䣨�룩
    
    private float transitionTimer = 0f; // ���ɼ�ʱ��

    void Start()
    {

 light2D = Lighteffet.GetComponent<Light2D>();

        //var volume = GetComponent<Volume>();
        //globalVolume = volume.profile;
        ChangBloom();
        // var globalVolume = GetComponent<Volume>();
        // globalVolume.TryGet(typeof(Bloom), out Bloom bloom);

        // globalVolume.TryGet(typeof(Bloom),out ColorLookup lutWeight);
        //if (globalVolume != null && globalVolume.TryGet(out VolumeComponent bloomComponent))
        //  {
        //globalVolume.TryGet(out VolumeComponent bloomComponent);
            //bloom = bloomComponent;
       // }
       // else
        //{
          //  Debug.LogWarning("Bloom component not found in the assigned global volume profile.");
        //}

        if (bloom != null)
        {
            //bloom.isActiveAndEnabled = true; // ȷ��BloomЧ�����ڼ���״̬
            bloom.tint.value = startTint; // ����Bloom����ʼTint��ɫ
        }
    }

    void Update()
    {
        globalVolume.TryGet(typeof(Bloom), out Bloom bloom);
        if (bloom != null)
        {
            //if (transitionTimer < transitionDuration)
           // {

                float cycleProgress = (Time.time - transitionTimer) / transitionDuration;
              //  transitionTimer += Time.deltaTime;
               // float t = Mathf.Clamp01(transitionTimer / transitionDuration);
               // Color lerpedColor = Color.Lerp(startTint, endTint, t);

                Color lerpedColor = Color.Lerp(colors[currentIndex], colors[(currentIndex + 1) % colors.Length], cycleProgress);
                // ����ֵ�����ɫӦ�õ�����
                lerpedColor.a=255f;
                light2D.color = lerpedColor;


                bloom.tint.value = lerpedColor;
            //   }

            if (cycleProgress >= 1f)
            {
                currentIndex = (currentIndex + 1) % colors.Length;
                transitionTimer = Time.time;
            }
           // if (transitionTimer > transitionDuration)
           // {
           //     transitionTimer = 0f;
           // }
        }
    }

    private ColorParameter Lerp(Color startTint, Color endTint, float t)
    {
        throw new NotImplementedException();
    }

    private void ChangBloom()
    {

        //if (e != null)
        //{
            globalVolume.TryGet(typeof(Bloom), out Bloom bloom);
        if (bloom != null) 
        { 
            bloom.tint.value = startTint; 
        }
          
          //          }


    }


    public static implicit operator Blooms(VolumeParameter v)
    {
        throw new NotImplementedException();
    }

    public static implicit operator Blooms(VolumeComponent v)
    {
        throw new NotImplementedException();
    }
}