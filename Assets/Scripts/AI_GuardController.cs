using UnityEngine;
using System.Collections;

public class AI_GuardController : MonoBehaviour {
	public enum Job{StartRound, EndRound};
	public Job job;

	public enum endRoundStatus{Chase, Fight, Capture, Retreat};
	public endRoundStatus endStatus;

	public Contestant target;
	public float closingDistance, closingSpeed, speed;


	public float minTalkTime;

	NavMeshAgent agent;
	Contestant c;
	float talktimer = 0;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		c = GetComponent<Contestant>();
	}
	
	// Update is called once per frame
	void Update () {
		if(job == Job.EndRound){
			switch(endStatus){
				case endRoundStatus.Capture:
					Capture();
					break;
				case endRoundStatus.Chase:
					Chase();
					break;
				case endRoundStatus.Fight:
					Fight();
					break;
				case endRoundStatus.Retreat:
					Retreat();
					break;
			}

		}
	}

	void Capture(){
		agent.destination = target.transform.position;
		agent.speed = speed;
		if(agent.remainingDistance < 1f){
			FindObjectOfType<RoundManager>().endRound();
		}
		if(target.equipped != null){
			endStatus = endRoundStatus.Chase;
		}
	}

	void Chase(){
		agent.destination = target.transform.position;

		if(agent.remainingDistance < closingDistance){
			agent.speed = closingSpeed;
		}
		else{
			agent.speed = speed;
		}

		if(Input.GetKeyDown(KeyCode.E)){
			endStatus = endRoundStatus.Capture;
			if (target.equipped != null){
				target.equipped.Unequip();
				target.equipped = null;
			}
			target.GetComponent<PlayerController>().enabled = false;
			FindObjectOfType<StaticGameStats>().Influence(StaticGameStats.InfluenceTrigger.EndOfRoundSurrender, 0);
		}
		else{
			if(talktimer <= 0){
				talktimer = minTalkTime;
				if(Random.value < 0.5f){
					if(target.equipped != null){
						c.Say("DROP THE WEAPON!");
					}
					else{
						c.Say("SURRENDER!");
					}
				}
			}
			else{
				talktimer -= Time.deltaTime;
			}
		}
	}

	void Fight(){
		agent.destination = target.transform.position;
		if(agent.remainingDistance < closingDistance){
			agent.speed = closingSpeed;
		}
		else{
			agent.speed = speed;
		}
		if(LineOfSight(target.GetComponent<Collider>(),15)){
			c.UseEquipped(true);
			c.UseEquipped(false);
		}
		if(c.GetAmmo() == 0){
			StartRetreat();
		}
		transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,(target.transform.position - transform.position), Time.deltaTime*3, 0));
	}

	void StartRetreat(){
		RoundManager rm = FindObjectOfType<RoundManager>();
		agent.destination = rm.outerSpawns[Random.Range(0,rm.outerSpawns.Count)].position;
		endStatus = endRoundStatus.Retreat;
		agent.speed = speed;
	}

	void Retreat(){
		if(agent.remainingDistance < 1){
			c.equipped.GetComponent<RangedWeapon>().ammo = c.equipped.GetComponent<RangedWeapon>().Maxammo;
			endStatus = endRoundStatus.Fight;
		}
	}

	bool LineOfSight(Collider losTarget, float dist){
		Debug.DrawRay(transform.position+ new Vector3(0,1,0), transform.forward * dist);
		RaycastHit hit;
		Ray r = new Ray(transform.position + new Vector3(0,1,0), transform.forward);
		Physics.Raycast(r, out hit, dist);
		if(hit.collider == losTarget){
			return true;
		}
		else{
			return false;
		}
	}


}
