using UnityEngine;

public class PlayerAudioManager : MonoBehaviour {

	private AudioSource _audioSource;

	public AudioClip jump;
	public AudioClip attack;
	public AudioClip takeDamage;
	public AudioClip die;

	private void Awake () {
		GetReferences ();
	}

	public void JumpSound () {
		_audioSource.clip = jump;
		_audioSource.Play ();
	}

	public void AttackSound () {
		_audioSource.clip = attack;
		_audioSource.Play ();
	}

	public void TakeDamageSound () {
		_audioSource.clip = takeDamage;
		_audioSource.Play ();
	}

	public void DieSound () {
		_audioSource.clip = die;
		_audioSource.Play ();
	}

	private void GetReferences () {
		_audioSource = GetComponent<AudioSource> ();
	}
}
