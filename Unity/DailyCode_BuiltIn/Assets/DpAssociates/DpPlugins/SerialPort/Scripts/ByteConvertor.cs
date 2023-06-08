using System;

namespace DpPlugin
{
    public enum ByteConvertingType
    {
        Byte,
        String,
        Hex,
    }

    public class ByteConvertor
    {
        public static string GetConvertedData(byte[] bytes, ByteConvertingType type)
        {
            switch (type)
            {
                case ByteConvertingType.Byte:
                    return bytes.ToString();
                case ByteConvertingType.String:
                    return System.Text.Encoding.Default.GetString(bytes);
                case ByteConvertingType.Hex:
                    return BitConverter.ToString(bytes).Replace("-", "");
            }

            return "";
        }
    }
}
