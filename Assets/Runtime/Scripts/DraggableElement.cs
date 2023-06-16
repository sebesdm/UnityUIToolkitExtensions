using System;
using UnityEngine.UIElements;

namespace CoupleHunerdGames.UnityUIToolkitExtensions
{
    public class DraggableElement : VisualElement
    {
        public event DroppedOnTargetEventHandler OnDroppedOnTarget;
        public event DragStartedEventHandler OnDragStarted;
        public event DragStoppedEventHandler OnDragStopped;

        public void StartDrag()
        {
            OnDragStarted?.Invoke(EventArgs.Empty);
        }

        public void CancelDrag()
        {
            OnDragStopped?.Invoke(EventArgs.Empty);
        }

        public void DropOnTarget(DraggableTargetElement element)
        {
            OnDroppedOnTarget?.Invoke(new DragEventArgs(this, element));
        }

        public new class UxmlFactory : UxmlFactory<DraggableElement, UxmlTraits>
        {
        }

        public class UxmlTraits : VisualElement.UxmlTraits
        {
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
            }
        }

        public delegate void DroppedOnTargetEventHandler(DragEventArgs eventArgs);
        public delegate void DragStartedEventHandler(EventArgs eventArgs);
        public delegate void DragStoppedEventHandler(EventArgs eventArgs);
    }
}