using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CoupleHunerdGames.UnityUIToolkitExtensions
{
    // Basis for this class provided by Unity UI Toolkit documentation: https://docs.unity3d.com/Manual/UIE-create-drag-and-drop-ui.html
    public class DragAndDropManipulator : PointerManipulator
    {
        public event DraggableOverDraggableTargetEventHandler OnDraggableOverDraggableTarget;
        public event DraggableDroppedInDraggableTargetEventHandler OnDraggableDroppedInDraggableTarget;

        public DragAndDropManipulator(VisualElement documentRoot, DraggableElement target, string draggableTargetClass)
        {
            this.target = target;
            root = documentRoot;
            this.draggableTargetClass = draggableTargetClass;
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
            target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
            target.RegisterCallback<PointerUpEvent>(PointerUpHandler);
            target.RegisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
            target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler);
            target.UnregisterCallback<PointerUpEvent>(PointerUpHandler);
            target.UnregisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
        }

        private DraggableElement draggableTarget => (DraggableElement)target;
        private Vector2 targetStartLayoutPosition { get; set; }
        private Vector2 targetStartTransformPosition { get; set; }
        private Vector3 pointerStartPosition { get; set; }
        private bool enabled { get; set; }
        private VisualElement root { get; }

        private void PointerDownHandler(PointerDownEvent evt)
        {
            targetStartLayoutPosition = target.layout.position;
            targetStartTransformPosition = target.transform.position;
            pointerStartPosition = evt.position;
            target.CapturePointer(evt.pointerId);
            enabled = true;
            draggableTarget.StartDrag();
        }

        private Vector2 lastMovePos;

        private void PointerMoveHandler(PointerMoveEvent evt)
        {
            if (enabled && target.HasPointerCapture(evt.pointerId))
            {
                Vector3 pointerDelta = evt.position - pointerStartPosition;
                lastMovePos = new Vector2(targetStartTransformPosition.x + pointerDelta.x, targetStartTransformPosition.y + pointerDelta.y);
                target.transform.position = lastMovePos;

                DraggableTargetElement closestOverlappingSlot = GetClosestOverlappingSlot();
                if (closestOverlappingSlot != null)
                {
                    OnDraggableOverDraggableTarget?.Invoke(new DragEventArgs(target, closestOverlappingSlot));
                }
            }
        }

        private void PointerUpHandler(PointerUpEvent evt)
        {
            if (enabled && target.HasPointerCapture(evt.pointerId))
            {
                target.ReleasePointer(evt.pointerId);
            }
        }

        private void PointerCaptureOutHandler(PointerCaptureOutEvent evt)
        {
            if (!enabled) return;

            DraggableTargetElement closestOverlappingSlot = GetClosestOverlappingSlot();

            if (closestOverlappingSlot != null)
            {
                target.transform.position = closestOverlappingSlot.layout.position - targetStartLayoutPosition;
                draggableTarget.DropOnTarget(closestOverlappingSlot);
                closestOverlappingSlot.AddElement(draggableTarget);
                OnDraggableDroppedInDraggableTarget?.Invoke(new DragEventArgs(target, closestOverlappingSlot));
            }
            else
            {
                draggableTarget.CancelDrag();
                target.transform.position = targetStartTransformPosition;
            }

            enabled = false;
        }

        private DraggableTargetElement GetClosestOverlappingSlot()
        {
            UQueryBuilder<DraggableTargetElement> allSlots = root.Query<DraggableTargetElement>(className: draggableTargetClass);
            UQueryBuilder<DraggableTargetElement> overlappingSlots = allSlots.Where(OverlapsTarget);
            DraggableTargetElement closestOverlappingSlot = FindClosestSlot(overlappingSlots);
            return closestOverlappingSlot;
        }

        private bool OverlapsTarget(VisualElement slot)
        {
            return target.worldBound.Overlaps(slot.worldBound);
        }

        private DraggableTargetElement FindClosestSlot(UQueryBuilder<DraggableTargetElement> slots)
        {
            List<DraggableTargetElement> slotsList = slots.ToList();
            float bestDistanceSq = float.MaxValue;
            DraggableTargetElement closest = null;
            foreach (DraggableTargetElement slot in slotsList)
            {
                Vector3 displacement = RootSpaceOfSlot(slot) - target.transform.position;
                float distanceSq = displacement.sqrMagnitude;
                if (distanceSq < bestDistanceSq)
                {
                    bestDistanceSq = distanceSq;
                    closest = slot;
                }
            }

            return closest;
        }

        private Vector3 RootSpaceOfSlot(VisualElement slot)
        {
            Vector2 slotWorldSpace = slot.parent.LocalToWorld(slot.layout.position);
            return root.WorldToLocal(slotWorldSpace);
        }

        private string draggableTargetClass;
        public delegate void DraggableOverDraggableTargetEventHandler(DragEventArgs eventArgs);
        public delegate void DraggableDroppedInDraggableTargetEventHandler(DragEventArgs eventArgs);
    }
}