using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerformanceUI : MonoBehaviour
{
    [SerializeField] private CrowdManager crowdManager;
    [SerializeField] private Button toggleButton;
    [SerializeField] private TMP_Text modeText;
    [SerializeField] private TMP_Text statsText;

    private float fpsUpdateTimer;
    private float currentFps;

    private void Awake()
    {
        toggleButton.onClick.AddListener(OnToggleClicked);
    }

    private void Update()
    {
        UpdateFps();
        UpdateTexts();
    }

    private void OnToggleClicked()
    {
        crowdManager.ToggleMode();
    }

    private void UpdateFps()
    {
        fpsUpdateTimer += Time.deltaTime;
        if (fpsUpdateTimer >= 0.2f)
        {
            currentFps = 1f / Mathf.Max(Time.unscaledDeltaTime, 0.0001f);
            fpsUpdateTimer = 0f;
        }
    }

    private void UpdateTexts()
    {
        modeText.text = $"Mode  {crowdManager.CurrentMode}";
        statsText.text = $"FPS  {currentFps:F0}\nAgents  {crowdManager.ActiveAgentCount}";
    }
}
