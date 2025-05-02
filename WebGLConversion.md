# SamuraiTap WebGL変換ガイド

## 概要
このガイドはSamuraiTapをUnity 4.xからUnity 2022.3 LTSにアップグレードし、WebGLビルドに変換するための詳細な手順を提供します。

## 変換手順

### 1. 準備作業

#### 1.1 プロジェクトのバックアップ
- 元のUnity 4.xプロジェクトの完全なバックアップを作成
- 重要なアセットとスクリプトを特定し、リストアップ

#### 1.2 必要なソフトウェアのインストール
- Unity Hub: https://unity.com/download
- Unity 2022.3 LTS: Unity Hubから「インストール」→「2022.3.x LTS」を選択
- Git LFS: 大きなアセットファイルを扱うために必要

### 2. 新しいUnityプロジェクトの作成

#### 2.1 プロジェクト作成
- Unity Hubを開き、「新規作成」をクリック
- テンプレートとして「2D」を選択
- プロジェクト名を「SamuraiTap_WebGL」に設定
- 場所を選択し、「作成」をクリック

#### 2.2 アセットのインポート
- 元のプロジェクトから以下のフォルダをコピー:
  - Assets/Animations
  - Assets/Images
  - Assets/Prefabs
  - Assets/Scenes
  - Assets/Scripts
  - Assets/Sounds
  - Assets/Plugins (iTween.cs)

#### 2.3 プロジェクト設定の調整
- Edit → Project Settings → Player → WebGL設定を開く
- 「Resolution and Presentation」で解像度を960x600に設定
- 「Publishing Settings」で圧縮方式をBrotliに設定
- 「Other Settings」でScripting Backend を「IL2CPP」に設定

### 3. スクリプトの更新

#### 3.1 シーン管理の更新
SceneController.csを以下のように更新:
```csharp
// 古いコード
using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {
    public string nextStage;
    
    void Update() {
        if (isStageEnd && timer > waitingTime && Input.GetButtonDown("Fire1")) {
            Application.LoadLevel(nextStage);
        }
    }
}

// 新しいコード
using UnityEngine;
using UnityEngine.SceneManagement; // 追加
using System.Collections;

public class SceneController : MonoBehaviour {
    public string nextStage;
    
    void Update() {
        if (isStageEnd && timer > waitingTime && Input.GetButtonDown("Fire1")) {
            SceneManager.LoadScene(nextStage); // 更新
        }
    }
}
```

#### 3.2 UI関連スクリプトの更新
LifeGaugeUI.csを完全に書き換え、OnGUIの代わりにCanvasベースのUIを使用:
```csharp
// 新しいコード
using UnityEngine;
using UnityEngine.UI; // 追加
using System.Collections;

public class LifeGaugeUI : MonoBehaviour {
    public Image backgroundImage;
    public Image fillImage;
    
    // 残りのコードは更新済みのLifeGaugeUI.csを参照
}
```

#### 3.3 その他のスクリプト更新
- Tap_to_Start.cs: Application.LoadLevelとguiTextの更新
- GameOverWrapper.cs: Application.LoadLevelとaudioコンポーネントの更新
- SampleGUI.cs: OnGUIベースのUIをCanvasベースに変更（SampleGUI_Canvas.csを参照）

#### 3.4 共通の更新パターン
- `Application.LoadLevel()` → `SceneManager.LoadScene()`
- `guiText` → `Text` コンポーネント
- `audio` → `AudioSource` コンポーネント
- `GUI.BeginGroup()`, `GUI.DrawTexture()` → Canvas, Image コンポーネント
- `SendMessage()` → 直接メソッド呼び出し

### 4. UIシステムの再構築

#### 4.1 Canvasの作成
- 各シーンに「UI → Canvas」を追加
- Canvas Scalerを設定:
  - UI Scale Mode: Scale With Screen Size
  - Reference Resolution: 960x600
  - Screen Match Mode: Match Width Or Height
  - Match: 0.5 (バランス)

#### 4.2 ライフゲージUIの作成
1. Canvasに「UI → Image」を追加し、「LifeGaugeBackground」と名付ける
2. 背景画像のスプライトを設定
3. 「UI → Image」を追加し、「LifeGaugeFill」と名付ける
4. Fill画像のスプライトを設定
5. Image TypeをFilledに変更し、Fill Methodを「Horizontal」に設定
6. LifeGaugeUIスクリプトをアタッチし、参照を設定

#### 4.3 タイトル画面UIの作成
1. Canvasに「UI → Text」を追加し、「TapToStart」と名付ける
2. フォントとスタイルを設定
3. Tap_to_Startスクリプトをアタッチし、Text参照を設定

### 5. シーンの更新

#### 5.1 シーンの変換
- 各シーンを開き、古いGUITextやGUITextureを削除
- 新しいCanvasベースのUIに置き換え
- 参照を更新

#### 5.2 Build Settingsへのシーン追加
- File → Build Settings を開く
- 「Scenes In Build」に以下のシーンを追加:
  - Title.unity
  - Stage 1-1.unity から Stage 1-5.unity
  - Ending.unity

### 6. WebGLビルド設定

#### 6.1 WebGLモジュールのインストール
- Unity Hub → インストール → 「Unity 2022.3.x」を選択 → 「モジュールを追加」
- WebGLビルドサポートを選択してインストール

#### 6.2 ビルド設定
- File → Build Settings を開く
- プラットフォームをWebGLに切り替え
- Player Settings:
  - WebGL Template: Default
  - Memory: 512MB
  - Compression Format: Brotli
  - Decompression Fallback: チェック（古いブラウザ対応）

#### 6.3 ビルド実行
- Build Settingsで「Build」をクリック
- 出力フォルダを選択（例: Build/WebGL）
- ビルドプロセスを待機

### 7. テストとデバッグ

#### 7.1 ローカルテスト
- Unity Editor内でのテスト
- ローカルWebサーバーでのテスト:
  - Python: `python -m http.server`
  - Node.js: `npx http-server`

#### 7.2 一般的な問題と解決策
- テクスチャ圧縮の問題: Player Settings → WebGL → Texture Compression を調整
- メモリ不足: Player Settings → WebGL → Memory Size を増加
- 音声の問題: AudioSource設定を確認、WebGLでは一部の音声機能に制限あり

### 8. デプロイ

#### 8.1 GitHub Pagesへのデプロイ
1. リポジトリのSettingsタブを開く
2. 左側のメニューから「Pages」を選択
3. Source を「main」ブランチに設定
4. フォルダを「/docs」または「/(root)」に設定
5. WebGLビルドファイルをリポジトリの適切な場所にコピー
6. 変更をコミットしてプッシュ

#### 8.2 その他のホスティングオプション
- Netlify
- Vercel
- Firebase Hosting
- Amazon S3

## 変更済みスクリプト
1. SceneController.cs - シーン管理APIを更新
2. LifeGaugeUI.cs - OnGUIをCanvasベースUIに変更
3. Tap_to_Start.cs - シーン読み込みとテキスト処理を更新
4. GameOverWrapper.cs - オーディオとシーン管理を更新
5. SampleGUI_Canvas.cs - SampleGUI.csの代替実装

## 注意点と制限事項
- WebGLビルドではマルチスレッド処理に制限があります（Coroutineを使用）
- ファイルI/O操作はWebGLでは異なる方法で処理する必要があります（PlayerPrefsまたはIndexedDBを使用）
- 一部のプラグインはWebGLと互換性がない可能性があります（iTweenは互換性あり）
- モバイルブラウザでのタッチ入力は追加の処理が必要な場合があります
- WebGLビルドのサイズを小さく保つために、未使用のアセットを削除することを検討

## 参考リソース
- [Unity WebGL ドキュメント](https://docs.unity3d.com/Manual/webgl.html)
- [Unity UI システム](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/index.html)
- [Unity 4.xから5.xへの移行ガイド](https://docs.unity3d.com/Manual/UpgradeGuide5.html)
- [GitHub Pages ドキュメント](https://docs.github.com/ja/pages)
