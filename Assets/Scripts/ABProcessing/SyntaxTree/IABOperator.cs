using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IABOperator {
    ABNode[] Inputs { get; set; }

    string ClassName { get; set; }

    System.Type getOutcomeType ();

	System.Type getIncomeType (int index);
	bool acceptIncome (int index, System.Type income);
}
