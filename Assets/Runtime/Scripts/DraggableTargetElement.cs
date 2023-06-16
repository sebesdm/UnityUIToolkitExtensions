using UnityEngine.UIElements;

namespace CoupleHunerdGames.UnityUIToolkitExtensions
{
    public class DraggableTargetElement : VisualElement
    {
        public DraggableTargetElement()
        {
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