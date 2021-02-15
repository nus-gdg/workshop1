using Fungus;
using UnityEngine;

namespace Core.Managers
{
    public class StoryManager : MonoBehaviour
    {
        // TODO: Add listener for dialogues events
        // TODO: Add handling of dialogue ui

        [SerializeField]
        private Flowchart progression;
        public Flowchart Progression => progression;

        private void Awake()
        {
            progression = GetComponent<Flowchart>();
        }
    }
}
