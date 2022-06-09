using UnityEngine;

public class SoundManager : MonoBehaviour {
	
	private AudioSource _audioSource;

	public AudioClip pickUpItem;
	public AudioClip pickUpNote;

	public AudioClip buttonClick;

	public AudioClip useBeer;
	public AudioClip useWeed;
	public AudioClip useMushroom;

	private void Awake () {
		GetReferences ();
	}

	public void PickUpItemSound () {
		_audioSource.clip = pickUpItem;
		_audioSource.Play ();
	}

	public void PickUpNoteSound () {
		_audioSource.clip = pickUpNote;
		_audioSource.Play ();
	}

	public void ButtonClickSound () {
		_audioSource.clip = buttonClick;
		_audioSource.Play ();
	}

	public void UseSmokeSound () {
		_audioSource.clip = useWeed;
		_audioSource.Play ();
	}

	public void UseBeerSound () {
		_audioSource.clip = useBeer;
		_audioSource.Play ();
	}

	public void UseMushroomSound () {
		_audioSource.clip = useMushroom;
		_audioSource.Play ();
	}

	private void GetReferences () {
		_audioSource = GetComponent<AudioSource> ();
	}
}
