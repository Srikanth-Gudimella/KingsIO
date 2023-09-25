using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkArea : MonoBehaviour {
//	Random.insideUnitSphere * 250;
		void OnDrawGizmos() {
					Gizmos.color = new Color(1, 0, 0, 0.5F);
					//		Gizmos.DrawCube(transform.position, new Vector3(foodRegionMultiplier*enemyRegion, foodRegionMultiplier*enemyRegion, foodRegionMultiplier*enemyRegion));

		Gizmos.DrawCube(transform.position, new Vector3((1200),50,(560)));
			
					//		Gizmos.color = new Color(1, 0, 0, 0.1F);
					//		Gizmos.DrawCube(transform.position, new Vector3(5*enemyRegion/4, 5*enemyRegion/4, 5*enemyRegion/4));
				}
}
