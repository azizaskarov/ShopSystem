using System.ComponentModel;
using System.Security.Cryptography;

namespace WpfForM_CRM.Entities;

public class Stock
{
    [DisplayName("Номер")]
    public int? Номер { get; set; }
    public string? Штрихкод { get; set; }
    public string? Категория { get; set; }
    public string? Подкатегория { get; set; }
    public string? Продукт { get; set; }
    public string? Прибывшая { get; set; }
    public string? Текущая { get; set; }
    public string? Количство { get; set; }
    //public string Магазин { get; set; }

    //public string? ShopName { get; set; }
}