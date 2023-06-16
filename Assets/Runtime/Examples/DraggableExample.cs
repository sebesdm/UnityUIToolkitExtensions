using CoupleHunerdGames.UnityUIToolkitExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace CoupleHunerdGames.Examples
{
    public class DraggableExample : MonoBehaviour
    {
        public void Awake()
        {
            VisualElement documentRoot = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;

            DraggableElement draggableGreen = documentRoot.Q<DraggableElement>(name: "DraggableGreen");
            manipulatorGreen = new DragAndDropManipulator(documentRoot, draggableGreen, draggableTargetClass: "green-draggable-target-type");

            DraggableElement draggableRed = documentRoot.Q<DraggableElement>(name: "DraggableRed");
            manipulatorRed = new DragAndDropManipulator(documentRoot, draggableRed, draggableTargetClass: "red-draggable-target-type");
        }

        private DragAndDropManipulator manipulatorGreen;
        private DragAndDropManipulator manipulatorRed;
    }
}