using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviorTreeIARobin;

namespace BehaviorTreeIARobin
{
    /*
    public class Node
    {

        public string _name;

        public delegate bool DoSomething();

        public DoSomething _action;

        public Node(string name)
        {
            _name = name;
        }

        public Node(DoSomething action, string name = "") : this(name)
        {
            if (action != null)
            {
                _action = action;
            }
        }

        public virtual bool Execute()
        {
            if (_action != null)
            {
                if (_action())
                {
                    Debug.Log(_name + " : true");
                    return true;
                }
                else
                {
                    Debug.Log(_name + " : false");
                    return false;
                }
            }
            throw new System.Exception("Il manque une action dans le node : " + _name);
        }

    }

    public class Composite : Node
    {

        protected List<Node> _nodes;

        public Composite(string name) : this(null, name)
        {
            _nodes = new List<Node>();
        }

        public Composite(DoSomething action, string name = "") : base(action, name)
        {
            _nodes = new List<Node>();
        }

        public override bool Execute()
        {
            return base.Execute();
        }

        public void Add(Node node)
        {
            if (_nodes == null)
            {
                _nodes = new List<Node>();
            }

            if (node != null && !_nodes.Contains(node))
            {
                _nodes.Add(node);
            }
        }

    }

    public class Task : Node
    {

        public Task() : base("")
        {

        }

        public Task(DoSomething action, string name = "") : base(action, name)
        {

        }

    }

    public class Condition : Node
    {

        public Condition() : base("")
        {

        }

        public Condition(DoSomething action, string name = "") : base(action, name)
        {

        }

    }

    public class Sequence : Composite
    {
        public Sequence(string name) : base(name)
        {
            _action = new DoSomething(() =>
            {
                foreach (Node node in _nodes)
                {
                    if (!node.Execute())
                    {
                        return false;
                    }
                }
                return true;
            });
        }

        public override bool Execute()
        {
            return base.Execute();
        }

    }

    public class Selector : Composite
    {

        public Selector(string name) : base(name)
        {
            _action = new DoSomething(() =>
            {
                foreach (Node node in _nodes)
                {
                    if (node.Execute())
                    {
                        return true;
                    }
                }
                return false;
            });
        }

        public override bool Execute()
        {
            return base.Execute();
        }

    }

    public class Inverter : Node
    {
        public Node _inverse = null;

        public Inverter(string name) : base(name)
        {
            _action = new DoSomething(() =>
            {
                if (_inverse != null)
                {
                    return !_inverse.Execute();
                }
                throw new System.Exception("Il manque une node à inverser : " + _name);
            });
        }

        public override bool Execute()
        {
            return base.Execute();
        }
    }

}


public class TestSelector : MonoBehaviour
{

    public bool _isActive;

    public bool _isUp;
    public bool _isLow;

    Sequence _sequence;

    void Start()
    {
        // SET SEQUENCE

        _sequence = new Sequence("Main Sequence");

        _sequence.Add(new Condition(() => {
            return _isActive;
        }, "Is Active"));

        // SET SUB SELECTOR

        Selector selec = new Selector("Sub Sequence 1");
        
        Sequence seq = new Sequence("Sub Min Sequence 1");

        seq.Add(new Condition(() =>
        {
            return _isLow;
        }, "Is Low"));

        seq.Add(new Task(() =>
        {
            return true;
        }, "ACTION Sequence 1"));

        selec.Add(seq);

        // SET SUB SEQUENCE

        Sequence seq2 = new Sequence("Sub Max Sequence 1");

        seq2.Add(new Condition(() =>
        {
            return _isUp;
        }, "Is Up"));

        seq2.Add(new Task(() =>
        {
            return true;
        }, "ACTION Sequence 2"));

        selec.Add(seq2);

        _sequence.Add(selec);
    }

    void Update()
    {
        var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);
        _sequence.Execute();
    }
    */
}