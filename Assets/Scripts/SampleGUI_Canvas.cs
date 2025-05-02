using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;


public class SampleGUI_Canvas : MonoBehaviour {
    [Header("基本UI要素")]
    public Text labelText;
    public Button button;
    public Button popupButton;
    public Button repeatButton;
    
    [Header("スライダー")]
    public Slider horizontalSlider;
    public Slider verticalSlider;
    
    [Header("入力フィールド")]
    public InputField textField;
    public InputField textArea;
    
    [Header("その他のUI要素")]
    public Toggle toggle;
    public ToggleGroup toolbarGroup;
    public Toggle[] toolbarToggles;
    public ToggleGroup selectionGrid;
    public Toggle[] selectionToggles;
    
    [Header("ポップアップウィンドウ")]
    public GameObject popupWindow;
    public Button closeWindowButton;
    
    private float horizontalSliderValue;
    private float verticalSliderValue;
    private string textFieldValue = "";
    private string textAreaValue = "";
    private bool toggleState;
    private int toolbarValue = 0;
    private int selectionGridValue = 0;
    
    void Start() {
        if (button != null) {
            button.onClick.AddListener(() => {
                Debug.Log("Click Button!");
            });
        }
        
        if (popupButton != null) {
            popupButton.onClick.AddListener(() => {
                if (popupWindow != null) {
                    popupWindow.SetActive(true);
                }
            });
        }
        
        if (closeWindowButton != null) {
            closeWindowButton.onClick.AddListener(() => {
                if (popupWindow != null) {
                    popupWindow.SetActive(false);
                }
            });
        }
        
        if (horizontalSlider != null) {
            horizontalSlider.onValueChanged.AddListener((value) => {
                horizontalSliderValue = value;
                Debug.Log("horizontal slider! " + horizontalSliderValue);
            });
        }
        
        if (verticalSlider != null) {
            verticalSlider.onValueChanged.AddListener((value) => {
                verticalSliderValue = value;
                Debug.Log("vertical slider! " + verticalSliderValue);
            });
        }
        
        if (textField != null) {
            textField.onValueChanged.AddListener((value) => {
                textFieldValue = value;
                Debug.Log("textfield! " + textFieldValue);
            });
        }
        
        if (textArea != null) {
            textArea.onValueChanged.AddListener((value) => {
                textAreaValue = value;
                Debug.Log("textArea! " + textAreaValue);
            });
        }
        
        if (toggle != null) {
            toggle.onValueChanged.AddListener((value) => {
                toggleState = value;
                Debug.Log("toggle! " + toggleState);
            });
        }
        
        if (toolbarToggles != null && toolbarToggles.Length > 0) {
            for (int i = 0; i < toolbarToggles.Length; i++) {
                int index = i; // ローカル変数にコピー
                if (toolbarToggles[i] != null) {
                    toolbarToggles[i].onValueChanged.AddListener((value) => {
                        if (value) {
                            toolbarValue = index;
                            Debug.Log("toolbar! " + toolbarValue);
                        }
                    });
                }
            }
        }
        
        if (selectionToggles != null && selectionToggles.Length > 0) {
            for (int i = 0; i < selectionToggles.Length; i++) {
                int index = i; // ローカル変数にコピー
                if (selectionToggles[i] != null) {
                    selectionToggles[i].onValueChanged.AddListener((value) => {
                        if (value) {
                            selectionGridValue = index;
                            Debug.Log("selectionGrid! " + selectionGridValue);
                        }
                    });
                }
            }
        }
        
        if (popupWindow != null) {
            popupWindow.SetActive(false);
        }
    }
    
    void Update() {
        if (repeatButton != null && repeatButton.IsPressed()) {
            Debug.Log("Click RepeatButton!");
        }
    }
}

public static class ButtonExtension {
    public static bool IsPressed(this Button button) {
        return UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == button.gameObject 
            && Input.GetMouseButton(0);
    }
}
