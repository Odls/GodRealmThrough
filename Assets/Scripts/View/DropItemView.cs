using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemView : MonoBehaviour{
	[SerializeField] SpriteRenderer render;
	[SerializeField] float minWaitTime = 0.5f;
	[SerializeField] float maxWaitTime = 1.5f;
	[SerializeField] float moveSpeed = 2f;
	[SerializeField] float hitRadius = 0.1f;

	private void Start() {
		Vector3 _offset = Random.onUnitSphere * 0.6f;
		_offset.y = Mathf.Abs(_offset.y);

		transform.localPosition += _offset;
	}

	public void Pop(Sprite p_icon, Transform p_targetTransform, System.Action p_onHitPlayer) {
		render.sprite = p_icon;
		StartCoroutine(IePop(p_targetTransform, p_onHitPlayer));
	}
	IEnumerator IePop(Transform p_targetTransform, System.Action p_onHitPlayer) {
		yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
		Vector2 _ray;
		do {
			yield return null;

			transform.position = Vector3.MoveTowards(transform.position, p_targetTransform.position, moveSpeed * Time.deltaTime);
			_ray = transform.position - p_targetTransform.position;

		} while (_ray.sqrMagnitude > (hitRadius * hitRadius));

		p_onHitPlayer?.Invoke();

		Destroy(gameObject);
	}
}
