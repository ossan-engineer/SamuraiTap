using UnityEngine;
using UnityEngine.UI; // UI要素用の名前空間を追加
using UnityEngine.SceneManagement; // SceneManager用の名前空間を追加
using System.Collections;
using System.IO;

public class MenuGUI : MonoBehaviour {
    public Image titleLogo;
    public Texture2D titleTexture;
    public Button startButton;
    
    private Canvas canvas;
    private GameObject titleLogoObj;
    private GameObject startButtonObj;

    void Awake() {
        Debug.Log("MenuGUI Awake: 初期化開始");
        
        canvas = GetComponent<Canvas>();
        if (canvas == null) {
            Debug.LogWarning("Canvasが見つかりません。親オブジェクトに追加します。");
            canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            gameObject.AddComponent<CanvasScaler>();
            gameObject.AddComponent<GraphicRaycaster>();
        }
        
        if (titleTexture == null) {
            Debug.LogWarning("titleTextureがnullです。Resources/Imagesから読み込みます。");
            titleTexture = Resources.Load<Texture2D>("Images/Title");
            
            if (titleTexture == null) {
                Debug.LogWarning("Resources/Images/Titleが見つかりません。");
                
                #if UNITY_EDITOR
                try {
                    string[] titleFiles = Directory.GetFiles(Application.dataPath + "/Images", "Title.png", SearchOption.AllDirectories);
                    if (titleFiles.Length > 0) {
                        string path = titleFiles[0].Replace(Application.dataPath, "Assets");
                        titleTexture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                        Debug.Log("タイトルテクスチャを読み込みました: " + path);
                    }
                } catch (System.Exception e) {
                    Debug.LogError("タイトルテクスチャの読み込み中にエラーが発生しました: " + e.Message);
                }
                #endif
            }
        }
    }

    void Start() {
        Debug.Log("MenuGUI Start: 初期化開始");
        
        if (titleLogo == null) {
            Debug.LogWarning("titleLogoがnullです。自動的に作成します。");
            
            titleLogoObj = new GameObject("TitleLogo");
            titleLogoObj.transform.SetParent(transform);
            titleLogo = titleLogoObj.AddComponent<Image>();
            
            RectTransform logoRect = titleLogo.GetComponent<RectTransform>();
            logoRect.anchorMin = new Vector2(0.5f, 0.7f);
            logoRect.anchorMax = new Vector2(0.5f, 0.9f);
            logoRect.pivot = new Vector2(0.5f, 0.5f);
            logoRect.anchoredPosition = Vector2.zero;
            logoRect.sizeDelta = new Vector2(500, 200);
        }
        
        Debug.Log("titleLogo: " + titleLogo.name);
        
        titleLogo.color = Color.white;
        titleLogo.raycastTarget = false;
        
        if (titleTexture != null) {
            Debug.Log("titleTexture: " + titleTexture.name + " サイズ: " + titleTexture.width + "x" + titleTexture.height);
            try {
                Sprite titleSprite = Sprite.Create(titleTexture, new Rect(0, 0, titleTexture.width, titleTexture.height), new Vector2(0.5f, 0.5f));
                titleLogo.sprite = titleSprite;
                titleLogo.preserveAspect = true;
                Debug.Log("タイトルロゴの設定完了");
            } catch (System.Exception e) {
                Debug.LogError("タイトルロゴの設定中にエラー: " + e.Message);
                titleLogo.color = new Color(0.8f, 0.8f, 0.8f, 1f);
            }
        } else {
            Debug.LogError("titleTextureがnullです。単色で表示します。");
            titleLogo.color = new Color(0.8f, 0.8f, 0.8f, 1f);
        }
        
        if (startButton == null) {
            Debug.LogWarning("startButtonがnullです。自動的に作成します。");
            
            startButtonObj = new GameObject("StartButton");
            startButtonObj.transform.SetParent(transform);
            startButton = startButtonObj.AddComponent<Button>();
            
            Image buttonImage = startButtonObj.AddComponent<Image>();
            buttonImage.color = new Color(0.2f, 0.2f, 0.8f, 0.8f);
            
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(startButtonObj.transform);
            Text buttonText = textObj.AddComponent<Text>();
            buttonText.text = "TAP TO START";
            buttonText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            buttonText.fontSize = 24;
            buttonText.alignment = TextAnchor.MiddleCenter;
            buttonText.color = Color.white;
            
            RectTransform textRect = buttonText.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;
            
            RectTransform buttonRect = startButton.GetComponent<RectTransform>();
            buttonRect.anchorMin = new Vector2(0.5f, 0.3f);
            buttonRect.anchorMax = new Vector2(0.5f, 0.4f);
            buttonRect.pivot = new Vector2(0.5f, 0.5f);
            buttonRect.anchoredPosition = Vector2.zero;
            buttonRect.sizeDelta = new Vector2(300, 60);
        }
        
        Debug.Log("startButton: " + startButton.name);
        
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() => {
            Debug.Log("スタートボタンがクリックされました");
            SceneManager.LoadScene("Stage 1-1");
        });
        
        Debug.Log("MenuGUI Start: 初期化完了");
    }
    
    void Update() {
        try {
            bool touchDetected = false;
            try {
                touchDetected = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
            } catch (System.Exception e) {
                Debug.LogWarning("タッチ入力の検出中にエラーが発生しました: " + e.Message);
                touchDetected = false;
            }
            
            if (Input.GetMouseButtonDown(0) || touchDetected) {
                Debug.Log("画面がタップされました");
                try {
                    SceneManager.LoadScene("Stage 1-1");
                } catch (System.Exception e) {
                    Debug.LogError("シーン読み込み中にエラーが発生しました: " + e.Message);
                }
            }
        } catch (System.Exception e) {
            Debug.LogError("Update中にエラーが発生しました: " + e.Message + "\n" + e.StackTrace);
        }
    }
}
