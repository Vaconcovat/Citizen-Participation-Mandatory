using UnityEngine;
using System.Collections;

public class PlayerAttributes : MonoBehaviour {

	public float speed;
	public float MaxHealth = 100f;
	public float CurrentHealth = 0f;
	public GameObject HealthBar;
	public GameObject bloodSplatter;
	public GameObject damageNumbers;

	RoundManager rm;

	// Use this for initialization
	void Start () 
	{
		//init round manager
		rm = FindObjectOfType<RoundManager>();
		//Sets all enemies to full health on startup
		CurrentHealth = MaxHealth;


	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentHealth <= 0){
			//tell the round manager we've died
			rm.Death();
			Destroy(gameObject);
		}
	}

	public void TakeDamage(int damage){
		CurrentHealth -= damage;
		GameObject blood = (GameObject)Instantiate(bloodSplatter, transform.position, Quaternion.identity);
		blood.transform.localScale = new Vector3(Random.Range(0.2f,1.0f),Random.Range(0.2f,1.0f),1);
		blood.transform.rotation = Quaternion.AngleAxis(Random.Range(-90.0f,90.0f), Vector3.forward);
		Vector3 pos = transform.position + new Vector3(Random.Range(-1.0f,1.0f),Random.Range(-1.0f,1.0f),0);
		GameObject number = (GameObject)Instantiate(damageNumbers, pos, Quaternion.identity);
		number.GetComponent<TextMesh>().text = damage.ToString();

		//returns a percent value for the health bar to represent, the value is a post damage value
		float calcHealth = CurrentHealth / MaxHealth;
		//calc health is passed into sethealthbar which updates the visual health bar
		SetHealthBar (calcHealth);
	}

	public void SetHealthBar(float myHealth)
	{
		//myHealth needs to be a value between 0 and 1
		HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x, myHealth, HealthBar.transform.localScale.z);	
	}

	//void decreaseHealth() Testing an Auto Decreasing Health Bar
	//{
		//CurrentHealth -= 2f;
		//returns a percent value for the health bar to represent, the value is a post damage value
		//float calcHealth = CurrentHealth / MaxHealth;
		//calc health is passed into sethealthbar which updates the visual health bar
		//SetHealthBar (calcHealth);
	//}
}
