using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class SampleGUI : MonoBehaviour {
	private float horizontalSliderValue;
	private float verticalSliderValue;
	private string textFieldValue = "";
	private string textAreaValue = "";
	private bool toggleState;
	private int toolbarValue=0;
	private string[] toolbarStrings =  {
		"Toolbar1", "Toolbar2", "Toolbar3"};
	private int selectionGridValue=0;
	private string[] selStrings =  {
		"Grid 1", "Grid 2", "Grid 3",
		"Grid 4", "Grid 5", "Grid 6"};
	private bool visibleWindow = false;
	private Rect windowRect = new Rect(5, 5, 250, 100);
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){
		drawGUI();
		
		GUI.BeginGroup(new Rect(255, 0, 255, 505));
		drawGUI();
		GUI.EndGroup();
		
		//Box
		GUI.Box(new Rect(515, 5, 250, 500), "This is GUI Sample"); 
		GUILayout.BeginArea(new Rect(520, 30, 240, 470));
		drawLayout();
		GUILayout.EndArea();
	}
	
	private void drawGUI(){
		//Box
		GUI.Box(new Rect(5, 5, 250, 500), "This is GUI Sample"); 
		
		//Label Text
		GUI.Label (new Rect(10, 30, 100, 30), "This is Label");
		
		//Button
		if(GUI.Button(new Rect(10, 60, 100, 30), "This is Button")){
			Debug.Log("Click Button!");
		}
		
		//RepeatButton
		if(GUI.RepeatButton(new Rect(10, 100, 200, 30), "This is Repeat Button")){
			Debug.Log("Click RepeatButton!");
		}
		
		//HorizontalSlider
		float hv = GUI.HorizontalSlider(new Rect(10, 140, 100, 10), horizontalSliderValue, 0.0f, 100.0f);
		if(hv !=  horizontalSliderValue){
			horizontalSliderValue = hv;
			Debug.Log("horizontal slider! "+horizontalSliderValue);
		}
		
		//VerticalSlider
		float vv = GUI.VerticalSlider(new Rect(10, 155, 10, 135), verticalSliderValue, 0.0f, 100.0f);
		if(vv != verticalSliderValue){
			verticalSliderValue = vv;
			Debug.Log("vertical slider! "+verticalSliderValue);
		}
		
		//TextField
		string tf = GUI.TextField(new Rect(30, 160, 80, 30), textFieldValue, 10);
		if(tf != textFieldValue){
			textFieldValue = tf;
			Debug.Log("textfield! "+textFieldValue);
		}
		
		//TextArea
		string ta = GUI.TextArea(new Rect(30, 200, 80, 90), textAreaValue);
		if(ta != textAreaValue){
			textAreaValue = ta;
			Debug.Log("textArea! "+textAreaValue);
		}
		
		//Toggle
		bool b = GUI.Toggle(new Rect(10, 300, 100, 30), toggleState, "This is Toggle");
		if(b != toggleState){
			toggleState = b;
			Debug.Log("toggle! "+toggleState);
		}
		
		//Toolbar
		int tb = GUI.Toolbar (new Rect (10, 340, 240, 30), toolbarValue, toolbarStrings);
		if(tb != toolbarValue){
			toolbarValue = tb;
			Debug.Log("toolbar! "+toolbarValue);
		}
		
		//SelectionGrid
		int sg = GUI.SelectionGrid(new Rect(10, 380, 240, 80), selectionGridValue, selStrings, 3);
		if(sg != selectionGridValue){
			toolbarValue = sg;
			Debug.Log("selectionGrid! "+selectionGridValue);
		}
		
		//PopupWindows
		if(GUI.Button(new Rect(120, 60, 120, 30), "Popup Windows!")){
			visibleWindow = true;
		}
		if(visibleWindow){
			windowRect = GUI.Window(0, windowRect, DoMyWindow, "Popup Window");
		}
		
	}
	
	void DoMyWindow(int windowID) {
		
		//Label Text
		GUI.Label (new Rect(60, 25, 240, 30), "This is PopUp Window");
		
		//Button
		if(GUI.Button(new Rect(30, 60, 200, 30), "Close")){
			visibleWindow = false;
		}
		
		GUI.DragWindow(new Rect(0, 0, 10000, 20));
	}
	
	void drawLayout(){
		GUILayout.BeginVertical();
		//Label Text
		GUILayout.Label ("This is Label");
		GUILayout.BeginHorizontal();
		//Button
		if(GUILayout.Button("This is Button")){
			Debug.Log("Click Button!");
		}
		//PopupWindows
		if(GUILayout.Button("Popup Windows!")){
			visibleWindow = true;
		}
		if(visibleWindow){
			windowRect = GUI.Window(0, windowRect, DoMyWindow, "Popup Window");
		}
		GUILayout.EndHorizontal();
		//RepeatButton
		if(GUILayout.RepeatButton("This is Repeat Button")){
			Debug.Log("Click RepeatButton!");
		}
		//HorizontalSlider
		float hv = GUILayout.HorizontalSlider(horizontalSliderValue, 0.0f, 100.0f);
		if(hv !=  horizontalSliderValue){
			horizontalSliderValue = hv;
			Debug.Log("horizontal slider! "+horizontalSliderValue);
		}
		GUILayout.BeginHorizontal();
		//VerticalSlider
		float vv = GUILayout.VerticalSlider(verticalSliderValue, 0.0f, 100.0f, GUILayout.Height(135));
		if(vv != verticalSliderValue){
			verticalSliderValue = vv;
			Debug.Log("vertical slider! "+verticalSliderValue);
		}
		GUILayout.BeginVertical();
		//TextField
		string tf = GUILayout.TextField(textFieldValue, 10);
		if(tf != textFieldValue){
			textFieldValue = tf;
			Debug.Log("textfield! "+textFieldValue);
		}
		
		//TextArea
		string ta = GUILayout.TextArea(textAreaValue, GUILayout.Height(90));
		if(ta != textAreaValue){
			textAreaValue = ta;
			Debug.Log("textArea! "+textAreaValue);
		}
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		//Toggle
		bool b = GUILayout.Toggle(toggleState, "This is Toggle");
		if(b != toggleState){
			toggleState = b;
			Debug.Log("toggle! "+toggleState);
		}
		//FlexibleSpace
		GUILayout.FlexibleSpace();
		//Toolbar
		int tb = GUILayout.Toolbar (toolbarValue, toolbarStrings);
		if(tb != toolbarValue){
			toolbarValue = tb;
			Debug.Log("toolbar! "+toolbarValue);
		}
		//Space
		GUILayout.Space(10);
		//SelectionGrid
		int sg = GUILayout.SelectionGrid(selectionGridValue, selStrings, 3);
		if(sg != selectionGridValue){
			toolbarValue = sg;
			Debug.Log("selectionGrid! "+selectionGridValue);
		}
		GUILayout.EndVertical();
	}
}
