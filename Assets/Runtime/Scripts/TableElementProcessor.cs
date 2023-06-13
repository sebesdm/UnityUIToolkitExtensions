using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CoupleHunerdGames.UnityUIToolkitExtensions
{
    public class TableElementProcessor : MonoBehaviour
    {
        void Start()
        {
            var documents = FindObjectsOfType<UIDocument>();
            foreach (UIDocument document in documents)
            {
                tableElements.AddRange(GetAllTableElements(document.rootVisualElement));
            }
        }

        void Update()
        {
            foreach (TableElement tableElement in tableElements)
            {
                tableElement.RecalculateColumnWidths();
            }
        }

        private List<TableElement> GetAllTableElements(VisualElement parent)
        {
            List<TableElement> tableElements = new List<TableElement>();
            var elements = parent.Query<TableElement>().ToList();

            foreach (var element in elements)
            {
                tableElements.Add(element);
            }

            return tableElements;
        }

        private List<TableElement> tableElements = new List<TableElement>();
    }
}
