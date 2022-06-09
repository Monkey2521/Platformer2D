using UnityEngine;

public class BeeAI : DamageableObject {

	public Vector2 defaultViewSide;
	protected Vector2 _viewSide;

	protected Transform _transform;
	protected EnemyAttackController _attackController;
	protected BeeMoveAI _moveController;

	public GameObject health;

	public Item dropItem;

	private void Awake () {
		GetReferences ();

		isAlive = true;
		_HP = _maxHP;
		ChangeHeatlBar ();
	}

	public void ColliderEnter (Collider2D collider) {
		if (collider.tag == _attackController.Tag) {
			SetViewSide (collider);
			_moveController.Move (_viewSide);
		}
	}

	public void ColliderStay (Collider2D collider) {
		if (collider.tag == _attackController.Tag) {
			
			SetViewSide (collider);
			_moveController.Move (_viewSide);

			_attackController.Attack ();
		}
	}

	private void SetViewSide (Collider2D collider) {
		if (collider.transform.position.x - _transform.position.x < 0 && !_viewSide.Equals (Vector2.left)) {
			_viewSide = Vector2.left;
		}

		if (collider.transform.position.x - _transform.position.x > 0 && !_viewSide.Equals (Vector2.right)) {
			_viewSide = Vector2.right;
		}
	}

	protected override void ChangeHeatlBar ()
	{
		base.ChangeHeatlBar ();

		if (_HP <= 0) { 
			health.SetActive (false);
		}


		health.transform.localScale = new Vector3 (_HP / _maxHP, 1f, 1f);
	}

	public void DeleteCollider () {
		if (GetComponentInChildren<CircleCollider2D> () != null) {
			GetComponentInChildren<CircleCollider2D> ().enabled = false;
		}
		GetComponent<CapsuleCollider2D> ().enabled = false;
	}

	public override void Destroy ()
	{
		base.Destroy ();

		ItemInventory _itemInventory = new ItemInventory (dropItem);
		_itemInventory.count = 1;

		Inventory.instance.Add (_itemInventory);

		Destroy (this.gameObject);
	}

	private void GetReferences () {
		_attackController = GetComponentInChildren<EnemyAttackController> ();
		_moveController = GetComponent<BeeMoveAI> ();

		_transform = GetComponent<Transform> ();
		_animator = GetComponent<Animator> ();

		_viewSide = defaultViewSide;
	}
}
