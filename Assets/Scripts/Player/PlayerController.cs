using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : DamageableObject {
	#region Control keys
	public KeyCode moveLeftKey;
	public KeyCode moveRightKey;
	public KeyCode moveUpOnStairsKey;
	public KeyCode moveDownOnStairsKey;
	public KeyCode jumpKey;
	public KeyCode runKey;
	public KeyCode attackKey;
	public KeyCode actionKey;
	public KeyCode inventoryKey;
	public KeyCode closeKey;
	#endregion

	#region Controllers
	private PlayerAttackController _attackController;

	private PlayerActionController _actionController;
	#endregion

	#region GUI
	[Header("UI elements")]
	[SerializeField] private GameObject _inventoryPanel;
	[SerializeField] private GameObject _itemInfoPanel;
	[SerializeField] private GameObject _notesPanel;
	[SerializeField] private GameObject _promptsPanel;
	public GameObject actionPanel;
	public GameObject _activeUI = null;

	public GameObject UI;
	public GameObject pauseUI;
	public GameObject health;

	public static bool _onUI = false;
	public static bool _onPause = false;
	#endregion

	#region CheckPoint
	private Vector3 _checkpointPosition = new Vector3 (0f, 0f, 0f);
	#endregion

	#region Effects
	public List<Effect> effects = new List<Effect> ();
	public GameObject ciggarete;
	public static bool smoking = false;
	#endregion

	private void Awake () {
		GetReferences ();

		transform.position = _checkpointPosition;

		isAlive = true;
		_HP = _maxHP;

		_animator.SetBool ("Alive", true);

		if (!smoking) {
			HideCiggarete ();
		}
	}

	public void SetCheckpointPosition (Vector3 position) {
		_checkpointPosition = position;
	}

	public void SetPositionInCheckpoint () {
		transform.position = _checkpointPosition;
	}

	public void SetPositionInHouse (Vector3 position) {
		transform.position = position;
	}

	private void Start () {
		_promptsPanel.SetActive (false);
	}

	private void Update () {
		if (isAlive) {
			if (!_onUI && !_onPause) {
				if (Input.GetKey (attackKey)) {
					_attackController.Attack ();
				}

				if (_actionController._canAction && !_actionController._onAction) {
					if (Input.GetKeyDown (actionKey)) {
						actionPanel = _actionController.Action (); 

						if (actionPanel != null) {
							ShowUI (actionPanel);
						} else {
							_actionController.StopAction ();
						}

						return;
					}
				}
			}
			if (!_onPause) {
				if (Input.GetKeyDown (inventoryKey)) {
					if (_activeUI == actionPanel && _activeUI != null) {
						CloseActiveUI ();
						_actionController.StopAction ();
						ShowUI (_inventoryPanel);
						_itemInfoPanel.SetActive (false);
						FindObjectOfType<InventoryItemContoller> ().UpdateUI ();
						return;
					} else if (_activeUI != null) { 
						CloseActiveUI ();
						return;
					} else {
						ShowUI (_inventoryPanel);
						_itemInfoPanel.SetActive (false);
						FindObjectOfType<InventoryItemContoller> ().UpdateUI ();
						return;
					}
				}

				if (Input.GetKeyDown (closeKey) && _activeUI != null) {
					CloseActiveUI ();
					return;
				}
			}

			if (Input.GetKeyDown (closeKey)) {
				Pause ();
			}
		}
	}

	#region UI
	public void CloseActiveUI () {
		if (_activeUI == actionPanel) {
			_actionController.StopAction ();
		}

		if (_activeUI == _inventoryPanel && FindObjectOfType<InventoryItemInfo> () != null) {
			FindObjectOfType<InventoryItemInfo> ().HideMessageBox ();
		}

		if (_activeUI == _notesPanel && FindObjectOfType<Notes> () != null) {
			FindObjectOfType<Notes> ().notePanel.SetActive (false);
		}

		_activeUI.SetActive (false);
		_activeUI = null;

		actionPanel = null;

		_onUI = false;

		UI.SetActive (true);
	}

	private void ShowUI (GameObject currentUI) {
		_activeUI = currentUI.gameObject;
		_activeUI.SetActive (true);
		_onUI = true;
		UI.SetActive (false);
	}

	public void Pause () {
		_onPause = !_onPause;

		pauseUI.SetActive (!pauseUI.activeSelf);
		UI.SetActive (!UI.activeSelf);
	}

	public void OpenInventory () {
		ShowUI (_inventoryPanel);
		_itemInfoPanel.SetActive (false);
		FindObjectOfType<InventoryItemContoller> ().UpdateUI ();
		UI.SetActive (false);
	}

	public void OpenNotes () {
		ShowUI (_notesPanel);
		UI.SetActive (false);
	}

	public void OpenPrompts () {
		ShowUI (_promptsPanel);
		UI.SetActive (false);
	}

	protected override void ChangeHeatlBar ()
	{
		if (_HP <= 0) { 
			health.SetActive (false);
		}

		health.transform.localScale = new Vector3 (_HP / _maxHP, 1f, 1f);
	}
	#endregion

	private void GetReferences () {
		_attackController = GetComponentInChildren<PlayerAttackController> ();
		_actionController = GetComponentInChildren<PlayerActionController> ();

		_animator = GetComponent<Animator> ();
	}

	#region Die functions
	public delegate void OnPlayerDie ();
	public OnPlayerDie OnPlayerDieCallback;

	public void OnPlayerDieCall () {
		OnPlayerDieCallback.Invoke ();
	}

	public override void Destroy ()
	{
		if (_activeUI != null) {
			CloseActiveUI ();
		}

		Debug.Log ("Effects = " + effects.Count);

		for (int i = 0; i < effects.Count;) {
			Debug.Log (effects[0].name);
			RevealEffect (effects[0]);
		}

		Debug.Log ("Effects after = " + effects.Count);

		if (OnEffectChangedCallback != null) {
			OnEffectChangedCallback.Invoke ();
		}

		this.gameObject.SetActive (false);
		_animator.SetBool ("Alive", false);
	}

	public void Restart () {
		_HP = _maxHP / 2;
		health.SetActive (true);
		ChangeHeatlBar ();

		transform.position = _checkpointPosition;

		_animator.SetBool ("Alive", true);
		isAlive = true;
	}
	#endregion


	#region Effects
	public delegate void OnEffectChanged ();
	public OnEffectChanged OnEffectChangedCallback;

	public delegate void OnDamageChanged ();
	public OnDamageChanged OnDamageChangedCallback;

	public delegate void OnSpeedChanged ();
	public OnSpeedChanged OnSpeedChangedCallback;

	public void TakeEffect (Effect effect) {

		if (UnderEffect (effect)) {
			RevealEffect (effect);
			effects.Add (effect);
		} else {
			effects.Add (effect);
		}

		effect.Make ();

		if (effect.name == "Ciggarete") {
			smoking = true;
		}

		if (OnEffectChangedCallback != null) {
			OnEffectChangedCallback.Invoke ();
		}

		if (OnSpeedChangedCallback != null) {
			OnSpeedChangedCallback.Invoke ();
		}
		if (OnDamageChangedCallback != null) {
			OnDamageChangedCallback.Invoke ();
		}
	}

	private bool UnderEffect (Effect effect) {
		return effect._onEffect;
	}

	public void RevealEffect (Effect effect) {

		effect.Reveal ();

		if (effect.name == "Ciggarete") {
			smoking = false;
		}

		effects.Remove (effect);

		if (OnSpeedChangedCallback != null) {
			OnSpeedChangedCallback.Invoke ();
		}
		if (OnDamageChangedCallback != null) {
			OnDamageChangedCallback.Invoke ();
		}
	}

	public void ShowCiggarete () {
		ciggarete.SetActive (true);
	}

	public void HideCiggarete () {
		ciggarete.SetActive (false);
	}
	#endregion
}
