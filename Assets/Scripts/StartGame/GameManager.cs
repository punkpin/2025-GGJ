using UnityEngine;
using UnityEngine.SceneManagement;  // 用于加载场景
using UnityEngine.UI;


public class GameManager: MonoBehaviour
{
public AudioClip[] BGMClips;  // 管理背景音乐
  public float bgmAudioSourceValue;
  public float sfxAudioSourceValue;

    private AudioSource bgmAudioSource;


    

    [SerializeField]
    GameObject buttonUI;

    public Slider Slider001;          
    public Slider Slider002;

    public string sceneName;
     
    void Start()
    {
        ShotDownSetting();
        Slider001.onValueChanged.AddListener(OnSlider001ValueChanged);
        Slider002.onValueChanged.AddListener(OnSlider002ValueChanged);   
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.clip = BGMClips[0];  
            bgmAudioSource.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bgmAudioSource.Stop();
          
            StartGame(sceneName); // Replace "YourSceneName" with the actual scene name you want to load
        }
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

    public void ShowSetting()
    {
        buttonUI.SetActive(true);
    }

    public void ShotDownSetting()
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



