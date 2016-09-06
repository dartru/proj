using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstTrip
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 10000; i++)
            {
                Console.WriteLine(i + " городов = " + Test(i));
            }
            Console.ReadLine();
        }

        static List<Card> GetSortedCards(List<Card> raw_list)
        {
            List<Card> sorted_list = new List<Card>();
            string first_city = null;
            string last_city = null;

            if (raw_list.Count > 0)
            {
                sorted_list.Add(raw_list[0]);
                raw_list.RemoveAt(0);
                first_city = sorted_list[0].city_from;
                last_city = sorted_list[0].city_to;
            }
            
            while (first_city != null)
            {
                Card prev_card = raw_list.Find(x => x.city_to == first_city);
                if (prev_card != null)
                {
                    sorted_list.Insert(0, prev_card);
                    raw_list.Remove(prev_card);
                    first_city = prev_card.city_from;
                }
                else
                {
                    first_city = null;
                }
            }

            while (last_city != null)
            {
                Card next_card = raw_list.Find(x => x.city_from == last_city);
                if (next_card != null)
                {
                    sorted_list.Add(next_card);
                    raw_list.Remove(next_card);
                    last_city = next_card.city_to;
                }
                else
                {
                    last_city = null;
                }
            }
            return sorted_list;
        }

        static bool Test(int cities_count)
        {
            List<Card> raw_list = new List<Card>();
            string city_from_name = Guid.NewGuid().ToString();
            string city_to_name = String.Empty;
            if (cities_count == 1)
            {
                raw_list.Add(new Card(city_from_name, city_from_name));
            }
            else if (cities_count>1)
            {
                Random random = new Random();
                for (int i = 0; i < cities_count; i++)
                {
                    city_to_name = Guid.NewGuid().ToString();
                    raw_list.Insert(random.Next(0, raw_list.Count), new Card(city_from_name, city_to_name));
                    city_from_name = city_to_name;
                }
            }
            List<Card> sorted_list = GetSortedCards(raw_list);
            return IfSorted(sorted_list);
        }

        static bool IfSorted(List<Card> sorted_list)
        {
            bool is_checked = true;
            int sorted_list_count = sorted_list.Count;
            if (sorted_list_count<2) {
                return is_checked;
            }
            else {
                string city = sorted_list[0].city_to;
                for (int i = 1; i < sorted_list_count; i++ )
                {
                    is_checked = (city == sorted_list[i].city_from) ? is_checked : false;
                    city = sorted_list[i].city_to;
                }
            }
            return is_checked;
        }
    }

    class Card
    {
        public string city_from { get; set; }
        public string city_to { get; set; }
        public Card(string city_from, string city_to)
        {
            this.city_from = city_from;
            this.city_to = city_to;
        }
    }
}