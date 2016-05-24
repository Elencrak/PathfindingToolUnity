using UnityEngine;
using System.Collections;

public class PierreBehaviourTree : MonoBehaviour {

    PierreComposite root;

	// Use this for initialization
	void Start ()
    {
        InitTree();
	}

    void InitTree()
    {
        root = new PierreSequence();

        PierreSelecteur pS1 = new PierreSelecteur();

        PierreGetInputTask pt = new PierreGetInputTask(KeyCode.A);

        PierrePrintTask pt2 = new PierrePrintTask("Selecteur print");

        pS1.AddNode(pt);
        pS1.AddNode(pt2);

        PierreSequence pS2 = new PierreSequence();

        PierreGetInputTask pt3 = new PierreGetInputTask(KeyCode.E);

        PierrePrintTask pt4 = new PierrePrintTask("Sequence print");

        pS2.AddNode(pt3);
        pS2.AddNode(pt4);


        root.AddNode(pS2);
        root.AddNode(pS1);
    }

	// Update is called once per frame
	void Update () {
        root.Execute();
	}
}
