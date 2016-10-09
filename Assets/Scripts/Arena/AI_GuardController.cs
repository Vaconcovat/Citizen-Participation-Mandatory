using UnityEngine;
using System.Collections;

public class AI_GuardController : MonoBehaviour {
	public enum Job{StartRound, EndRound};
	public Job job;

	public enum endRoundStatus{Chase, Fight, Capture, Retreat, TutorialGuard, TutorialEscort};
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
		minTalkTime += Random.Range(-2.0f, 2.0f);
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
				case endRoundStatus.TutorialGuard:
					TutorialGuard();
					break;
				case endRoundStatus.TutorialEscort:
					TutorialEscort();
					break;
			}

		}
	}

	void Capture(){
		agent.destination = target.transform.position;
		agent.speed = speed;
		c.moving = true;
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
			c.moving = false;
		}
		else{
			agent.speed = speed;
			c.moving = true;
		}

		if(Input.GetKeyDown(KeyCode.Q)){
			endStatus = endRoundStatus.Capture;
			if (target.equipped != null){
				target.equipped.Unequip();
				target.equipped = null;
			}
			target.GetComponent<PlayerController>().enabled = false;
			if (!StaticGameStats.instance.FirstRun) {
				StaticGameStats.instance.Influence (StaticGameStats.InfluenceTrigger.EndOfRoundSurrender, 0);
			}
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
		transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,(target.transform.position - transform.position), Time.deltaTime*3, 0));
	}

	void Fight(){
		agent.destination = target.transform.position;
		if(agent.remainingDistance < closingDistance){
			agent.speed = closingSpeed;
			c.moving = false;
		}
		else{
			agent.speed = speed;
			c.moving = true;
		}
		if(LineOfSight(target.GetComponent<Collider>(),15) && target.isAlive){
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
		if(rm != null){
			agent.destination = rm.outerSpawns[Random.Range(0,rm.outerSpawns.Count)].position;
		}
		else{
			TutorialSpawner[] spawners = FindObjectsOfType<TutorialSpawner>();
			agent.destination = spawners[Random.Range(0,spawners.Length)].transform.position;
		}

		endStatus = endRoundStatus.Retreat;
		agent.speed = speed;
		c.moving = true;
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

	void TutorialGuard(){
		agent.destination = target.transform.position;
		transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,(target.transform.position - transform.position), Time.deltaTime*3, 0));
		c.moving = false;
		agent.speed = 0;
		if(Vector3.Distance(target.transform.position, transform.position) < 3.2f){
			if(c.currentTalkCard == null){
				c.Say((Random.value > 0.5f)?"BACK OFF":"STEP BACK",35);
			}
		}
		if(Vector3.Distance(target.transform.position, transform.position) < 1f){
			AI_GuardController[] guards = FindObjectsOfType<AI_GuardController>();
			foreach(AI_GuardController guard in guards){
				guard.endStatus  = AI_GuardController.endRoundStatus.Fight;
			}
		}
		if(talktimer <= 0){
			talktimer = minTalkTime + Random.Range(-2.0f, 2.0f);
			if (Random.value <= 0.5f && c.currentTalkCard == null) {
				if (target.equipped == null) {
					c.Say (FindObjectOfType<ContestantGenerator>().GetLine(ContestantGenerator.LineType.GuardPassive));
				} else {
					c.Say (FindObjectOfType<ContestantGenerator>().GetLine(ContestantGenerator.LineType.GuardThreat));
				}
			}
		}
		else{
			talktimer -= Time.deltaTime;
		}
	}

	void TutorialEscort(){
		agent.destination = target.transform.position;
		transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,(target.transform.position - transform.position), Time.deltaTime*3, 0));
		if(agent.remainingDistance < closingDistance - 2){
			agent.speed = closingSpeed;
			c.moving = false;
		}
		else{
			agent.speed = speed;
			c.moving = true;
		}
	}
}
