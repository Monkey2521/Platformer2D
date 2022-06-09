using UnityEngine;

[CreateAssetMenu(fileName = "New effect", menuName = "Effect")]
public class Effect : ScriptableObject {
	[Header("Debug settings")]
	public bool isDebug = true;

	[Header("Settings")]
	public string _name;
	public float effectTime;
	public Sprite icon;

	public bool _onEffect = false;

	public void TakeEffect () {
		FindObjectOfType<PlayerController> ().TakeEffect (this);
	}

	public void RevealEffect () {
		FindObjectOfType<PlayerController> ().RevealEffect (this);
	}

	public void Make () {
		_onEffect = true;

		switch (name) {
		case "Ciggarete":
				TakeCiggareteEffect();
			Debug.Log ("Make " + name + " effect");
			break;
		case "Mushrooms":
			TakeMushroomEffect ();
			Debug.Log ("Make " + name + " effect");
			break;
		case "Beer":
			TakeBeerEffect ();
			Debug.Log ("Make " + name + " effect");
			break;

		default:
			Debug.Log ("ERROR !!!");
			break;
		}
	}

	public void Reveal () {
		_onEffect = false;

		switch (name) {
		case "Ciggarete":
				RevealCiggareteEffect();
			Debug.Log ("Reveal " + name + " effect");
			break;
		case "Mushrooms":
			RevealMushroomEffect ();
			Debug.Log ("Reveal " + name + " effect");
			break;
		case "Beer":
			RevealBeerEffect ();
			Debug.Log ("Reveal " + name + " effect");
			break;

		default:
			Debug.Log ("ERROR!!!");
			break;
		}

	}

	private void TakeCiggareteEffect() {
		FindObjectOfType<SoundManager> ().UseSmokeSound ();

		float heal = 20f;

		FindObjectOfType<PlayerController> ().Heal (heal);

		FindObjectOfType<PlayerMoveController> ().effectSpeedMutliplier *= 0.9f;
		FindObjectOfType<PlayerAttackController> ().effectDamageMultiplier *= 1.2f;
	}

	private void RevealCiggareteEffect() {
		FindObjectOfType<PlayerMoveController> ().effectSpeedMutliplier /= 0.9f;
		FindObjectOfType<PlayerAttackController> ().effectDamageMultiplier /= 1.2f;
	}

	private void TakeBeerEffect () {
		FindObjectOfType<SoundManager> ().UseBeerSound ();

		float heal = 10f;

		FindObjectOfType<PlayerController> ().Heal (heal);

		FindObjectOfType<PlayerMoveController> ().effectSpeedMutliplier *= 1.4f;
		FindObjectOfType<PlayerAttackController> ().effectDamageMultiplier *= 1.1f;
	}

	private void RevealBeerEffect () {
		FindObjectOfType<PlayerMoveController> ().effectSpeedMutliplier /= 1.4f;
		FindObjectOfType<PlayerAttackController> ().effectDamageMultiplier /= 1.1f;
	}

	private void TakeMushroomEffect () {
		FindObjectOfType<SoundManager> ().UseMushroomSound ();

		FindObjectOfType<PlayerMoveController> ().effectSpeedMutliplier *= 0.5f;
		FindObjectOfType<PlayerAttackController> ().effectDamageMultiplier *= 0.3f;

		FindObjectOfType<CameraEffectContoller> ().TakeMushroomEffect ();
	}

	private void RevealMushroomEffect () {
		FindObjectOfType<PlayerMoveController> ().effectSpeedMutliplier /= 0.5f;
		FindObjectOfType<PlayerAttackController> ().effectDamageMultiplier /= 0.3f;

		FindObjectOfType<CameraEffectContoller> ().RevealMushroomEffect ();
	}
}
