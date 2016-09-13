using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InputEventHandler : MonoBehaviour {
	public static bool _isStartTouchAction = false;
	public static bool _isEndTouchAction = false;
	public static Vector3 _currentTouchPosition = Vector2.zero;

	void Update () {
		if(Input.GetMouseButtonDown(0)) {

			#if UNITY_EDITOR
			if(!EventSystem.current.IsPointerOverGameObject())
				_isStartTouchAction = true;
			#else
			if(!EventSystem.current.IsPointerOverGameObject(0))
				_isStartTouchAction = true;
			#endif
		}
		if(_isStartTouchAction) {
			Plane plane = new Plane(Vector3.right, Vector3.zero);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float distance = 0;
			if(plane.Raycast(ray, out distance)) {
				_currentTouchPosition = ray.GetPoint(distance);
			}
		}

		if(Input.GetMouseButtonUp(0))
			_isStartTouchAction = false;
	}

}
