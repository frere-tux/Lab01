using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]
public class RaycastController : MonoBehaviour {

	public LayerMask collisionMask;
	
	public const float skinWidth = .015f;
	const float dstBetweenRays = .25f;
	[HideInInspector]
	public int horizontalRayCount;
	[HideInInspector]
	public int verticalRayCount;

	[HideInInspector]
	public float horizontalRaySpacing;
	[HideInInspector]
	public float verticalRaySpacing;

	[HideInInspector]
	public BoxCollider controllerCollider;
	public RaycastOrigins raycastOrigins;

	public virtual void Awake() {
		controllerCollider = GetComponent<BoxCollider> ();
	}

	public virtual void Start() {
		CalculateRaySpacing ();
	}

	public void UpdateRaycastOrigins() {
		Bounds bounds = controllerCollider.bounds;
		bounds.Expand (skinWidth * -2);
		
		raycastOrigins.bottomLeft = new Vector3 (bounds.min.x, bounds.min.y, transform.position.z);
		raycastOrigins.bottomRight = new Vector3 (bounds.max.x, bounds.min.y, transform.position.z);
		raycastOrigins.topLeft = new Vector3 (bounds.min.x, bounds.max.y, transform.position.z);
		raycastOrigins.topRight = new Vector3 (bounds.max.x, bounds.max.y, transform.position.z);
	}
	
	public void CalculateRaySpacing() {
		Bounds bounds = controllerCollider.bounds;
		bounds.Expand (skinWidth * -2);

		float boundsWidth = bounds.size.x;
		float boundsHeight = bounds.size.y;
		
		horizontalRayCount = Mathf.RoundToInt (boundsHeight / dstBetweenRays);
		verticalRayCount = Mathf.RoundToInt (boundsWidth / dstBetweenRays);
		
		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}
	
	public struct RaycastOrigins {
		public Vector3 topLeft, topRight;
		public Vector3 bottomLeft, bottomRight;
	}
}
