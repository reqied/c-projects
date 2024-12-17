using System;

namespace HotelAccounting;

public class AccountingModel : ModelBase
{
    private double price;
    private double total;
    private int nightsCount;
    private double discount;
    
    public double Price
    {
        get => price;
        set
        {
            if (value < 0)
                throw new ArgumentException();
            price = value;
            Notify(nameof(Price));
            UpdateTotal();
        }
    }
    
    public int NightsCount
    {
        get => nightsCount;
        set
        {
            if (value <= 0) 
                throw new ArgumentException();
            nightsCount = value;
            Notify(nameof(NightsCount));
            UpdateTotal();
        }
    }
    
    public double Discount
    {
        get => discount;
        set
        {
            if (value > 100) 
                throw new ArgumentException();
            discount = value;
            Notify(nameof(Discount));
            UpdateTotal();
        }
    }

    public double Total
    {
        get => total;
        set
        {
            if (value < 0)
                throw new ArgumentException();
            total = value;
            Notify(nameof(Total));
            UpdateDiscount();
        }
    }
    
    private void UpdateTotal() 
    {
        var newTotal = price * nightsCount * (1 - discount / 100);
        if (newTotal is < 0 or > double.MaxValue)
            throw new ArgumentException();
        total = newTotal;
        Notify(nameof(Total));
    }
    
    private void UpdateDiscount()
    {
        if (price * nightsCount == 0)
        {
            throw new ArgumentException();
        }
        var calculatedDiscount = 100 * (1 - (total / (price * nightsCount)));
        if (calculatedDiscount < 0)
        {
            throw new ArgumentException();
        }
        discount = calculatedDiscount;
        Notify(nameof(Discount));
    }
}