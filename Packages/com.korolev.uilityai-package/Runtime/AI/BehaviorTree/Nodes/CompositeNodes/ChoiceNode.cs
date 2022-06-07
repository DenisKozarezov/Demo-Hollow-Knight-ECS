/*******************************************
 * Created by Pavel Korolev
 * Last Modified 19.04.2022
 *******************************************/
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree.Nodes.CompositeNodes
{
    public class ChoiceNode : CompositeNode
    {
        [HideInInspector] private bool _existZeroParameter = false;
        [HideInInspector] protected Node actualNode;
        [HideInInspector] protected float maxCost = 0;
        [HideInInspector] public List<ParameterNode> ParametersList = new List<ParameterNode>();
        
        public override void OnStart() { }
        public override void OnStop() { }
        public override State OnUpdate()
        {
            _existZeroParameter = false;
            maxCost = 0;
            actualNode = null;
            
            if (ChildNodes.Count > 0)
            {
                foreach (var node in ChildNodes)
                {
                    _existZeroParameter = false;
                    float currentCost = 0;
                    if (ParametersList.Count > 0)
                    {
                        foreach (var parameter in ParametersList)
                        {
                            var cost = node.Cost(parameter);
                            if (cost == 0 || cost < 0.001f)
                                _existZeroParameter = true;
                            currentCost += cost ;
                        }
                    }
                    else
                        currentCost = node.Cost();
                    
                    if (_existZeroParameter)
                        currentCost = 0;
                    
                    if (currentCost > maxCost && currentCost > 0) {
                        actualNode = node;
                        maxCost = currentCost;
                    }
                }
                
                if(actualNode != null)
                {
                    BehaviorTreeRef.SetCurrentNode(actualNode);
                    return State.Success;
                }
            }
            return State.Success;
        }    
    }   
}