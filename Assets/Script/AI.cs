using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
	public NavMeshAgent Agent;
	public Transform Target;
	public int col, row;
	void Update()
	{
		FindTarget();
		Agent.SetDestination(Target.position);
		Agent.isStopped = true;
		OnDrawGizmosSelected();
	}

	public void FindTarget()
	{
		var Find = GameObject.Find(row + "," + col);
		var findPosition = Find.transform.position;
		var targetPosition = new Vector3(findPosition.x, Target.position.y, findPosition.z);
		Target.position = targetPosition;
	}

	public void OnDrawGizmosSelected()
	{
		var nav = GetComponent<NavMeshAgent>();
		if (nav == null || nav.path == null)
			return;
		var line = this.GetComponent<LineRenderer>();
		if (line == null)
		{
			line = this.gameObject.AddComponent<LineRenderer>();
			line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.yellow };
			line.startWidth = 0.1f;
			line.endWidth =  0.1f;
			line.startColor = Color.yellow;
			line.endColor = Color.yellow;
		}
		var path = nav.path;
		line.positionCount = path.corners.Length;
		for (int i = 0; i < path.corners.Length; i++)
		{
			line.SetPosition(i, path.corners[i]);
		}
	}
}
