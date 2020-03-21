using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateEncription
{
   public class NewDateKey
    {
        public string Info { get; set; }
        public string DateTimeInfo { get; set; }
        public string DateTimeSequence { get; set; }
        public char[] InfoCharsList { get; set; }
        public int[] MapedSequenceIndexInfo { get; set; }
        public int[] ShuffledDateTimeKey { get; set; }
        public CompleteAlphabet AlphabetMap = new CompleteAlphabet();
        
        public NewDateKey(string p_Info = null, DateTime? p_DateTimeKey = null, string p_DateTimeSequence = null)
        {
            if (p_DateTimeKey.HasValue && !string.IsNullOrWhiteSpace(p_DateTimeSequence) && !string.IsNullOrWhiteSpace(p_Info))
            {
                AttachData(Info, p_DateTimeKey.Value, DateTimeSequence);
            }
        }

        private int GetHexaDecimalRepresentationValue(char p_charValue)
        {
            switch
                (p_charValue)
            {
                case 'A': return 10;
                case 'B': return 11;
                case 'C': return 12;
                case 'D': return 13;
                case 'E': return 14;
                case 'F': return 15;
                case 'G': return 16;
                default: return int.Parse(p_charValue.ToString());
            }

        }

        private char[] ShuffleWithDatetimeSequence(string p_DatetimeSequence, char[] p_DateTimeCharList)
        {
            char[] datetimeSequenceList = p_DatetimeSequence.ToArray();
            char[] shuffledList = new char[16];
            int i;
            int j = 0;

            for (i = 0; i < p_DatetimeSequence.Length; i++)
            {
                if (p_DateTimeCharList.Length <= i)
                {
                    j = 0;
                }

                int position = GetHexaDecimalRepresentationValue(datetimeSequenceList[i]);

                shuffledList[position] = p_DateTimeCharList[j];
                shuffledList[j] = p_DateTimeCharList[position];

                j++;

            }

            return shuffledList;
        }

        /// <summary>
        /// Put data information that need to Encrypt/Decrypt
        /// </summary>
        /// <param name="StringInfo">Password or something for encryption/decryption</param>
        /// <param name="DatetimeInfo">Datetime key for encryption/decryption</param>
        /// <param name="DatetimeSequence">Hexadecimal sequence Key for datetime key encryption</param>
        /// <param name="CharacterMapSequence">Characters sequence map for charracter position key encryption = optional EX:"abcdefghij";
        ///    Attebtion: it need all characters known</param>
        public void AttachData(string StringInfo, DateTime DatetimeInfo, string DatetimeSequence, string CharacterMapSequence = null)
        {

            if (!string.IsNullOrWhiteSpace(CharacterMapSequence))
            {
                AlphabetMap.CharactersMap = CharacterMapSequence;
            }

            bool invalid = false;
            foreach (char c in DatetimeSequence.ToList())
            {
                int i = 0;
                if (int.TryParse(c.ToString(), out i)) { continue; }

                if (c != 'A' && c != 'B'
                && c != 'C' && c != 'D'
                && c != 'E' && c != 'F')
                {
                    invalid = true;
                }
            }

            if (invalid)
            {
                throw new Exception("DateTime sequence need only numbers and hexadecimal letters");
            }

            char[] DateTimeCharactersList = DatetimeInfo.ToString("yyyyMMddhhmmssff").ToCharArray();

            DateTimeCharactersList = ShuffleWithDatetimeSequence(DatetimeSequence, DateTimeCharactersList);


            List<int> ShuffledDateTimeCharactersList = new List<int>();

            foreach (char c_index in DateTimeCharactersList.ToList())
            {
                ShuffledDateTimeCharactersList.Add(int.Parse(c_index.ToString()));
            }

            InfoCharsList = StringInfo.ToCharArray();
            ShuffledDateTimeKey = ShuffledDateTimeCharactersList.ToArray();
        }

        /// <summary>
        /// Encrypts information you want to be encrypted in NewDateKey encryption
        /// </summary>
        /// <returns></returns>
        public string Encrypt()
        {
            if (InfoCharsList.Length <= 0)
            {
                throw new Exception("Need attach informations before encryption");
            }
            MapedSequenceIndexInfo = AlphabetMap.MapCharacters(InfoCharsList);
            List<int> encryptedInfoIndexes = new List<int>();
            if (MapedSequenceIndexInfo.Length > ShuffledDateTimeKey.Length)
            {
                int j = 0;
                int MinorLength = ShuffledDateTimeKey.Length;
                for (int i = 0; i < MapedSequenceIndexInfo.Length; i++)
                {
                    MapedSequenceIndexInfo[i] += (ShuffledDateTimeKey[j]);

                    j++;

                    if (MinorLength <= j)
                    {
                        j = 0;
                    }
                }
            }
            else
            {
                int j = 0;
                int MinorLength = MapedSequenceIndexInfo.Length;
                for (int i = 0; i < ShuffledDateTimeKey.Length; i++)
                {
                    MapedSequenceIndexInfo[j] += (ShuffledDateTimeKey[i]);
                    
                    j++;

                    if (MinorLength <= j)
                    {
                        j = 0;
                    }
                }
            }
            var u = AlphabetMap.MapIndexes(MapedSequenceIndexInfo.ToArray());

            return string.Join("", u).TrimStart();

        }
        
        /// <summary>
        /// Decrypts information you want to be decrypted in NewDateKey decryption
        /// </summary>
        /// <returns></returns>
        public string Decrypt()
        {
            if (InfoCharsList.Length <= 0)
            {
                throw new Exception("Need attach informations before decryption");
            }
            MapedSequenceIndexInfo = AlphabetMap.MapCharacters(InfoCharsList);
            List<int> decryptedInfoIndexes = new List<int>();
            if (MapedSequenceIndexInfo.Length > ShuffledDateTimeKey.Length)
            {
                int j = 0;
                int MinorLength = ShuffledDateTimeKey.Length;
                for (int i = 0; i < MapedSequenceIndexInfo.Length; i++)
                {
                    MapedSequenceIndexInfo[i] -= (ShuffledDateTimeKey[j]);


                    j++;

                    if (MinorLength <= j)
                    {
                        j = 0;
                    }
                }
            }
            else
            {
                int j = 0;
                int MinorLength = MapedSequenceIndexInfo.Length;
                for (int i = 0; i < ShuffledDateTimeKey.Length; i++)
                {
                    MapedSequenceIndexInfo[j] -= (ShuffledDateTimeKey[i]);


                    j++;

                    if (MinorLength <= j)
                    {
                        j = 0;
                    }
                }
            }
            var u = AlphabetMap.MapIndexes(MapedSequenceIndexInfo.ToArray());

            string returnMethod = string.Join("", u);

            returnMethod = returnMethod.TrimStart();

            return returnMethod;


        }


    }



}












