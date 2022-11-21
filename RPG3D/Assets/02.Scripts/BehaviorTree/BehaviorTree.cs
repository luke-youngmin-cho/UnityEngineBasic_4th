using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace BT
{
    public enum Status
    {
        Success,
        Failure,
        Running
    }
    
    public class BTTester
    {
        void Test()
        {
            new BehaviorTree()
                .StartBuild()
                    .Sequence()
                        .Condition(() => true)
                            .Execution(() => Status.Success)
                        .Execution(() => Status.Success)
                        .ExitCurrentComposite()
                    .Selector()
                        .Execution(() => Status.Success)
                        .Sequence()
                            .Condition(() => true)
                                .Condition(() => true)
                                    .Execution(() => Status.Success)
                            .Execution(() => Status.Success)
                            .ExitCurrentComposite()
                        .ExitCurrentComposite();
        }
    }



    public class BehaviorTree
    {
        private Root _root = new Root();
        public void Tick()
        {
            _root.Invoke();
        }

        private Stack<Composite> _compositeStack = new Stack<Composite>();
        private Behavior _current;
        private Behavior _tmp;
        public BehaviorTree StartBuild()
        {
            _current = _root;
            return this;
        }

        public BehaviorTree Sequence()
        {
            Sequence sequence = new Sequence();
            AttachAsChild(_current, sequence);
            _compositeStack.Push(sequence);
            _current = sequence;
            return this;
        }

        public BehaviorTree Selector()
        {
            Selector selector = new Selector();
            AttachAsChild(_current, selector);
            _compositeStack.Push(selector);
            _current = selector;
            return this;
        }

        public BehaviorTree RandomSelector()
        {
            RandomSelector randomSelector = new RandomSelector();
            AttachAsChild(_current, randomSelector);
            _compositeStack.Push(randomSelector);
            _current = randomSelector;
            return this;
        }

        public BehaviorTree Condition(Func<bool> condition)
        {
            Condition tmpCondition = new Condition(condition);
            AttachAsChild(_current, tmpCondition);
            _current = tmpCondition;
            return this;
        }

        public BehaviorTree Execution(Func<Status> execute)
        {
            Execution execution = new Execution(execute);
            AttachAsChild(_current, execution);

            if (_compositeStack.Count > 0)
                _current = _compositeStack.Peek();
            else
                _current = _root;

            return this;
        }

        public BehaviorTree RunAndSleepRandom(float minTime, float maxTime)
        {
            RunAndSleepRandom runAndSleepRandom = new RunAndSleepRandom(minTime, maxTime);
            AttachAsChild(_current, runAndSleepRandom);
            _current = runAndSleepRandom;
            return this;
        }

        private void AttachAsChild(Behavior parent, Behavior child)
        {
            if (parent is Composite)
                (parent as Composite).AddChild(child);
            else if (parent is IChild)
                (parent as IChild).SetChild(child);
            else
                throw new Exception("[BehaviorTree] : AttachAsChild() - Parent does not have a child");
        }

        public BehaviorTree ExitCurrentComposite()
        {
            if (_compositeStack.Count > 1)
            {
                _compositeStack.Pop();
                _current = _compositeStack.Peek();
            }
            else if (_compositeStack.Count == 1)
            {
                _compositeStack.Pop();
                _current = _root;
            }
            else
                throw new Exception("[BehaviorTree] : Cannot exit last composite");

            return this;
        }
    }   

    #region Behaviors
    public interface IChild
    {
        public Behavior Child { get; }
        public void SetChild(Behavior child);
    }

    public abstract class Behavior
    {
        public abstract Status Invoke(out Behavior leaf);
    }

    public class Root : Behavior, IChild
    {
        public Behavior Child { get; private set; }
        public Behavior RunningLeaf { get; private set; }
        private Status _tmpResult;

        public void SetChild(Behavior child) => Child = child;

        public Status Invoke()
        {
            _tmpResult = Invoke(out Behavior leaf);
            if (_tmpResult == Status.Running)
                RunningLeaf = leaf;
            else
                RunningLeaf = null;
            return _tmpResult;
        }

        public override Status Invoke(out Behavior leaf)
        {
            return Child.Invoke(out leaf);
        }

        public Status InvokeRunningLeaf()
        {
            _tmpResult = RunningLeaf.Invoke(out Behavior leaf);
            if (_tmpResult == Status.Running)
                RunningLeaf = leaf;
            else
                RunningLeaf = null;
            return _tmpResult;
        }
    }

    public class Condition : Behavior, IChild
    {
        public Behavior Child { get; private set; }
        private Func<bool> _condition;

        public Condition(Func<bool> condition)
        {
            _condition = condition;
        }

        public void SetChild(Behavior child)
        {
            Child = child;
        }
        public void SetCondition(Func<bool> condition) => _condition = condition;

        public override Status Invoke(out Behavior leaf)
        {
            leaf = this;
            if (_condition.Invoke())
                return Child.Invoke(out leaf);
            else
                return Status.Failure;
        }

    }

    public abstract class Composite : Behavior
    {
        protected List<Behavior> _children = new List<Behavior>();
        public IEnumerable<Behavior> Children => _children;
        public void AddChild(Behavior child) => _children.Add(child);
        public void RemoveChild(Behavior child) => _children.Remove(child);
        public void ClearChild() => _children.Clear();
    }

    public class Sequence : Composite
    {
        private Status _tmpResult;

        public override Status Invoke(out Behavior leaf)
        {
            leaf = this;
            foreach (Behavior child in _children)
            {
                _tmpResult = child.Invoke(out leaf);
                if (_tmpResult != Status.Success)
                {
                    leaf = child;
                    return _tmpResult;
                }
            }
            return Status.Success;
        }
    }

    public class Filter : Sequence
    {
        public void AddCondition(Condition condition) => _children.Insert(0, condition);
    }

    public class Selector : Composite
    {
        private Status _tmpResult;

        public override Status Invoke(out Behavior leaf)
        {
            leaf = this;
            foreach (Behavior child in _children)
            {
                _tmpResult = child.Invoke(out leaf);
                if (_tmpResult != Status.Failure)
                {
                    leaf = child;
                    return _tmpResult;
                }
            }
            return Status.Failure;
        }
    }

    public class RandomSelector : Composite
    {
        private Status _tmpResult;

        public override Status Invoke(out Behavior leaf)
        {
            leaf = this;
            foreach (Behavior child in _children.OrderBy(c => UnityEngine.Random.Range(0, _children.Count)))
            {
                _tmpResult = child.Invoke(out leaf);
                if (_tmpResult != Status.Failure)
                {
                    leaf = child;
                    return _tmpResult;
                }
            }
            return Status.Failure;
        }
    }

    public class Pararell : Composite
    {
        public enum Policy
        {
            RequireOne,
            RequireAll
        }
        private Policy _successPolicy;
        private Policy _failurePolicy;
        private Status _tmpResult;

        public Pararell(Policy successPolicy, Policy failurePolicy)
        {
            _successPolicy = successPolicy;
            _failurePolicy = failurePolicy;
        }

        public override Status Invoke(out Behavior leaf)
        {
            leaf = this;
            int successCount = 0;
            int failureCount = 0;

            foreach (Behavior child in _children)
            {
                _tmpResult = child.Invoke(out leaf);

                if (_tmpResult == Status.Success)
                {
                    successCount++;
                }
                else if (_tmpResult == Status.Failure)
                {
                    failureCount++;
                }
                else if (_tmpResult == Status.Running)
                {
                    return _tmpResult;
                }
            }

            if (_successPolicy == Policy.RequireOne && successCount >= 1)
                return Status.Success;
            else if (_successPolicy == Policy.RequireAll && successCount >= _children.Count)
                return Status.Success;
            else if (_failurePolicy == Policy.RequireOne && failureCount >= 1)
                return Status.Failure;
            else if (_failurePolicy == Policy.RequireAll && failureCount >= _children.Count)
                return Status.Failure;

            throw new Exception("[BehaviorTree-Pararell] : Invalid policy.");
        }
    }

    public class Monitor : Pararell
    {
        public Monitor(Policy successPolicy, Policy failurePolicy) : base(successPolicy, failurePolicy)
        {
        }

        public void AddCondition(Condition condition) => _children.Insert(0, condition);
    }

    public class Execution : Behavior
    {
        private Func<Status> _execute;

        public Execution(Func<Status> execute)
        {
            _execute = execute;
        }

        public void SetExecute(Func<Status> execute)
        {
            _execute = execute;
        }

        public override Status Invoke(out Behavior leaf)
        {
            leaf = this;
            return _execute.Invoke();
        }
    }

    public abstract class Decorator : Behavior, IChild
    {
        public Behavior Child { get; private set; }

        public void SetChild(Behavior child)
        {
            Child = child;
        }
        public override Status Invoke(out Behavior leaf)
        {
            return Decorate(Child.Invoke(out leaf), out leaf);
        }

        public abstract Status Decorate(Status status, out Behavior leaf);        
    }

    public class RunAndSleepRandom : Decorator
    {
        private float _timeMin, _timeMax;
        private float _time;
        private float _timeMark;
        private int _state = 0;
        private Status _tmpResult;

        public RunAndSleepRandom(float timeMin, float timeMax)
        {
            _timeMin = timeMin;
            _timeMax = timeMax;
        }

        public override Status Invoke(out Behavior leaf)
        {
            if (_state == 0)
                return base.Invoke(out leaf);
            else
                return Decorate(Status.Running, out leaf);
        }

        public override Status Decorate(Status status, out Behavior leaf)
        {
            _tmpResult = Status.Running;
            leaf = this;
            switch (_state)
            {
                case 0:
                    {
                        _timeMark = Time.time;
                        _time = UnityEngine.Random.Range(_timeMin, _timeMax);
                        _state++;
                    }
                    break;
                case 1:
                    {
                        if (Time.time - _timeMark > _time)
                            _state++;
                    }
                    break;
                case 2:
                    {
                        _tmpResult = Status.Success;
                        _state = 0;
                    }
                    break;
                default:
                    break;
            }
            return _tmpResult;
        }
    }

    public class RepeatForSeconds : Decorator
    {
        private float _time;
        private float _timeMark;
        private int _state = 0;
        private Status _tmpResult;
        public RepeatForSeconds(float time)
        {
            _time = time;
        }

        public override Status Decorate(Status status, out Behavior leaf)
        {
            _tmpResult = Status.Running;
            leaf = Child;
            switch (_state)
            {
                case 0:
                    {
                        Child.Invoke(out leaf);
                        _timeMark = Time.time;
                        _state++;
                    }
                    break;
                case 1:
                    {
                        Child.Invoke(out leaf);
                        if (Time.time - _timeMark > _time)
                            _state++;
                    }
                    break;
                case 2:
                    {
                        _tmpResult = Child.Invoke(out leaf);
                        _state = 0;
                    }
                    break;
                default:
                    break;
            }
            return _tmpResult;
        }
    }

    public class RepeatForRandomSeconds : Decorator
    {
        private float _timeMin, _timeMax;
        private float _time;
        private float _timeMark;
        private int _state = 0;
        private Status _tmpResult;
        public RepeatForRandomSeconds(float timeMin, float timeMax)
        {
            _timeMin = timeMin;
            _timeMax = timeMax;
        }

        public override Status Decorate(Status status, out Behavior leaf)
        {
            _tmpResult = Status.Running;
            leaf = Child;
            switch (_state)
            {
                case 0:
                    {
                        Child.Invoke(out leaf);
                        _timeMark = Time.time;
                        _time = UnityEngine.Random.Range(_timeMin, _timeMax);
                        _state++;
                    }
                    break;
                case 1:
                    {
                        Child.Invoke(out leaf);
                        if (Time.time - _timeMark > _time)
                            _state++;
                    }
                    break;
                case 2:
                    {
                        _tmpResult = Child.Invoke(out leaf);
                        _state = 0;
                    }
                    break;
                default:
                    break;
            }
            return _tmpResult;
        }
    }

    public class Repeat : Decorator
    {
        private int _count;

        public Repeat(int count)
        {
            _count = count;
        }

        public override Status Decorate(Status status, out Behavior leaf)
        {
            leaf = Child;

            if (status == Status.Failure)
                return Status.Failure;

            _count--;
            while (_count > 0)
            {
                if (Child.Invoke(out leaf) == Status.Failure)
                    return Status.Failure;

                _count--;
            }
            return Status.Success;
        }
    }

    public class Inverter : Decorator
    {
        public override Status Decorate(Status status, out Behavior leaf)
        {
            leaf = Child;
            if (status == Status.Failure)
                return Status.Success;
            else if (status == Status.Success)
                return Status.Failure;
            else
                return Status.Running;
        }
    }
    #endregion
}
