using UnityEngine;
using System.Collections;

/// <summary>
/// Creating instance of sounds from code with no effort
/// </summary>
public class SoundEffectsHelper : MonoBehaviour
{
	
	/// <summary>
	/// Singleton
	/// </summary>
	public static SoundEffectsHelper Instance;
	
	// public AudioClip dashSound;
	public AudioClip explosionSound;
	public AudioClip playerShotSound;
	public AudioClip enemyShotSound;
	public AudioClip noDamageSound;
	public AudioClip destroySound;
	public AudioClip bossDestroySound;
	public AudioClip healingSound;

	
	void Awake()
	{
		Application.targetFrameRate = 60;

		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundEffectsHelper!");
		}
		Instance = this;
	}
	
//	public void MakeDashSound()
//	{
//		MakeSound(dashSound);
//	}

	public void MakeExplosionSound()
	{
		MakeSound(explosionSound);
	}
	
	public void MakePlayerShotSound()
	{
		MakeSound(playerShotSound);
	}
	
	public void MakeEnemyShotSound()
	{
		MakeSound(enemyShotSound);
	}

	public void MakeNoDamageSound()
	{
		MakeSound(noDamageSound);
	}

	public void MakeDestroySound()
	{
		MakeSound(destroySound);
	}

	public void MakeBossDestroySound()
	{
		MakeSound(bossDestroySound);
	}

	public void MakeHealingSound()
	{
		MakeSound(healingSound);
	}
	
	/// <summary>
	/// Play a given sound
	/// </summary>
	/// <param name="originalClip"></param>
	private void MakeSound(AudioClip originalClip)
	{
		// As it is not 3D audio clip, position doesn't matter.
		AudioSource.PlayClipAtPoint(originalClip, transform.position);
	}

//	private void StopSound(AudioClip originalClip)
//	{
//		dashSound.Stop();
//	}
}