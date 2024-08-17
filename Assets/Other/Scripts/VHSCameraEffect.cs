using UnityEngine;

[RequireComponent(typeof(Camera))]
public class VHSCameraEffect : MonoBehaviour
{
    public Shader shader;  // odnośnik do zasobu w Unity

    private Material material;  // materiał, który powstanie z shadera (jest nakladny pozniej na obiekt Kamery w Unity) 

    // parametry z zakresami
    [Header("Shader Properties")]
    // właściwości dla shadera CRT
    [Range(0.0f, 30.0f)]
    public float changeInterval = 3f; // odstęp między aktywacjami
    [Range(0.0f, 10.0f)]
    public float animationDuration = 1.24f; // czas trwania

    [Range(0f, 1f)]
    public float scanlineIntensity = 0.034f;

    [Range(0f, 1f)]
    public float scanlineThickness = 0.129f;

    [Range(0f, 10f)]
    public float scanlineSpeed = 1f;

    // przesuniecie wertykalne całego obrazu
    [Range(0f, 1f)]
    public float verticalOffset = 0.1f;


    // parametry dla zniekształcenia
    [Range(0f, 1f)]
    public float distortionStrength = 0.026f;

    [Range(0f, 1f)]
    public float distortionOffset = 0f;


    // parametry dla glitcha (falowanie pionowe)
    [Range(0f, 1f)]
    public float glitchIntensity = 0.004f;

    // parametry dla linii pionowych na górze kamery
    [Range(0f, 1f)]
    public float staticLinesIntensity = 0.034f;

    [Range(0f, 0.1f)]
    public float staticLinesSize = 0.129f;

    // rozdzielenie wartości RGB próbek
    [Range(0f, 1f)]
    public float colorShift = 0.004f;

    // przesunięcie obrazu w poziomie
    [Range(0f, 1f)]
    public float tapeJitter = 0.02f;

    [Range(0f, 1f)]
    public float jitterEnabled = 1f;

    // parametry dla falowania poziomego
    [Range(0.0f, 20.0f)]
    public float waveFrequency = 0.17f;
    [Range(0.0f, 0.2f)]
    public float waveAmplitude = 0.0343f;
    [Range(0.0f, 5.0f)]
    public float waveSpeed = 1f;

    private float timer; // licznika do śledzenia odstępów shadera CRT
    private bool isAnimating;

    void OnEnable()
    {
        if (shader == null) //w przypadku braku dodania Shadera do Unity na komponencie skryptu
        {
            Debug.LogError("Brak odniesienia na " + gameObject.name);
            enabled = false;
            return;
        }

        material = new Material(shader);
        timer = changeInterval; // start licznika do shadera CRT
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (!isAnimating && timer <= 0f) //warunek dla wlączenia się shadera  CRT
        {
            isAnimating = true;
            timer = animationDuration; // reset licznika na czas trwania shadera CRT
        }
        else if (isAnimating && timer <= 0f) //warunek dal wyłączenia się shadera CRT
        {
            isAnimating = false;
            timer = changeInterval; // reset licznika na odstęp między włączeniem się shadera CRT
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material != null)
        {
            // ustawianie właściwości shadera na podstawie ustawionych parametrów
            // material.SetFloat("_ScanlineIntensity", scanlineIntensity);
            // material.SetFloat("_ScanlineThickness", scanlineThickness);
            // material.SetFloat("_ScanlineSpeed", scanlineSpeed);
            material.SetFloat("_VerticalOffset", verticalOffset);
            material.SetFloat("_DistortionStrength", distortionStrength);
            material.SetFloat("_DistortionOffset", distortionOffset);
            material.SetFloat("_GlitchIntensity", glitchIntensity);
            material.SetFloat("_StaticLinesIntensity", staticLinesIntensity);
            material.SetFloat("_StaticLinesSize", staticLinesSize);
            material.SetFloat("_ColorShift", colorShift);
            material.SetFloat("_TapeJitter", tapeJitter);
            material.SetFloat("_JitterEnabled", jitterEnabled);
            material.SetFloat("_WaveFrequency", waveFrequency);
            material.SetFloat("_WaveAmplitude", waveAmplitude);
            material.SetFloat("_WaveSpeed", waveSpeed);

            //shader CRT
            if(isAnimating){
                material.SetFloat("_ScanlineIntensity", scanlineIntensity);
                material.SetFloat("_ScanlineThickness", scanlineThickness);
                material.SetFloat("_ScanlineSpeed", scanlineSpeed);     
            }
            else{
                material.SetFloat("_ScanlineIntensity", 0);
                material.SetFloat("_ScanlineThickness", 0);
                material.SetFloat("_ScanlineSpeed", 0);    
            }

            // kopiowanie tekstury źródłowej do tekstury docelowej z zastosowaniem shadera
            Graphics.Blit(source, destination, material);

        }
        else             // jeśli materiał nie istnieje
        {

            Graphics.Blit(source, destination);
        }
    }

    void OnDisable()
    {
        if (material != null)
        {
            DestroyImmediate(material); // czyszczenie materiału po wyłączeniu
        }
    }
}
