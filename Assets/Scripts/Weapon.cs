using UnityEngine;

using System.Collections;

public class Weapon : MonoBehaviour {

	public int ammo;
	public int bulletsPerShot;
	public int damagePerBullet;
	public float spread;
	public float cooldown;
	public float bulletVelocity;
	public GameObject muzzle;
	public bool isEquipped;
	public PlayerControls holder;
	public Texture2D cursorSprite;
	public GameObject bullet;
	public Sprite gunLogo;
	public bool isSponsor;

	Rigidbody2D body;
	BoxCollider2D col;
	float cooldownCounter;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		col = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isEquipped){
			transform.position = holder.gunAnchor.transform.position;
			transform.rotation = holder.gunAnchor.transform.rotation;
			if (cooldownCounter > 0){
				cooldownCounter -= Time.deltaTime;
			}
		}
	}

	public void Fire(){
		if (ammo == 0){
			SpriteRenderer render = GetComponent<SpriteRenderer>();
			render.color = new Color(1,1,1,0.8f);
		}
		if (ammo > 0 && cooldownCounter <= 0){
			for(int i = 0; i <= bulletsPerShot; i++){
				GameObject fired = (GameObject)Instantiate(bullet, muzzle.transform.position, Quaternion.identity * Quaternion.Euler(0f, 0f, -10f));
				//generate a random angle according to spread
				Vector2 angle = new Vector2(transform.right.x + (Random.Range(-spread, spread)), transform.right.y + Random.Range(-spread,+spread)).normalized;
				fired.GetComponent<Bullet>().damage = damagePerBullet;
				fired.GetComponent<Bullet>().Fire(angle * bulletVelocity);
			} 
			ammo--;
			cooldownCounter = cooldown;
		}
	}

	public void Throw(){
		isEquipped = false;
		body.isKinematic = false;
		col.enabled = true;
		holder = null;
		body.AddForce(transform.right.normalized * 10, ForceMode2D.Impulse);
		body.AddTorque(10);
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
	}

	public void Equip(PlayerControls player){
		body.isKinematic = true;
		col.enabled = false;
		isEquipped = true;
		holder = player;
		Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.Auto);
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (body.velocity.magnitude > 30 && coll.gameObject.tag == "Enemy"){
			//coll.gameObject.SendMessage("TakeDamage", Mathf.FloorToInt(body.velocity.magnitude)); //Damage based on velocity
			coll.gameObject.SendMessage("TakeDamage", 10f); //Set Damage
		}
	}

}
