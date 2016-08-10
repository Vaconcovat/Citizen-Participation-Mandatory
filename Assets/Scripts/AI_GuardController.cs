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
		if(Vector3.Distance(transform.position, target.transform.position)< 1f){
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
			if(target.equipped == null){
				endStatus = endRoundStatus.Capture;
			}
			else{
				if(talktimer <= 0){
					talktimer = minTalkTime;
					if(Random.value < 0.5f){
						c.Say("[Drop the weapon!]");
					}
				}
				else{
					talktimer -= Time.deltaTime;
				}
			}
		}
		else{
			agent.speed = speed;
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
		Debug.DrawRay(transform.position, transform.forward * dist);
		RaycastHit hit;
		Ray r = new Ray(transform.position, transform.forward);
		Physics.Raycast(r, out hit, dist);
		if(hit.collider == losTarget){
			return true;
		}
		else{
			return false;
		}
	}


}
