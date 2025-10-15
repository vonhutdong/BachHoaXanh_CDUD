using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BUS
{
    public class DataValidation
    {
        // Fields
        private string str_name;
        private int str_length;
        //BUS_SanPham bus_sp = new BUS_SanPham();

        // Constructors
        public DataValidation(string str_name, int str_length)
        {
            this.str_name = str_name;
            this.str_length = str_length;
        }

        public DataValidation()
        {
            this.str_name = string.Empty;
            this.str_length = 0;
        }

        // Properties
        public string Str_name { get => str_name; set => str_name = value; }
        public int Str_length { get => str_length; set => str_length = value; }

        // Methods
        public bool CheckString(string name, int strlength)
        {
            this.str_name = name;
            this.str_length = strlength;

            if (str_name != string.Empty && name.Length <= strlength)
            {
                return true;
            }

            return false;
        }

        /*
         * CheckNumber & return bool
         * Checked by name, strlength
         */
        public bool CheckNumber(string name, int strlength)
        {
            this.str_name = name;
            this.str_length = strlength;

            if (str_name == null)
            {
                return false;
            }

            if (str_name.Length > str_length)
            {
                return false;
            }

            for (int i = 0; i < str_name.Length; i++)
            {
                if (str_name[i] >= 'a' && str_name[i] <= 'z' || str_name[i] >= 'A' && str_name[i] <= 'Z')
                {
                    return false;
                }
            }

            return true;
        }

        /*
         * CheckNumber2 & return bool
         * Checked by name, strlength & int.Parse(name) > 100
         */
        public bool CheckNumber2(string name, int strlength)
        {
            this.str_name = name;
            this.str_length = strlength;

            if (str_name == null)
            {
                return false;
            }

            if (str_name.Length > str_length)
            {
                return false;
            }

            for (int i = 0; i < str_name.Length; i++)
            {
                if (str_name[i] >= 'a' && str_name[i] <= 'z' || str_name[i] >= 'A' && str_name[i] <= 'Z')
                {
                    return false;
                }
            }

            if (int.Parse(name) > 100)
            {
                return false;
            }

            return true;
        }

        /*
         * CheckNumber3 & return bool
         * Checked by name
         */
        public bool CheckNumber3(string name)
        {
            this.str_name = name;

            if (str_name == null)
            {
                return false;
            }

            for (int i = 0; i < str_name.Length; i++)
            {
                if (str_name[i] >= 'a' && str_name[i] <= 'z' || str_name[i] >= 'A' && str_name[i] <= 'Z')
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsValidString(string input)
        {
            // Kiểm tra chuỗi không null và không rỗng
            if (string.IsNullOrEmpty(input))
                return false;

            // Biểu thức chính quy kiểm tra chuỗi chỉ chứa chữ cái
            Regex regex = new Regex("^[a-zA-Z]+$");

            // Trả về true nếu chuỗi hợp lệ, ngược lại là false
            return regex.IsMatch(input);
        }

        public bool IsValidString2(string input)
        {
            // Kiểm tra chuỗi không null và không rỗng
            if (string.IsNullOrEmpty(input))
                return false;

            // Biểu thức chính quy kiểm tra chuỗi chỉ chứa chữ cái và số
            Regex regex = new Regex("^[a-zA-Z0-9]+$");

            // Trả về true nếu chuỗi hợp lệ, ngược lại là false
            return regex.IsMatch(input);
        }

        public bool IsValidNumber(string input)
        {
            // Kiểm tra chuỗi không null và không rỗng
            if (string.IsNullOrEmpty(input))
                return false;

            // Biểu thức chính quy kiểm tra chuỗi chỉ chứa chữ số
            Regex regex = new Regex("^[0-9]+$");

            // Trả về true nếu chuỗi hợp lệ, ngược lại là false
            return regex.IsMatch(input);
        }

        // Checked SoLuongSp trong kho
        //public bool CheckSoLuongSp(int idSanPham, string soLuong)
        //{
        //    this.str_name = soLuong;

        //    if (!IsValidNumber(str_name))
        //    {
        //        return false;
        //    }


        //    if (int.Parse(soLuong) > bus_sp.GetSoLuongSpTrongKho(idSanPham))
        //    {
        //        return false;
        //    }

        //    return true;
        //}
    }
}
