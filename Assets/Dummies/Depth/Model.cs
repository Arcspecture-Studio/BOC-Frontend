using System;
using System.Collections.Generic;

[Serializable]
public class Model
{
    public CaseStudy caseStudy;
}
public class CaseStudy
{
    public Depth depth;
}
[Serializable]
public class Depth
{
    public List<BarData> asks;
    public List<BarData> bids;
}
[Serializable]
public class BarData
{
    public double price;
    public double quantity;
}