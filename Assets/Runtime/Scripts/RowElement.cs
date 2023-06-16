using System.Collections.Generic;
using UnityEngine.UIElements;

namespace CoupleHunerdGames.UnityUIToolkitExtensions
{
    public class RowElement : VisualElement
    {
        public void SetValues(List<string> values)
        {
            foreach (string value in values)
            {
                VisualElement tableCell = new VisualElement();
                tableCell.AddToClassList("table-cell");
                Label label = new Label(value);
                tableCell.Add(label);
                Add(tableCell);
            }
        }

        public new class UxmlFactory : UxmlFactory<RowElement, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }
    }
}