using System.Collections;
using System.Collections.Generic;


public interface IProxyABOperator : IABOperator{
    Pin Outcome { get; set; }
    List<Pin> Incomes { get; set; }
}
