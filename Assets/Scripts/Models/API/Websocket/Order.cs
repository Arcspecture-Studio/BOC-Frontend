using System;
using System.Collections.Generic;

[Serializable]
public class Order
{
    public List<OrderContent> contents;

    public Order(List<OrderContent> contents)
    {
        this.contents = contents;
    }
}

[Serializable]
public class OrderContent
{
}