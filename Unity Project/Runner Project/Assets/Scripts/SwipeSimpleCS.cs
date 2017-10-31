using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class SwipeSimpleCS : MonoBehaviour {

	public Transform player; // Drag your player here
	private Vector2 fp; // first finger position
	private Vector2 lp; // last finger position

	public ThirdPersonUserControl tpc;
	public float speed = 20;

	private Vector3 newLerpPosition = Vector3.zero;
	private bool lerpInAction = false;


	void Update () {

		if(player.position.z > 70) {
			player.position = new Vector3 (player.position.x,player.position.y,-22);
		}
		
		foreach(Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				fp = touch.position;
				lp = touch.position;
			}
			if (touch.phase == TouchPhase.Moved )
			{
				lp = touch.position;
			}
			if(touch.phase == TouchPhase.Ended)
			{
				if((fp.x - lp.x) > 80) // left swipe
				{
					newLerpPosition = player.position + new Vector3(-2,0,0);
					lerpInAction = true;
				}
				else if((fp.x - lp.x) < -80) // right swipe
				{
					newLerpPosition = player.position + new Vector3(2,0,0);
					lerpInAction = true;
				}
				else if((fp.y - lp.y) < -80 ) // up swipe
				{
					tpc.m_Jump = true;
					// add your jumping code here
				}
			}
		}

		if(Input.GetKeyDown (KeyCode.LeftArrow)) {
			newLerpPosition = player.position + new Vector3(-2,0,0);
			lerpInAction = true;
		}

		if(Input.GetKeyDown (KeyCode.RightArrow)) {
			newLerpPosition = player.position + new Vector3(2,0,0);
			lerpInAction = true;
		}

		if(Input.GetKeyDown (KeyCode.UpArrow)) {
			tpc.m_Jump = true;
		}

		if(lerpInAction) {
			player.position = Vector3.Lerp(player.position,newLerpPosition,Time.deltaTime*speed);

			if(Mathf.Abs(Mathf.Abs(player.position.x)-Mathf.Abs(newLerpPosition.x)) < 0.3f) {
				lerpInAction = false;
				player.position = new Vector3(newLerpPosition.x,player.position.y,player.position.z);
			}
		}
	}
}
