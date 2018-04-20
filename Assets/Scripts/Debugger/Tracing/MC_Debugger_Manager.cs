using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Debugger_Manager : MonoBehaviour {

	#region SINGLETON
	/// <summary>
	/// The static instance of the Singleton for external access
	/// </summary>
	public static MC_Debugger_Manager instance = null;

	void Awake()
	{
		//Check if instance already exists and set it to this if not
		if (instance == null)
		{
			instance = this;
		}

		//Enforce the unicity of the Singleton
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}
	#endregion

	[SerializeField]
	GameObject container;

	// Current Values to be shown
	ABModel current_Model;
	AgentEntity current_Entity = null;
	Tracing current_Tracing;

	bool hidden = false;

	public void drawDebugger(){
		current_Tracing = ABManager.instance.getCurrentTracing (current_Entity);

		clearTracing ();
		instantiateTracing( current_Tracing, current_Model );
	}

	// Use this for initialization
	void Start () {

		if (lateralBar != null) {
			lateralBarWidth = lateralBar.anchorMax.x;
			hide ();
		}

		if (lateralBarButton != null) {
			lateralBarButton.onClick.AddListener ( clickLateralBar );
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!hidden){
			if (current_Entity != null) {
				drawDebugger ();
			}else{
				clearTracing ();
				deactivateDebugger ();
			}
		}
	}

	#region Activation
	public void activateDebugger( AgentEntity entity ){
		// activate container
		// get data
		current_Entity = entity;
		current_Model = ABManager.instance.FindABInstance( entity.Id ).Model;
	}
	public void deactivateDebugger(){
		// deactivate container
		// removing data
		current_Model = null;
		current_Entity = null;
		current_Tracing = null;
	}

	[SerializeField]
	RectTransform lateralBar;
	[SerializeField]
	UnityEngine.UI.Button lateralBarButton;
	[SerializeField]
	UnityEngine.UI.Image lateralBarImage;
	[SerializeField]
	float lateralBarWidth;
	public void clickLateralBar(){
		if (hidden) {
			show ();
		} else {
			hide ();
		}
	}

	void hide(){
		hidden = true;
		lateralBar.anchorMax = new Vector2 ( 0, lateralBar.anchorMax.y );
		lateralBarImage.transform.Rotate (0,0,-180);
	}
	void show(){
		hidden = false;
		lateralBar.anchorMax = new Vector2 ( lateralBarWidth, lateralBar.anchorMax.y );
		lateralBarImage.transform.Rotate (0,0,180);
	}
	#endregion

	#region Tracing
	void instantiateTracing( Tracing tracing, ABModel model ){
		if (tracing != null && model != null) {
			List<TracingInfo> tracingList = tracing.getTracingInfo ();
			//List<Debugger_Node> proxies = new List<Debugger_Node> ();
			List<MC_Proxy_Debugger_Image> proxies = new List<MC_Proxy_Debugger_Image> ();
			for (int i = 0; i < tracingList.Count; i++) {
				// State
				//proxies.Add (tracingList [i].InfoProxy (model, container.transform));
				proxies.Add (tracingList [i].InfoProxy_Image (model, container.transform));
				UnityEngine.UI.LayoutElement layoutElement = proxies [i].gameObject.AddComponent<UnityEngine.UI.LayoutElement> ();
				layoutElement.flexibleHeight = 120;
				//proxies [i].transform.position = new Vector3 (0, (tracingList.Count - i) * 1.5f, 0);
			}
		}
	}
	void clearTracing(  ){
		for( int i = 0; i < container.transform.childCount; i++ ){
			Transform child = container.transform.GetChild (i);
			Destroy (child.gameObject);
		}
	}
	#endregion
}
