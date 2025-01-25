using UnityEngine;

namespace Assets.Project.Scripts
{
    public interface IHighlightable
    {
        void StartHighlight(GameObject sender);
        void StopHighLight(GameObject sender);
    }
}