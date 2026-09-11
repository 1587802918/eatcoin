using Godot;
using System;

[Tool]

public partial class Background : Node2D
{
	public int GridWidth = 9;
	public int GridHeight = 16;
	public Vector2 CellSize = new Vector2(80f,80f);
	public Color LineColor = new Color(1f,1f,1f,0.4f);
	public float LineThickness = 2.0f;
	public override void _Draw(){
		float totalWidth = GridWidth * CellSize.X;
		float totalHeight = GridHeight * CellSize.Y;
		for(int i=0;i<=GridWidth;i++){
			float x = i * CellSize.X;
			Vector2 from = new Vector2(x,0);
			Vector2 to = new Vector2(x,totalHeight);
			DrawLine(from,to,LineColor,LineThickness);
		}
		for(int i=0;i<=GridHeight;i++){
			float y = i * CellSize.Y;
			Vector2 from = new Vector2(0,y);
			Vector2 to = new Vector2(totalWidth,y);
			DrawLine(from,to,LineColor,LineThickness);
		}
	}
}
