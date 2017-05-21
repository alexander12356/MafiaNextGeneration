using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses;

public class CameraController : MonoBehaviour {

	[SerializeField]
	private float cameraIn;
	[SerializeField]
	private float cameraOut;
	[SerializeField]
	private GameObject world_1;
	[SerializeField]
	private GameObject world_2;
	[SerializeField]
	private GameObject world_3;
	[SerializeField]
	private GameObject world_4;
	[SerializeField]
	private GameObject WorldCanvas;
    [SerializeField]
    private Camera cam;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			CloseWorld();
            PersonManager.Instance.LeaveWorld();
		}
	}

	IEnumerator DoLerp(Vector3 currentPos, Vector3 endPos) {
		while (currentPos != endPos) {
			currentPos = Vector3.Lerp(currentPos, endPos, Time.deltaTime * 5f);
            currentPos.z = -10.0f;

            transform.position = currentPos;
			yield return new WaitForEndOfFrame();
        }
	}
	IEnumerator DoFieldOfView(float currentValue, float endValue) {
		while (currentValue != endValue) {
            currentValue = Mathf.Lerp(currentValue, endValue, Time.deltaTime * 2f);
            //Camera.main.fieldOfView = currentValue;
            cam.orthographicSize= currentValue;
			yield return new WaitForEndOfFrame();
           
        }
	}
	public void OpenWorld(int id) {
        StartCoroutine(DoFieldOfView(cameraOut, cameraIn));
        switch (id) {
			case 1:
				StartCoroutine(DoLerp(transform.position, world_1.transform.position));
				//TO DO WorldInstance 
				break;
			case 2:
				StartCoroutine(DoLerp(transform.position, world_2.transform.position));
				//TO DO WorldInstance
				break;
			case 3:
				StartCoroutine(DoLerp(transform.position, world_3.transform.position));
				//TO DO WorldInstance
				break;
			case 4:
				StartCoroutine(DoLerp(transform.position, world_4.transform.position));
				//TO DO WorldInstance
				break;
		}
        WorldCanvas.SetActive(false);
    }

	public void CloseWorld() {
        WorldCanvas.SetActive(true);
        StartCoroutine(DoLerp(transform.position, new Vector3(0, 1, -10)));
		StartCoroutine(DoFieldOfView(cameraIn, cameraOut));
		//transform.position = Vector3.Lerp(transform.position, new Vector3(0, 1, -10), Time.deltaTime * 5f);
	}
}
