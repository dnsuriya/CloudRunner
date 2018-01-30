using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
	
	private LineRenderer line;

	private Vector2 mousePos;
	private Vector2 start;
	private Vector2 end;
	public int m_chalkSize = 20;

    private Slider m_slider;

    public Material material;
	public PhysicsMaterial2D m_physicMat;
	private int currLines = 0;
	private List<Vector2> pointsList;
	private bool isMousePressed;
	private static int lineNumber;
	private List<BoxCollider2D> colliders, toBeRemoved;
	private Dictionary<string, LineRenderer> m_lines;
	private float m_currentLineLength;
	private string btnCpation;

	//void OnGUI () // deprecated, use ordinary .UI now available in Unity
	//{
//		if (GUI.Button (new Rect (150, 10, 100, 50), btnCpation)) {
//			if(btnCpation == "Erase")
//				btnCpation = "Click on Line";
//			else
//				btnCpation = "Erase";
//		}
//	}

	void Awake ()
	{
		// Create line renderer component and set its property
		createLine ();	
		isMousePressed = false;
		pointsList = new List<Vector2> ();
		colliders = new List<BoxCollider2D> ();
		toBeRemoved = new List<BoxCollider2D> ();
		m_lines = new Dictionary<string, LineRenderer> ();
		lineNumber = 0;
		btnCpation = "Erase";
		m_currentLineLength = 0.0f;

        m_slider = GetComponentInChildren<Canvas>().GetComponentInChildren<Slider>();

    }

	//public void ScreenMouseRay()
	//{
	//	Vector3 mousePosition = Input.mousePosition;
	//	mousePosition.z = 5f;

	//	Vector2 v = Camera.main.ScreenToWorldPoint(mousePosition);

	//	Collider2D[] col = Physics2D.OverlapPointAll(v);

	//	if(col.Length > 0){
	//		foreach(Collider2D c in col)
	//		{
	//			Debug.Log("Clicked on: " + c.GetComponent<Collider2D>().gameObject.name);

	//			string name = c.GetComponent<Collider2D> ().gameObject.name;

	//			foreach(BoxCollider2D collder in colliders)
	//			{
	//				if (collder.name == name) {
	//					toBeRemoved.Add(collder);
	//					Destroy (collder.GetComponent<Collider2D> ().gameObject);
	//				}
	//			}

	//			LineRenderer toBeDeletedLine = m_lines[name];

	//			toBeDeletedLine.SetVertexCount (0);
	//		}
	//	}

	//	foreach (BoxCollider2D collder in toBeRemoved) {
	//		colliders.Remove (collder);
	//	}
	//}

	void Update ()
	{
		// If mouse button down, remove old line and set its color to green
		if (Input.GetMouseButtonDown (0)) {

			//if (btnCpation == "Click on Line") {
			//	ScreenMouseRay ();
			//	btnCpation = "Erase";
			//} else {
				if (line == null) {			
					createLine ();
				}
				//m_currentLineLength = 0;
				isMousePressed = true;
				line.SetVertexCount (0);
				pointsList.RemoveRange (0, pointsList.Count);
				line.SetColors (Color.green, Color.green);
			//}

		} else if (Input.GetMouseButtonUp (0)) {
			isMousePressed = false;

			m_lines.Add ("Chalk_" + lineNumber,line);

			lineNumber++;

			line = null;
		}
			
		// Drawing line when mouse is moving(presses)
		if (isMousePressed && m_currentLineLength < m_chalkSize) {
			mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			if (!pointsList.Contains (mousePos)) {
                pointsList.Add (mousePos);
				line.SetVertexCount (pointsList.Count);
				line.SetPosition (pointsList.Count - 1, (Vector2)pointsList [pointsList.Count - 1]);

				if (pointsList.Count > 2)
					AddColliderToLine (line, (Vector2)pointsList [pointsList.Count - 2], (Vector2)pointsList [pointsList.Count - 1]);
			}
		}

        float val = (float)(((float)m_chalkSize - (float)m_currentLineLength) / (float)m_chalkSize);
        Debug.Log(val);
        m_slider.value = val;




    }


	private void createLine ()
	{
		line = new GameObject ("Line" + currLines).AddComponent<LineRenderer> ();
		line.tag = "Obstical";
		line.material = material;
		line.SetVertexCount (3);
		line.SetWidth (0.15f, 0.15f);
		line.useWorldSpace = true; 
	}

	void addColliders ()
	{
		for (int i = 0; i < pointsList.Count - 2; i++) {
			Vector2 start = pointsList [i];
			Vector2 end = pointsList [i + 2];

			AddColliderToLine (line, start, end);

			i += 2;
		}
	}

	private void AddColliderToLine (LineRenderer line, Vector2 startPoint, Vector2 endPoint)
	{
		BoxCollider2D lineCollider = new GameObject ("Chalk_" + lineNumber).AddComponent<BoxCollider2D> ();
		lineCollider.sharedMaterial = m_physicMat;
		lineCollider.transform.parent = line.transform; 
		float lineWidth = line.endWidth; 
		float lineLength = Vector2.Distance (startPoint, endPoint);      
		lineCollider.size = new Vector2 (lineLength, lineWidth);  
		Vector2 midPoint = (startPoint + endPoint) / 2;
		lineCollider.transform.position = midPoint;

		float angle = Mathf.Atan2 ((endPoint.y - startPoint.y), (endPoint.x - startPoint.x));

		angle *= Mathf.Rad2Deg;

		//angle *= -1; 
		lineCollider.transform.Rotate (0, 0, angle);

		colliders.Add (lineCollider);

		float distance = Vector2.Distance (startPoint, endPoint);

		m_currentLineLength += distance;

		Debug.Log (m_currentLineLength.ToString());
	}


}

