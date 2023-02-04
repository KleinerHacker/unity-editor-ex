using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UnityEditorEx.Editor.editor_ex.Scripts.Editor
{
    public abstract class TableReorderableList : ReorderableList
    {
        private float HeaderMarginLeft => draggable ? 15f : 0f;

        public IList<Column> Columns { get; } = new List<Column>();
        public float ColumnMargin { get; set; } = 5f;

        protected TableReorderableList(IList elements, Type elementType) : base(elements, elementType) =>
            Init();

        protected TableReorderableList(IList elements, Type elementType, bool draggable, bool displayHeader, bool displayAddButton, bool displayRemoveButton) : base(elements, elementType, draggable, displayHeader, displayAddButton, displayRemoveButton) =>
            Init();

        protected TableReorderableList(SerializedObject serializedObject, SerializedProperty elements) : base(serializedObject, elements) =>
            Init();

        protected TableReorderableList(SerializedObject serializedObject, SerializedProperty elements, bool draggable, bool displayHeader, bool displayAddButton, bool displayRemoveButton) : base(serializedObject, elements, draggable, displayHeader, displayAddButton, displayRemoveButton) =>
            Init();

        private void Init()
        {
            drawHeaderCallback += DrawHeaderCallback;
            drawElementCallback += DrawElementCallback;
        }

        private void DrawHeaderCallback(Rect rect)
        {
            var fixedSpace = Columns.OfType<FixedColumn>()
                .Sum(x => x.AbsoluteWidth) + Columns.OfType<FixedColumn>().Count() * ColumnMargin;
            var dynamicSpace = rect.width - HeaderMarginLeft - fixedSpace - Columns.OfType<FlexibleColumn>().Count() * ColumnMargin;

            var leftCounter = HeaderMarginLeft;
            foreach (var column in Columns)
            {
                var width = CalculateWidth(fixedSpace, dynamicSpace, column);

                GUI.Label(new Rect(rect.x + leftCounter, rect.y, width, rect.height), column.Header, EditorStyles.boldLabel);
                leftCounter += width + ColumnMargin;
            }
        }

        private void DrawElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            var fixedSpace = Columns.OfType<FixedColumn>()
                .Sum(x => x.AbsoluteWidth) + Columns.OfType<FixedColumn>().Count() * ColumnMargin;
            var dynamicSpace = rect.width - fixedSpace - Columns.OfType<FlexibleColumn>().Count() * ColumnMargin;

            var leftCounter = 0f;
            foreach (var column in Columns)
            {
                var width = CalculateWidth(fixedSpace, dynamicSpace, column);
                var height = column.MaxHeight < 0f ? rect.height : column.MaxHeight;

                column.ElementCallback(new Rect(rect.x + leftCounter, rect.y, width, height), i, isactive, isfocused);
                leftCounter += width + ColumnMargin;
            }
        }

        private float CalculateWidth(float fixedSpace, float dynamicSpace, Column column)
        {
            if (column is FixedColumn fixedColumn)
                return fixedColumn.AbsoluteWidth;
            if (column is FlexibleColumn flexibleColumn)
                if (flexibleColumn.PercentageWidth < 0f)
                    return dynamicSpace - Columns.OfType<FlexibleColumn>()
                        .Where(x => x.PercentageWidth >= 0f)
                        .Sum(x => x.PercentageWidth * dynamicSpace);
                else
                    return dynamicSpace * flexibleColumn.PercentageWidth;

            throw new NotImplementedException(column.GetType().FullName);
        }

        public abstract class Column
        {
            public GUIContent Header { get; set; }

            public string HeaderText
            {
                get => Header.text;
                set => Header = new GUIContent(value);
            }

            public ElementCallbackDelegate ElementCallback { get; set; }

            public float MaxHeight { get; set; } = -1f;
        }

        public sealed class FixedColumn : Column
        {
            public float AbsoluteWidth { get; set; } = 100f;
        }

        public sealed class FlexibleColumn : Column
        {
            public float PercentageWidth { get; set; } = -1f;
        }
    }
}