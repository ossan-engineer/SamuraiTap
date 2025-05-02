using UnityEngine;
using UnityEngine.SceneManagement; // 新しいシーン管理用の名前空間を追加
using System.Collections;

public class GameOverWrapper : MonoBehaviour {

	public bool isGameOver = false;
	// public AudioClip SEClip;

	public float timer;
	public float waitingTime = 2f;
	
	private AudioSource audioSource;
	
	void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	// Use this for initialization
	public void BlowOff () {
		Hashtable hTable = new Hashtable();
		hTable.Add ("y", 8f);
		hTable.Add ("easeType", "easeOutQuad");
		// hTable.Add ("easeType", "easeOutBounce");
		hTable.Add ("oncomplete", "CompleteHandler");
		hTable.Add ("time", 1);
		// hTable.Add ("delay", 1);
		iTween.MoveTo(gameObject, hTable);
		Debug.Log ("BlowOff");

		// audio.PlayOneShot (SEClip); // 非推奨APIを削除

		isGameOver = true;
		
		if (audioSource != null) {
			audioSource.Play();
		}
		
		Debug.Log(isGameOver);
	}

	void Start () {
		// audio.clip = SEClip; // 非推奨APIを削除
	}

	// Update is called once per frame
	void Update () {
		if (isGameOver) {

			timer += Time.deltaTime;
			if(timer > waitingTime){
				//Action
				if (Input.GetButtonDown("Fire1")) {
					// Application.LoadLevel("Title"); // 非推奨APIを削除
					SceneManager.LoadScene("Title"); // 新しいシーン読み込みAPIを使用
				}
				// timer = 0;
			}
		}	
	}
}
