using UnityEngine.UIElements;

namespace CoupleHunerdGames.UnityUIToolkitExtensions
{
    public class DraggableTargetElement : VisualElement
    {
        private DraggableElement draggableElement;

        public void AddElement(DraggableElement draggableElement)
        {
            if (this.draggableElement == draggableElement) return;

            this.draggableElement = draggableElement;
            this.draggableElement.OnDroppedOnTarget += HandleDropOnTarget;
        }

        private void HandleDropOnTarget(DragEventArgs eventArgs)
        {
            if (eventArgs.DraggableTargetElement != this)
            {
                draggableElement.OnDroppedOnTarget -= HandleDropOnTarget;
                draggableElement = null;
            }
        }

        public new class UxmlFactory : UxmlFactory<DraggableTargetElement, UxmlTraits>
        {
        }

        public class UxmlTraits : VisualElement.UxmlTraits
        {
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
            }
        }
    }
}