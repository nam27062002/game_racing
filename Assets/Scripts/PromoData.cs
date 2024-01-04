[System.Serializable]
public class PromoData
{
    public bool success;
    public int status;
    public string message;
    public Promo[] data;
    public string error;
}

[System.Serializable]
public class Promo
{
    public int id;
    public string title;
    public string des;
    public string image;
    public string status;
    public string code;
    public int discount;
    public int quantity;
    public string type;
    public string startDate;
    public string endDate;
    public string createdAt;
    public string updatedAt;
    public string GetString => $"{id} - {title} - {des} - {image} - {status} - {code} - {discount} - {quantity} - {type} - {startDate} - {endDate}";
}