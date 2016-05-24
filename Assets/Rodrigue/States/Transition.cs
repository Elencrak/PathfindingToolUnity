using UnityEngine;
using System.Collections;
namespace Rodrigue
{
    public delegate bool check();
    public class Transition
    {
        public class Not
        {
            protected check _check;

            public static Not getInstance(check check)
            {
                return new Not(check);
            }

            public Not(check check)
            {
                _check = check;
            }

            public bool check()
            {
                return !_check();
            }
        }

        public class AndCondition
        {
            protected check[] _checks;

            public static AndCondition getInstance(check check, params check[] checks)
            {
                return new AndCondition(check, checks);
            }

            public AndCondition(check check, params check[] checks)
            {
                check[] checkArray = new check[checks.Length + 1];
                checkArray[0]=check;
                checks.CopyTo(checkArray, 1);
                _checks = checkArray;
            }

            public bool check()
            {
                foreach (check check in _checks)
                    if (!check())
                        return false;
                return true;
            }
        }

        public class OrCondition
        {
            protected check[] _checks;

            public static OrCondition getInstance(check check, params check[] checks)
            {
                return new OrCondition(check, checks);
            }

            public OrCondition(check check, params check[] checks)
            {
                check[] checkArray = new check[checks.Length + 1];
                checkArray[0] = check;
                checks.CopyTo(checkArray, 1);
                _checks = checkArray;
            }

            public bool check()
            {
                foreach (check check in _checks)
                    if (check())
                        return true;
                return false;
            }
        }


        public check _checkDelegate;
        public State nextState;

        public Transition(check myDelegate, State _nextState)
        {
            _checkDelegate = myDelegate;
            nextState = _nextState;
        }

        public bool Check()
        {
            return _checkDelegate();
        }
    }
}

