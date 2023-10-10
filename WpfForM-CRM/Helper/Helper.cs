using System.Linq;
using System;
using WpfForM_CRM.Context;

namespace WpfForM_CRM.Helper;

public static class Helper
{
    public static string GenerateBarcode()
    {
        var db = new AppDbContext();
        Random random = new Random();
        const int barcodeLength = 8; // 11 ta raqamdan keyin 637 qo'shiladi

        // Random raqamlardan tuzilgan barcode generatsiya qilish
        string barcode;
        bool isUnique = false;

        do
        {
            barcode = "637" + new string(Enumerable.Range(0, barcodeLength - 3)
                .Select(_ => random.Next(10).ToString()[0])
                .ToArray());

            // Bazada shu barcode mavjudlikni tekshirish
            isUnique = !db.Products.Any(p => p.Barcode == barcode);
        } while (!isUnique);

        return barcode;
    }


    public static string ToUpperNamesOneChar(string name)
    {

        name = char.ToUpper(name[0]) + name.Substring(1);
        return name;
    }
}