using System;
using System.Text;
using System.Runtime.InteropServices;

namespace EZ_Japanese_Tutor
{
    public class MeCab
    {
        [DllImport("libmecab.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        #pragma warning disable IDE1006 // Naming Styles - these functions are dictated by DLL file, cannot change
        private extern static IntPtr mecab_new2(string arg);
        [DllImport("libmecab.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private extern static IntPtr mecab_sparse_tostr(IntPtr m, byte[] str);
        [DllImport("libmecab.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private extern static void mecab_destroy(IntPtr m);

        public static String Parse(String input)
        {
            IntPtr mecab = mecab_new2("");
            IntPtr nativeStr = mecab_sparse_tostr(mecab, Encoding.UTF8.GetBytes(input));
            int size = nativeArraySize(nativeStr) - 1;
            byte[] data = new byte[size];
            Marshal.Copy(nativeStr, data, 0, size);

            mecab_destroy(mecab);

            return Encoding.UTF8.GetString(data);

        }
        
        private static int nativeArraySize(IntPtr ptr)
        {
            int size = 0;
            while (Marshal.ReadByte(ptr, size) > 0)
                size++;

            return size;

        }

    }

}