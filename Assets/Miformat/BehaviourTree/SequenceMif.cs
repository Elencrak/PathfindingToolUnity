using UnityEngine;
using System.Collections;

public class SequenceMif : CompositeMif 
{
	public SequenceMif()
	{

	}

	public override bool Execute()
	{
		foreach (NodeMif NM in elemList)
		{
			if (!NM.Execute ()) 
			{
				return false;             
			}
		}
		return true;
	}
}
