using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZ_Japanese_Tutor
{
    class Tokenized
    {
        public string CharacterWord { get; set; } = "";
        public string PartOfSpeech { get; set; } = "";
        public string PartOfSpeedSubdiv1 { get; set; } = "";
        public string PartOfSpeedSubdiv2 { get; set; } = "";
        public string PartOfSpeedSubdiv3 { get; set; } = "";
        public string UsageType { get; set; } = "";
        public string InflectionForm { get; set; } = "";
        public string OriginalForm { get; set; } = "";
        public string Reading { get; set; } = "";
        public string Pronunciation { get; set; } = "";

        public Tokenized(string setString)
        {
            string[] splitString = setString.Split(',');

            if (splitString.Length == 10)
            {
                CharacterWord = splitString[0];
                PartOfSpeech = splitString[1];
                PartOfSpeedSubdiv1 = splitString[2];
                PartOfSpeedSubdiv2 = splitString[3];
                PartOfSpeedSubdiv3 = splitString[4];
                UsageType = splitString[5];
                InflectionForm = splitString[6];
                OriginalForm = splitString[7];
                Reading = KatakanaToHiragana(splitString[8]);
                Pronunciation = KatakanaToHiragana(splitString[9]);

            }
            else
            {
                if (splitString[0] != "EOS")
                {
                    System.Diagnostics.Debug.WriteLine("Unexpected Tokenized string length: " + string.Join(",", splitString));

                }

            }

        }

        public override string ToString()
        {
            return CharacterWord;

        }

        public string KatakanaToHiragana(string input)
        {
            byte[] unicodes = Encoding.GetEncoding("Unicode").GetBytes(input);
            int i;

            for (i = 0; i < unicodes.Length; i += 2)                                     //Each 16 bits.
            {
                int _word = (unicodes[i + 1] << 8) | (unicodes[i] & 0xFF);              //Two byte make a word.
                if (_word >= 0x30A1 && _word <= 0x3100)                                 //In hiragana area
                {
                    _word -= 0x60;                                                      //Add difference
                    unicodes[i + 1] = (byte)(_word >> 8);                               //Write back high byte.
                    unicodes[i] = (byte)(_word & 0xFF);                                 //Write back low byte.

                }

            }

            return Encoding.GetEncoding("Unicode").GetString(unicodes);

        }

        public string HiraganaToKatakana(string input)
        {
            byte[] unicodes = Encoding.GetEncoding("Unicode").GetBytes(input);
            int i;

            for (i = 0; i < unicodes.Length; i += 2)                                     //Each 16 bits.
            {
                int _word = (unicodes[i + 1] << 8) | (unicodes[i] & 0xFF);              //Two byte make a word.
                if (_word >= 0x3041 && _word <= 0x30A0)                                 //In hiragana area
                {
                    _word += 0x60;                                                      //Add difference
                    unicodes[i + 1] = (byte)(_word >> 8);                               //Write back high byte.
                    unicodes[i] = (byte)(_word & 0xFF);                                 //Write back low byte.

                }

            }

            return Encoding.GetEncoding("Unicode").GetString(unicodes);

        }

    }

}
