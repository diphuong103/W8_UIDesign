using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Menu Canvas")]
    public GameObject CanvasObject;

    [Header("UI GameObjects to Hide/Show")]
    public GameObject TitleObject;
    public GameObject MessageObject;
    public GameObject EnterButtonObject;
    public GameObject RestartButtonObject;

    [Header("Text Components")]
    public TMP_Text TitleText;
    public TMP_Text MessageText;

    void Start()
    {
        ResolveMenuReferences();
        ConfigureMenuLayout();

        if (TitleText != null) TitleText.text = "Welcome to Unity!";
        if (MessageText != null) MessageText.text = "Do you want to continue?";

        ShowUIContent(false);
    }

    private void ResolveMenuReferences()
    {
        Transform[] descendants = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in descendants)
        {
            if (TitleObject == null && child.name == "Title")
                TitleObject = child.gameObject;
            if (MessageObject == null && child.name == "Message")
                MessageObject = child.gameObject;
            if (EnterButtonObject == null && child.name == "Enter")
                EnterButtonObject = child.gameObject;
            if (RestartButtonObject == null && child.name == "Restart")
                RestartButtonObject = child.gameObject;
        }

        if (TitleText == null && TitleObject != null)
            TitleText = TitleObject.GetComponent<TMP_Text>();
        if (MessageText == null && MessageObject != null)
            MessageText = MessageObject.GetComponent<TMP_Text>();

        if (CanvasObject == null)
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null)
                CanvasObject = canvas.gameObject;
        }
    }

    private void ConfigureMenuLayout()
    {
        RectTransform panel = TitleObject != null
            ? TitleObject.transform.parent as RectTransform
            : null;

        if (panel != null)
        {
            SetCenteredRect(panel, Vector2.zero, new Vector2(760f, 380f));

            Image panelImage = panel.GetComponent<Image>();
            if (panelImage != null)
                panelImage.color = new Color(0.055f, 0.11f, 0.2f, 0.94f);
        }

        ConfigureText(TitleObject, TitleText, new Vector2(0f, 105f), new Vector2(680f, 90f), 56f,
            new Color(0.96f, 0.98f, 1f, 1f));
        ConfigureText(MessageObject, MessageText, new Vector2(0f, 25f), new Vector2(640f, 60f), 30f,
            new Color(0.78f, 0.86f, 0.95f, 1f));

        ConfigureButton(EnterButtonObject, new Vector2(-125f, -105f),
            new Vector2(220f, 64f), new Color(0.12f, 0.58f, 0.82f, 1f));
        ConfigureButton(RestartButtonObject, new Vector2(125f, -105f),
            new Vector2(220f, 64f), new Color(0.2f, 0.28f, 0.4f, 1f));

        CanvasScaler scaler = CanvasObject != null
            ? CanvasObject.GetComponent<CanvasScaler>()
            : null;
        if (scaler != null)
        {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.matchWidthOrHeight = 0.5f;
        }
    }

    private static void ConfigureText(GameObject textObject, TMP_Text text, Vector2 position,
        Vector2 size, float fontSize, Color color)
    {
        if (textObject == null)
            return;

        RectTransform rect = textObject.GetComponent<RectTransform>();
        if (rect != null)
            SetCenteredRect(rect, position, size);

        if (text != null)
        {
            text.alignment = TextAlignmentOptions.Center;
            text.fontSize = fontSize;
            text.color = color;
        }
    }

    private static void ConfigureButton(GameObject buttonObject, Vector2 position, Vector2 size,
        Color color)
    {
        if (buttonObject == null)
            return;

        RectTransform rect = buttonObject.GetComponent<RectTransform>();
        if (rect != null)
            SetCenteredRect(rect, position, size);

        Image image = buttonObject.GetComponent<Image>();
        if (image != null)
            image.color = color;

        TMP_Text label = buttonObject.GetComponentInChildren<TMP_Text>(true);
        if (label != null)
        {
            label.alignment = TextAlignmentOptions.Center;
            label.fontSize = 26f;
            label.color = Color.white;
        }
    }

    private static void SetCenteredRect(RectTransform rect, Vector2 position, Vector2 size)
    {
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;
    }

    public void ShowUIContent(bool isShow)
    {
        if (CanvasObject != null) CanvasObject.SetActive(isShow);
        if (TitleObject != null) TitleObject.SetActive(isShow);
        if (MessageObject != null) MessageObject.SetActive(isShow);
        if (EnterButtonObject != null) EnterButtonObject.SetActive(isShow);
        if (RestartButtonObject != null) RestartButtonObject.SetActive(isShow);
    }

    public void NextScene()
    {
        if (SceneManager.sceneCountInBuildSettings < 2)
        {
            Debug.LogError("Cannot continue: add the next scene to Build Settings first.", this);
            return;
        }

        SceneManager.LoadScene(1);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}