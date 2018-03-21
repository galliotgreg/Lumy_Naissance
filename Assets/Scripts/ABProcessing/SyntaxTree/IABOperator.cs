using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IABOperator {
	ABNode[] Inputs { get; set; }

    string ClassName { get; set; }
    string ViewName { get; set; }
    string SymbolName { get; set; }

	OperatorType OpType { get; set; }

    System.Type getOutcomeType ();

	System.Type getIncomeType (int index);
	bool acceptIncome (int index, System.Type income);

	IABOperator Clone ();
}
