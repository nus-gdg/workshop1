using UnityEngine;
#if UNITY_EDITOR
using XNodeEditor;
#endif

namespace Common.Logic
{
    [DisallowMultipleNodes]
    public class RootNode : BehaviourTreeNode
    {
        [Output(connectionType = ConnectionType.Override)]
        [SerializeField]
        private BehaviourTreeNode child;

        /// <summary>
        /// Starts the evaluation of the behaviour tree for the given controller.
        /// <br/>
        /// If the root node completed or failed in the previous tick,
        /// resets the node status of the controller before ticking.
        /// </summary>
        public override Status Evaluate(BehaviourTreeController controller)
        {
            // Return completed when there is no child.
            if (child == null)
            {
                return Status.Completed;
            }
            
            // Reset if the last tick completed or failed.
            if (!child.IsStatus(controller, Status.Running))
            {
                child.ResetController(controller);
            }

            return child.Tick(controller);
        }

        public override void ResetController(BehaviourTreeController controller)
        {
            base.ResetController(controller);
            if (child == null)
            {
                return;
            }
            child.ResetController(controller);
        }
        
        public override void RemoveController(BehaviourTreeController controller)
        {
            base.RemoveController(controller);
            if (child == null)
            {
                return;
            }
            child.RemoveController(controller);
        }

        protected override void Serialize()
        {
            // Serialize child port
            var outputPort = GetOutputPort("child");
            child = GetConnectedNode(outputPort);
        }
    }

    #if UNITY_EDITOR
    [CustomNodeEditor(typeof(RootNode))]
    public class RootNodeEditor : BehaviourTreeNodeEditor
    {
        /// <summary> Draws standard field editors for all public fields </summary>
        public override void OnBodyGUI()
        {
            serializedObject.Update();
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("child"));
            DrawProperties();
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
