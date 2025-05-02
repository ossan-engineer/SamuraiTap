using UnityEngine;
using UnityEngine.UI; // UI要素用の名前空間を追加
using UnityEngine.SceneManagement; // SceneManager用の名前空間を追加
using System.Collections;

public class MenuGUI : MonoBehaviour {
    public Image titleLogo;
    public Texture2D titleTexture;
    public Button startButton;

    void Start() {
        if (titleLogo != null && titleTexture != null) {
            Debug.Log("タイトルロゴを設定します");
            try {
                Sprite titleSprite = Sprite.Create(titleTexture, new Rect(0, 0, titleTexture.width, titleTexture.height), new Vector2(0.5f, 0.5f));
                titleLogo.sprite = titleSprite;
                titleLogo.color = Color.white;
                Debug.Log("タイトルロゴの設定完了");
            } catch (System.Exception e) {
                Debug.LogError("タイトルロゴの設定中にエラー: " + e.Message);
            }
        } else {
            if (titleLogo == null) {
                Debug.LogError("titleLogoがnullです。Inspectorで設定してください。");
            }
            if (titleTexture == null) {
                Debug.LogError("titleTextureがnullです。Inspectorで設定してください。");
            }
        }

        if (startButton != null) {
            startButton.onClick.AddListener(() => {
                SceneManager.LoadScene("Stage 1-1");
            });
        } else {
            Debug.LogError("startButtonがnullです。Inspectorで設定してください。");
        }
    }
}
