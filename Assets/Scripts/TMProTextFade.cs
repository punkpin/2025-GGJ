using UnityEngine;
using TMPro;

public class TMProTextFade : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float fadeDuration = 1.0f;
    private float timer = 0.0f;
    private bool fadingOut = true;

    void Update()
    {
        if (textMeshPro == null) return;

        timer += Time.deltaTime;
        float alpha = Mathf.PingPong(timer / fadeDuration, 0.8f) + 0.2f;

        Color color = textMeshPro.color;
        color.a = alpha;
        textMeshPro.color = color;
    }
}