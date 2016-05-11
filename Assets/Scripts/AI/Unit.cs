using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public Transform firstTarget;
	public Transform secondTarget;
    float speed = .1f;
    Vector3[] path;
    int targetIndex;
	float timer = 1f;
	public bool pathReached = false;
	public Vector3 targetPos;

    void Start()
    {
		Debug.Log ("Path Requested!");
		targetPos = firstTarget.position;
		//Updates path when called
		PathRequester.RequestPath (transform.position, firstTarget.position, OnPathFound);


		Debug.Log ("Start: Target is located at " + targetPos);
		StartCoroutine("updatePath");
    }

	IEnumerator updatePath()
	{
		Debug.Log ("\tUpdating Path: Target is located at " + targetPos);
		Debug.Log (pathReached);
		while(pathReached == false)
		{
			//Debug.Log ("\tPath hasn't been reached: Target is located at " + targetPos);
			if (targetPos != firstTarget.position) {
				targetPos = firstTarget.position;
				//Debug.Log ("\tNew Path Requested: Target is located at " + targetPos);
				//Debug.Log ("\tNew Path Requested: Target is located at " + firstTarget.position);
				PathRequester.RequestPath (transform.position, firstTarget.position, OnPathFound);
				yield return new WaitForSeconds (timer);
			} if (Vector3.Distance(transform.position,firstTarget.position) < 0.1f) {
				//If at final position then do something next // Debug.Log ("\tTarget Position Reached");
				pathReached = true;
				yield return null;
			}

			yield return null;
		}
		/*while(pathReached == true)
		{
			Debug.Log ("\tPath hasn't been reached: Target is located at " + targetPos);
			if (targetPos != secondTarget.position) {
				targetPos = secondTarget.position;
				//Debug.Log ("\tNew Path Requested: Target is located at " + targetPos);
				//Debug.Log ("\tNew Path Requested: Target is located at " + secondTarget.position);
				PathRequester.RequestPath (transform.position, secondTarget.position, OnPathFound);
				yield return new WaitForSeconds (timer);
			} else if (targetPos == secondTarget.position && transform.position == targetPos) {
				//If at final position then do something next // Debug.Log ("\tTarget Position Reached");
				yield break;
			}

			yield return null;
		}*/
		//update to check for pathing requirements
		yield return null;
	}

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
			Debug.Log("Path Successfully Found!");
            path = newPath;
			//Debug.Log ("Coroutine 'FollowPath' stopped!");
			StopCoroutine("FollowPath");
			//Debug.Log ("Coroutine 'FollowPath' started");
            StartCoroutine("FollowPath");
		}
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        
		while (true)
        {
			if (Vector3.Distance(transform.position,currentWaypoint) < 0.1f) // if distance from next waypoint from this objects position is less than 0.1f
            {
                targetIndex++; //increment targetIndex
                if (targetIndex >= path.Length)//if the targetIndex exceeds the length of the path array
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];

            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
			Debug.Log ("Path Completed!");
            yield return null;
        }

    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one * 0.5f);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

}
