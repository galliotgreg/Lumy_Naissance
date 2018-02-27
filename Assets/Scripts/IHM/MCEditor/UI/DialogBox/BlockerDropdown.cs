using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerDropdown : UnityEngine.UI.Dropdown {

	GameObject blocker;

	protected override GameObject CreateBlocker (Canvas rootCanvas)
	{
		blocker = base.CreateBlocker (rootCanvas);
		return blocker;
	}

	protected override void OnDestroy ()
	{
		Destroy (blocker);
		base.OnDestroy ();
	}
}
