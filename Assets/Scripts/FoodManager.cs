using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FoodManager : MonoBehaviour {

    public int foodQuantity = 1500;
    float spawnRange;
     float staticFoodMinSize = 2f;
     float staticFoodMaxSize = 6f;
    public static FoodManager instance;
    public GameObject foodPrefab;
	public GameObject spintfoodPrefab;

    public Color[] foodColorRandomList;
	public Sprite[] foodTypes;
    
//	public Text foodCountTxt;
    void Awake()
    {
        instance = this;
    }
	// Use this for initialization
	void Start () {
        spawnRange = Population.instance.spawnCircleLenght;
        SpawnAllFood();
	}


    void SpawnAllFood() {
        for (int i = 0; i < foodQuantity; i++) {
            SpawnFood(Random.Range(staticFoodMinSize,staticFoodMaxSize), foodColorRandomList[Random.Range(0, foodColorRandomList.Length)],Vector3.zero);
//			SpawnFood(Random.Range(staticFoodMinSize,staticFoodMaxSize), foodColorRandomList[Random.Range(0, foodColorRandomList.Length)],Vector3.zero);

        }
    }
	int foodType=0;

	public void SpawnFood(float power,Color foodColor, Vector3 spawningPosition,bool randomPosition = true, bool isDestroyable = false) {
//		return;
		foodColor.a = 1f;
		GameObject newFood = null;
//		if (!isDestroyable) {
			//			Debug.Log ("spawnFood ");
			newFood = (GameObject)Instantiate(foodPrefab, Vector3.zero, foodPrefab.transform.rotation);
			Food newFoodParameter = newFood.GetComponent<Food>();

			newFoodParameter.isDestroyable = isDestroyable;
			newFoodParameter.SetSize(power);
//			if (!isDestroyable) {
				foodType++;
				if (foodType >= foodTypes.Length)
					foodType = 0;
				newFoodParameter.setFoodType (foodTypes[foodType]);
//			} 
//		else {
//				newFoodParameter.setFoodType (foodTypes[Random.Range(0,foodTypes.Length)]);
//			}


			if (randomPosition)
			{
				MoveTransformOnCircle(newFood.transform);
			}
			else
			{
				newFood.transform.position = spawningPosition;

			}
//		}


        newFood.transform.parent = this.transform;
    }
	public void SpawnFoodDestroyable(float power,Color foodColor, Vector3 spawningPosition,Vector3 initPos,bool randomPosition = true, bool isDestroyable = false) {
//		Debug.LogError ("SpawnFoodDestroyable");
		//		return;
		foodColor.a = 1f;
		GameObject newFood = null;
//			newFood = (GameObject)Instantiate(spintfoodPrefab, Vector3.zero, foodPrefab.transform.rotation);
		newFood = PoolingSystem.Instance.InstantiateAPS ("FoodDestroyable", initPos, Quaternion.Euler(90,0,0));
		newFood.GetComponent<SphereCollider> ().enabled = false;

			newFood.SetActive (true);
			FoodDestroyable newFoodParameterDestroyable = newFood.GetComponent<FoodDestroyable>();
			newFoodParameterDestroyable.isDestroyable = isDestroyable;
			newFoodParameterDestroyable.SetSize(power);
//			if (!isDestroyable) {
//				foodType++;
//				if (foodType >= foodTypes.Length)
//					foodType = 0;
//				newFoodParameterDestroyable.setFoodType (foodTypes[foodType]);
//			} else {
				newFoodParameterDestroyable.setFoodType (foodTypes[Random.Range(0,foodTypes.Length)]);
//			}
			newFoodParameterDestroyable.transform.position=initPos;
			newFoodParameterDestroyable.targetPos = spawningPosition;
			//			if (randomPosition)
			//			{
			//				MoveTransformOnCircle(newFood.transform);
			//			}
			//			else
			//			{
			//				newFood.transform.position = spawningPosition;
			//
			//			}


//		newFood.transform.parent = this.transform;
	}
//	public void SpawnFoodNew(float power,Color foodColor, Vector3 spawningPosition,bool randomPosition = true, bool isDestroyable = false) {
//		foodColor.a = 1f;
//
//		GameObject newFood = (GameObject)Instantiate(spintfoodPrefab, Vector3.zero, spintfoodPrefab.transform.rotation);
//		Food newFoodParameter = newFood.GetComponent<Food>();
//		newFoodParameter.SetSize(power);
//		if (!isDestroyable) {
//			//			Debug.Log ("spawnFood ");
//			newFoodParameter.SetColor (foodColor);
//		} else {
//			newFoodParameter.GlowspriteRenderer.gameObject.SetActive (true);
//		}
////		if (isDiactiveCollider) {
////			newFood.GetComponent<SphereCollider> ().enabled = false;
////		}
//		newFoodParameter.isDestroyable = isDestroyable;
//
//		if (randomPosition)
//		{
//			MoveTransformOnCircle(newFood.transform);
//		}
//		else
//		{
//			newFood.transform.position = spawningPosition;
//
//		}
//
//		newFood.transform.parent = this.transform;
////		return newFood;
//	}
	public void reSpawnFood(GameObject food)
	{
		Color foodColor = foodColorRandomList [Random.Range (0, foodColorRandomList.Length)];
		foodColor.a = 1f;
		Food newFoodParameter = food.GetComponent<Food>();
		newFoodParameter.SetSize(Random.Range(staticFoodMinSize,staticFoodMaxSize));
		newFoodParameter.SetColor(foodColor);
		MoveTransformOnCircle(food.transform);
		food.transform.parent = this.transform;
	}

   public void MoveTransformOnCircle(Transform foodTransform) {
//        Vector3 rangeVector = Random.onUnitSphere * spawnRange;
		Vector3 rangeVector = new Vector3((Random.Range(-Population.instance.RangeX,Population.instance.RangeX)),0,(Random.Range(-Population.instance.RangeZ,Population.instance.RangeZ)));

//        rangeVector.y = 0;
        foodTransform.position = Vector3.zero + rangeVector;
    }
}
