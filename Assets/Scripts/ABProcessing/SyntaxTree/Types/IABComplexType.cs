using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IABComplexType : IABType {
	System.Type getInternalType (int index = 0);
}