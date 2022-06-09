using UnityEngine;

public class MusicManager : MonoBehaviour {

	private AudioSource _audioSource;

	public AudioClip mainMenuMusic;

	public AudioClip adventureMusic;
	public AudioClip battleMusic;
	public AudioClip lastBattleMusic;
	public AudioClip gamePassMusic;

	public AudioClip houseMusic;

	public AudioClip _currentMusic;
	private float _timer;

	private void Awake () {
		GetReferences ();
	}

	private void Update () {
		if (_currentMusic != null) {
			if (_timer <= 0) {
				if (_currentMusic == houseMusic || _currentMusic == lastBattleMusic || _currentMusic == battleMusic) {
					_timer = _currentMusic.length;
					_audioSource.Play ();
				} else {
					PlayAdventureMusic ();
				}
			} else {
				_timer -= Time.deltaTime;
			}
		}
	}

	public void PlayMenuMusic () {
		_audioSource.clip = mainMenuMusic;

		_currentMusic = mainMenuMusic;
		_timer = mainMenuMusic.length;

		_audioSource.Play ();
	}

	public void PlayAdventureMusic () {
		_audioSource.clip = adventureMusic;

		_currentMusic = adventureMusic;
		_timer = adventureMusic.length;

		_audioSource.Play ();
	}

	public void PlayBattleMusic () {
		_audioSource.clip = battleMusic;

		_currentMusic = battleMusic;
		_timer = battleMusic.length;

		_audioSource.Play ();
	}

	public void PlayLastBattleMusic () {
		_audioSource.clip = lastBattleMusic;

		_currentMusic = lastBattleMusic;
		_timer = lastBattleMusic.length;

		_audioSource.Play ();
	}

	public void PlayGamePassMusic () {
		_audioSource.clip = gamePassMusic;

		_currentMusic = gamePassMusic;
		_timer = gamePassMusic.length;

		_audioSource.Play ();
	}

	public void PlayHouseMusic () {
		_audioSource.clip = houseMusic;

		_currentMusic = houseMusic;
		_timer = houseMusic.length;

		_audioSource.Play ();
	}

	private void GetReferences () {
		_audioSource = GetComponent<AudioSource> ();
	}
}
