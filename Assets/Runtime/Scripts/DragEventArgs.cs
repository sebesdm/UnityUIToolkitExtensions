using System;
using UnityEngine.UIElements;

namespace CoupleHunerdGames.UnityUIToolkitExtensions
{
    public class DragEventArgs : EventArgs
    {
        public DragEventArgs(VisualElement draggableElement, DraggableTargetElement draggableTargetElement)
        {
            DraggableElement = draggableElement;
            DraggableTargetElement = draggableTargetElement;
        }

        public VisualElement DraggableElement { get; private set; }
        public DraggableTargetElement DraggableTargetElement { get; private set; }
    }
}