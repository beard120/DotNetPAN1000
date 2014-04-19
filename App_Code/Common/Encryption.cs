using System;
using System.Text;
namespace Common
{


    public class Encryption
    {
        public static string Escape(string content)
        {
            return escape(escape(escape(content)));
        }


        static string escape(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }

            string base64 = EncodeBase64(content);
            string reverseString = ReverseString(base64);
            string crossString = EncodeCrossString(reverseString, 3);
            return crossString;
        }

        public static string UnEscape(string content)
        {
            return unescape(unescape(unescape(content)));
        }

        static string unescape(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }

            string decodeCrossString = DecodeCrossString(content, 3);
            string reverseString = ReverseString(decodeCrossString);
            return DecodeBase64(reverseString);
        }


        public static string EncodeBase64(string str)
        {

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        public static string DecodeBase64(string str)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(str);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string DecodeCrossString(string str, int bitNum)
        {

            StringBuilder builder = new StringBuilder();
            int m = str.Length % bitNum;
            int g = (str.Length - m) / bitNum;

            string suffix = str.Substring(str.Length - m);


            string[] strArray = new string[g];

            int inx;

            //拆分为组
            for (inx = 0; inx < g; inx++)
            {
                strArray[inx] = str.Substring(inx * bitNum, bitNum);
            }

            //组内反转
            for (inx = 0; inx < g; inx++)
            {
                strArray[inx] = ReverseString(strArray[inx]);
            }




            //反转组
            string first = strArray[g - 1];
            for (inx = g - 1; inx > 0; inx--)
            {
                strArray[inx] = strArray[inx - 1];
            }
            strArray[0] = first;




            //ok
            for (inx = 0; inx < g; inx++)
            {
                builder.Append(strArray[inx]);
            }



            builder.Append(suffix);
            return builder.ToString();
        }



        public static string EncodeCrossString(string str, int bitNum)
        {

            StringBuilder builder = new StringBuilder();
            //012 345 678 9
            //abc def ghi jk
            //3
            int m = str.Length % bitNum;


            int g = (str.Length - m) / bitNum;


            string suffix = str.Substring(str.Length - m);



            string[] strArray = new string[g];


            int inx;

            //拆分为组
            for (inx = 0; inx < g; inx++)
            {
                strArray[inx] = str.Substring(inx * bitNum, bitNum);
            }


            //反转组
            string first = strArray[0];
            for (inx = 0; inx < (g - 1); inx++)
            {
                strArray[inx] = strArray[inx + 1];
            }
            strArray[g - 1] = first;


            //组内反转
            for (inx = 0; inx < g; inx++)
            {
                strArray[inx] = ReverseString(strArray[inx]);
                builder.Append(strArray[inx]);
            }


            builder.Append(suffix);
            return builder.ToString();
        }

        public static string ReverseString(string str)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = str.Length - 1; i >= 0; i--)
            {
                builder.Append(str[i]);
            }
            return builder.ToString();
        }

    }



}