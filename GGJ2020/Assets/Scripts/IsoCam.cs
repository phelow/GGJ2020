using UnityEngine;

public class IsoCam : MonoBehaviour {
    private Camera mCamera;

    #region Constants
    private const float NORMAL_FOV = 70;
#endregion

    public float dampTime = 0.5f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

    private void Start()
    {
        mCamera = GetComponent<Camera>();
        mCamera.fieldOfView = NORMAL_FOV;

    }

    // Update is called once per frame
    void LateUpdate() {
		if (target) {
			Vector3 point = Camera.main.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}
}