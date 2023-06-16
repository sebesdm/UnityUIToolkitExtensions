using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using CoupleHunerdGames.UnityUIToolkitExtensions;

namespace CoupleHunerdGames.Examples
{
    public class SampleTableGenerator : MonoBehaviour
    {
        void Start()
        {
            UIDocument doc = GameObject.Find("UIDocument").GetComponent<UIDocument>();

            var tables = GetAllTableElements(doc.rootVisualElement);

            foreach (TableElement table in tables)
            {
                List<string> headerValues = new List<string>() { "DESCRIPTION", "DATE", "AMOUNT", "DESCRIPTION", "DATE", "AMOUNT" };
                List<string> row1Values = new List<string>() { "1", "2", "3", "1", "2", "3" };
                List<string> row2Values = new List<string>() { "2022-01-01", "2022-01-012", "1", "1", "2", "3" };
                List<string> row3Values = new List<string>() { "3", "4", "5", "1", "2", "3" };
                List<string> row4Values = new List<string>() { "1", "2", "3", "1", "2", "3" };
                List<string> row5Values = new List<string>() { "2022-01-01", "2022-01-012", "1", "1", "2", "3" };
                List<string> row6Values = new List<string>() { "3", "4", "5", "1", "2", "3" };
                List<string> row7Values = new List<string>() { "1", "2", "3", "1", "2", "3" };
                List<string> row8Values = new List<string>() { "2022-01-01", "2022-01-012", "1", "1", "2", "3" };
                List<string> row9Values = new List<string>() { "3", "4", "5", "1", "2", "3" };

                RowElement header = new RowElement();
                RowElement row1 = new RowElement();
                RowElement row2 = new RowElement();
                RowElement row3 = new RowElement();
                RowElement row4 = new RowElement();
                RowElement row5 = new RowElement();
                RowElement row6 = new RowElement();
                RowElement row7 = new RowElement();
                RowElement row8 = new RowElement();
                RowElement row9 = new RowElement();

                header.SetValues(headerValues);
                row1.SetValues(row1Values);
                row2.SetValues(row2Values);
                row3.SetValues(row3Values);
                row4.SetValues(row4Values);
                row5.SetValues(row5Values);
                row6.SetValues(row6Values);
                row7.SetValues(row7Values);
                row8.SetValues(row8Values);
                row9.SetValues(row9Values);

                List<RowElement> rows = new List<RowElement>();
                rows.Add(row1);
                rows.Add(row2);
                rows.Add(row3);
                rows.Add(row4);
                rows.Add(row5);
                rows.Add(row6);
                rows.Add(row7);
                rows.Add(row8);
                rows.Add(row9);

                table.SetRows(header, rows);
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

        private TableElement table;
    }
}