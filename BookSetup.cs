using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleQuiz
{
    class BookSetup
    {
        private string path = "BibleBooks.txt";

        //The dictionary holds a persistent book list.
        public static Dictionary<int, string> BookDictionary = new Dictionary<int, string>();
        
        //The list holds all the keys from the dictionary to be shuffled for random quizes.
        public List<int> BookKeys = new List<int>(BookDictionary.Count);

        public BookSetup()
        {
            GetBooks();
            //ShuffleBookKeys();
        }

        public void GetBooks()
        {   //Populate the BookListDictionary.
            using (StreamReader sr = new StreamReader(path))
            {
                int counter = 1;
                while (sr.EndOfStream == false)
                {
                    string line = sr.ReadLine();
                    //Add the counter as the key and the book name as the value.
                    BookDictionary.Add(counter, line);
                    counter++;
                }
            }
        }

        public void ShuffleBookKeys()
        {
            //Add the keys from the BookDictionary to the BookKeys list.
            foreach (KeyValuePair<int, string> keyValue in BookDictionary)
            {
                BookKeys.Add(keyValue.Key);
            }

            int ListLength = BookKeys.Count;
            Random rand = new Random();

            //Shuffle the list.
            for (int i = 0; i < ListLength; i++)
            {
                int r = i + (rand.Next(0, ListLength - i));
                int temp = BookKeys[i];
                BookKeys[i] = BookKeys[r];
                BookKeys[r] = temp;
            }
        }
    }
}
