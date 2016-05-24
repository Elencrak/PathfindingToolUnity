using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CompositeMif : NodeMif 
{
	protected List<NodeMif> elemList = new List<NodeMif>();

	public void AddElem(NodeMif NM)
	{
		elemList.Add(NM);
	}

	public override bool Execute()
	{
		return false;
	}

	public void RemoveElem(NodeMif NM)
	{
		elemList.Remove(NM);
	}
}
