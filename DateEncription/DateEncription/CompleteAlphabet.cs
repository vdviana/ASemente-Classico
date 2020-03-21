using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateEncription
{
    public class CompleteAlphabet
    {
        public string CharactersMap { get; set; }
        public int Index { get; set; }
        public IDictionary<int, char> AlphabetMap { get; set; }
        
        public CompleteAlphabet()
        {
            AlphabetMap = new Dictionary<int, char>();
            
            char[] listchar = @" aU$Ká^b%ã~c¨àTJ`d&â´e*äºf(éIªgS)è'hL¹êV}i²ë{jH³.í]Rk£ì[l¢î1mW;¬Mï2Gn-ñ3oQ=ó4p_õ5qF+ò6rN§ô7Xs|Zút\Eù8u/ûO9vBY?ü0w<!CPx>@,y:Az#".ToCharArray();
            
            if (!string.IsNullOrWhiteSpace(CharactersMap) &&CharactersMap.ToCharArray().Length >= listchar.Length)
            {
                listchar = CharactersMap.ToCharArray();
            }
            
            for (int i = 0; i < listchar.Length; i++)
            {
                AlphabetMap.Add(i, listchar[i]);
            }

            Index = 0;

        }

        public int getCurrentIndex()
        {
            return Index;
        }
        
        /// <summary>
        /// if you don't want to put an argument , is possible , it just recover the char from the current index
        /// </summary>
        /// <returns></returns>
        public char getCharFromIndex(int? index)
        {
            if (!index.HasValue) { index = Index; }
            return AlphabetMap.Where(x => x.Key == index).Select(x => x.Value).FirstOrDefault();
        }

        public void PutNextIndex()
        {
            Index = Index++;
        }

        public char GetNextChar()
        {
            Index = Index++;
            return getCharFromIndex(Index);
        }

        public void PutBeforeIndex()
        {
            Index = Index--;
        }

        public char GetBeforeChar()
        {
            Index = Index--;
            return getCharFromIndex(Index);
        }

        public int getCharIndex(char? p_character)
        {
            if (p_character.HasValue)
                return AlphabetMap.Where(x => x.Value == p_character.Value).Select(x => x.Key).FirstOrDefault();
            else
                return -1;
        }

        public int[] MapCharacters(char[] p_CharList)
        {
            List<int> charsMap = new List<int>();

            for (int i = 0; i < p_CharList.Length; i++)
            {
                int index = AlphabetMap.Where(x => x.Value == p_CharList[i]).Select(x => x.Key).FirstOrDefault();

                charsMap.Add(index);
            }

            return charsMap.ToArray();
        }

        public char[] MapIndexes(int[] p_IndexList)
        {
            List<char> IndexMap = new List<char>();

            int valorMaximoIndexMapa = AlphabetMap.Keys.ToArray().Length-1;
            int valorMinimoIndexMapa = 0;
            for (int i = 0; i < p_IndexList.Length; i++)
            {
                int result = 0;
                bool excedidolimites = false;

                while (p_IndexList[i] > valorMaximoIndexMapa)
                {
                    excedidolimites = true;

                    result = p_IndexList[i] - valorMaximoIndexMapa;
                    p_IndexList[i] = result;
                }
                while (p_IndexList[i] < valorMinimoIndexMapa)
                {
                    excedidolimites = true;

                    result = p_IndexList[i] + valorMaximoIndexMapa;
                    p_IndexList[i] = result;
                }

                
                char index = AlphabetMap.Where(x => x.Key == p_IndexList[i]).Select(x => x.Value).FirstOrDefault();

                IndexMap.Add(index);
            }
            return IndexMap.ToArray();
        }

    }
}
