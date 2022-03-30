using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EraserLayout : LayoutGroup
{
    public int rows;
    public int columns;

    public Vector2 spacing;
    public float widthPaddings = 50;

    public float preferredTopPadding;

    public Vector3 eraserSize;
        
    public override void CalculateLayoutInputVertical()
    {
        if(rows == 0 || columns == 0)
        {
            rows = 4;
            columns = 5;
        }
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float parentHeight = GetComponent<RectTransform>().rect.height;

        float eraserHeight = (parentHeight-2 * preferredTopPadding - spacing.y * (rows - 1)) / rows;

        float eraserWidth = (parentWidth - widthPaddings) / columns /*eraserHeight*/;

        if (eraserWidth * columns + spacing.x * (columns - 1) > parentWidth)
        {
            eraserWidth = (parentWidth - 2*preferredTopPadding - (columns - 1) * spacing.x)/ columns;

        }
        eraserSize = new Vector3(eraserWidth, eraserHeight, 0f);

        padding.left = Mathf.FloorToInt((parentWidth - columns * eraserWidth - spacing.x * (columns - 1)) / 2);
        padding.top = Mathf.FloorToInt((parentHeight - rows * eraserHeight - spacing.y * (rows - 1)) / 2);
        padding.bottom = padding.top;
        for (int i = 0; i < rectChildren.Count; i++)
        {
            int rowCount = i / columns;
            int columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = padding.left + eraserSize.x * columnCount + spacing.x * (columnCount);
            var yPos = padding.top + eraserSize.y * rowCount + spacing.y * (rowCount);

            SetChildAlongAxis(item, 0, xPos, eraserSize.x);
            SetChildAlongAxis(item, 1, yPos, eraserSize.y);
        }
    }

    public override void SetLayoutHorizontal()
    {
        return;
    }

    public override void SetLayoutVertical()
    {
        return;
    }

}
