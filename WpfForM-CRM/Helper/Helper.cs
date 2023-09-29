using System.Linq;
using System;

namespace WpfForM_CRM.Helper;

public static class Helper
{
    public static string GenerateBarcode()
    {
        Random random = new Random();
        const int barcodeLength = 11; // 11 ta raqamdan keyin 637 qo'shiladi

        // Random raqamlardan tuzilgan barcode generatsiya qilish
        string barcode = "637" + new string(Enumerable.Range(0, barcodeLength - 3)
            .Select(_ => random.Next(10).ToString()[0])
            .ToArray());

        return barcode;
    }

    public static string ToUpperNamesOneChar(string name)
    {
        
        name = char.ToUpper(name[0]) + name.Substring(1);
        return name;
    }
}