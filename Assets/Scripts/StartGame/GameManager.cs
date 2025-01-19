using UnityEngine;
using UnityEngine.SceneManagement;  // 用于加载场景
using UnityEngine.UI;


public  class GameManager: MonoBehaviour
{

  public float bgmAudioSourceValue;
  public float sfxAudioSourceValue;


[SerializeField] GameObject buttonUI;




public Slider Slider001;          
public Slider Slider002;
     
    void Start()
    {
   
        ShotDownSetting();
        Slider001.onValueChanged.AddListener(OnSlider001ValueChanged);
         Slider002.onValueChanged.AddListener(OnSlider002ValueChanged);
       
    }



    
    void OnSlider001ValueChanged(float value)
    {
     
        float Values = Mathf.Lerp(0f, 1f, value);
           bgmAudioSourceValue=Values;
        
    }
    void OnSlider002ValueChanged(float value)
    {
     
        float Values = Mathf.Lerp(0f, 1f, value);
               sfxAudioSourceValue=Values;
        
    }






public   void ShowSetting()
{
      buttonUI.SetActive(true);
}

public   void ShotDownSetting()
{
     buttonUI.SetActive(false);
}


    public static void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void ExitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}



