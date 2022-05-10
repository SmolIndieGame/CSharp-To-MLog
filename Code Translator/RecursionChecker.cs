
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Transpiler
{
    internal class RecursionChecker
    {
        Dictionary<IMethodSymbol, HashSet<IMethodSymbol>> allCalls;
        HashSet<IMethodSymbol> secured;
        int count;

        public RecursionChecker()
        {
            allCalls = new Dictionary<IMethodSymbol, HashSet<IMethodSymbol>>();
        }

        public void Reset()
        {
            foreach (var item in allCalls.Values)
                item.Clear();
            allCalls.Clear();
        }

        public void AddCall(IMethodSymbol caller, IMethodSymbol callee)
        {
            if (!allCalls.TryGetValue(caller, out HashSet<IMethodSymbol> allCallsOfCaller))
            {
                allCallsOfCaller = new HashSet<IMethodSymbol>();
                allCalls.Add(caller, allCallsOfCaller);
            }
            if (allCallsOfCaller.Add(callee))
                count++;
        }

        public bool IsRecursive(IMethodSymbol entryPoint)
        {
            secured = new HashSet<IMethodSymbol>(count);
            HashSet<IMethodSymbol> path = new HashSet<IMethodSymbol>(count);
            return IsRecursive(ref path, entryPoint);
        }

        bool IsRecursive(ref HashSet<IMethodSymbol> path, IMethodSymbol caller)
        {
            if (secured.Contains(caller))
                return false;
            if (!allCalls.TryGetValue(caller, out HashSet<IMethodSymbol> calls))
            {
                secured.Add(caller);
                return false;
            }

            foreach (var callee in calls)
            {
                if (path.Contains(callee))
                    return true;
                path.Add(callee);
                if (IsRecursive(ref path, callee))
                    return true;
                path.Remove(callee);
            }
            secured.Add(caller);
            return false;
        }
    }
}
