using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace CoupleHunerdGames.UnityUIToolkitExtensions
{
    class TableElement : VisualElement
    {
        public TableElement()
        {
            AddToClassList("table");
        }

        public void SetRows(RowElement headerRow, List<RowElement> valueRows)
        {
            RemoveCurrentRows();

            headerRow.AddToClassList("row");
            headerRow.AddToClassList("header-row");
            Insert(0, headerRow);

            for (int i = 1; i <= valueRows.Count; i++)
            {
                RowElement row = valueRows[i - 1];
                AddRowClasses(row, i, valueRows.Count);
                AddRowToTable(row);
            }

            recalculateTableDimensions = true;
        }

        private void AddRowToTable(RowElement row)
        {
            Children().ToList()[1].Add(row);
        }

        private void RemoveCurrentRows()
        {
            VisualElement headerRow = Children().Where(c => c.ClassListContains("header-row")).ToList().SingleOrDefault();
            headerRow?.parent.Remove(headerRow);

            List<VisualElement> children = Children().ToList();
            List<VisualElement> currentRows = children[^1].Children().Where(c => !c.ClassListContains("header-row") && c.ClassListContains("row")).ToList();
            foreach (VisualElement row in currentRows)
            {
                row.parent.Remove(row);
            }
        }

        private static void AddRowClasses(RowElement row, int tableRowIndex, int totalRowCount)
        {
            row.AddToClassList("row");

            if (tableRowIndex % 2 == 1)
            {
                row.AddToClassList("odd-row");
            }
            else
            {
                row.AddToClassList("even-row");
            }

            if (tableRowIndex == totalRowCount && tableRowIndex != 1)
            {
                row.AddToClassList("final-row");
            }
        }


        public void RecalculateColumnWidths()
        {
            var headerRow = Children().Where(c => c.ClassListContains("header-row")).ToList().Single();
            var tableRows = Children().ToList()[1].Children().Where(c => !c.ClassListContains("header-row") && c.ClassListContains("row")).ToList();
            var parentContainerWidth = headerRow.parent.parent.parent.layout.width;

            if (!recalculateTableDimensions || float.IsNaN(parentContainerWidth)) return;
            recalculateTableDimensions = false;

            List<float> columnMaxWidths = new List<float>();
            var allRows = new List<VisualElement>() { headerRow }.Concat(tableRows).ToList();
            foreach (VisualElement t in allRows)
            {
                var cells = t.Children().ToList();
                for (int j = 0; j < cells.Count; j++)
                {
                    var cell = cells[j];
                    if (j == columnMaxWidths.Count)
                    {
                        columnMaxWidths.Add(cell.layout.width);
                    }
                    else if (cell.layout.width > columnMaxWidths[j])
                    {
                        columnMaxWidths[j] = cell.layout.width;
                    }
                }
            }

            var totalCalcWidth = columnMaxWidths.Sum();
            var extraWidth = parentContainerWidth - totalCalcWidth;

            if (extraWidth > 0)
            {
                columnMaxWidths = columnMaxWidths.Select(cmw => cmw + extraWidth / columnMaxWidths.Count).ToList();
            }

            foreach (VisualElement t in allRows)
            {
                var cells = t.Children().ToList();
                for (int j = 0; j < cells.Count; j++)
                {
                    var cell = cells[j];

                    cell.style.width = new StyleLength
                    {
                        value = new Length(columnMaxWidths[j], LengthUnit.Pixel),
                    };
                }
            }

            style.width = new StyleLength
            {
                value = new Length(columnMaxWidths.Sum(), LengthUnit.Pixel),
            };
        }

        private bool recalculateTableDimensions = true;

        public new class UxmlFactory : UxmlFactory<TableElement, UxmlTraits>
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