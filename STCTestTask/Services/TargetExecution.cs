using STCTestTask.Models;

namespace STCTestTask.Services
{
    public class TargetExecution
    {
        private IEnumerable<Target> _targetList;

        public TargetExecution(IEnumerable<Target> targetList)
        {
            _targetList = targetList;
        }

        public void ProcessTargetExecution(string executionTargetName)
        {
            Target requestedTarget = GetTarget(executionTargetName);

            var targetHashSet = new HashSet<Target>();
            var targetDependencyQueue = new Queue<Target>();
            var targetStack = new Stack<Target>();

            targetDependencyQueue.Enqueue(requestedTarget);
            targetStack.Push(requestedTarget);

            var target = requestedTarget;

            var visitedHashSet = new HashSet<Target>();

            while (targetDependencyQueue.Count != 0)
            {
                target = targetDependencyQueue.Dequeue();

                if (visitedHashSet.Contains(target))
                {
                    throw new ArgumentException("В указанных в файле зависимостях обнаружена петля.");
                }
                else
                    visitedHashSet.Add(target);

                foreach (var dependency in target.Dependencies)
                {
                    var dependencyTarget = GetTarget(dependency);
                    if (!targetDependencyQueue.Contains(dependencyTarget))
                    {
                        targetDependencyQueue.Enqueue(dependencyTarget);
                        targetStack.Push(dependencyTarget);
                    }
                }
            }
            while (targetStack.Count != 0)
            {
                targetHashSet.Add(targetStack.Pop());
            }

            PrintResult(targetHashSet);
        }



        private void PrintResult(ICollection<Target> targets)
        {
            foreach (var item in targets)
            {
                Console.WriteLine(item.Name);
                foreach (var action in item.Actions)
                {
                    Console.WriteLine(" " + action);
                }
            }
        }

        private Target GetTarget(string targetName)
        {
            var foundTarget = _targetList.FirstOrDefault(x => x.Name == targetName);
            if (foundTarget == null)
            {
                throw new ArgumentException($"Задача {targetName} отсутствует в файле входных данных.");
            }
            return foundTarget;
        }
    }
}
