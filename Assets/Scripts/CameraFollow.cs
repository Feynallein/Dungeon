using UnityEngine;

public class CameraFollow : MonoBehaviour{
	public Transform target;

	void FixedUpdate(){
		transform.position = new Vector3(target.position.x+1f, target.position.y+1f, transform.position.z);
	}
    
}
  