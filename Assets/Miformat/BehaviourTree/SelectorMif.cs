using UnityEngine;
using System.Collections;

public class SelectorMif : CompositeMif 
{
	public SelectorMif()
	{

	}

	public override bool Execute()
	{
		foreach (NodeMif NM in elemList)
		{
			if (NM.Execute ()) 
			{
				return true;                
			}
		}
		return false;
	}
}
