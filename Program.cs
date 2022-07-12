using System.Linq;
using System.Net;
using System.Data;
using System.IO;
using System;

namespace Books
{
    public class Program
    {

        public static void Main(string[] args)
        {

            WebClient client = new WebClient();
            string strPageCode = client.DownloadString("https://api.actionnetwork.com/web/v1/books");

            Root dobj = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(strPageCode);

            //Console.WriteLine(dobj.books.Count);

            //Console.Write(typeof(string).Assembly.ImageRuntimeVersion);

            for (int i = 0; i < dobj.books.Count; i++)
            {
                if (dobj.books[i].parent_name == null)
                {
                    dobj.books.RemoveAt(i);
                    i--;
                }
            }

            //Console.WriteLine(dobj.books.Count);

            for (int i = 0; i < dobj.books.Count; i++)
            {
                var counter = 0;
                foreach (string s in dobj.books[i].meta.states.ToList())
                {
                    if(s == "NJ" || s == "CO")
                    {
                        counter++;
                    }
                }

                if (counter == 0)
                {
                    dobj.books.RemoveAt(i);
                    i--;
                }
            }

            //Console.WriteLine(dobj.books.Count);

            //for (int i = 0; i < dobj.books.Count; i++)
            //{
            //    Console.WriteLine(dobj.books[i].id);
            //    Console.WriteLine(dobj.books[i].parent_name);
            //    Console.WriteLine(dobj.books[i].display_name);
            //    foreach (string s in dobj.books[i].meta.states.ToList())
            //    {
            //        Console.WriteLine(s);
            //    }

            //}

            var query = dobj.books.GroupBy(book => book.parent_name)
                              .Select(group =>
                                    new {
                                        parent_name = group.Key,
                                        Books = group.OrderByDescending(x => x.parent_name)
                                    })
                              .OrderBy(group => group.Books.First().parent_name);

            using (StreamWriter writetext = new StreamWriter("c:\\NENAD\\result.txt"))
            {

                foreach (var group in query)
                {
                    writetext.WriteLine("{0}", "{" + group.parent_name + "}");
                    foreach (var book in group.Books)
                    {
                        writetext.Write("{0}", "{" + book.display_name + "}");
                        int index = dobj.books.FindIndex(a => a.id == book.id);
                        writetext.Write("{");
                        foreach (string s in dobj.books[index].meta.states.ToList())
                        {
                            writetext.Write(s + ",");
                            //File.AppendAllText(@"c:\\NENAD\\result.txt", s + ",");
                            //using (StreamWriter w = File.AppendText("c:\\NENAD\\result.txt"))
                            //{
                            //    writetext.WriteLine("{0}", "{" + s + "}");
                            //}
                        }
                        writetext.Write("}" + "\n");
                    }

                }
            }
            string text = File.ReadAllText("c:\\NENAD\\result.txt");
            text = text.Replace(",}", "}");
            File.WriteAllText("c:\\NENAD\\result.txt", text);

            //string text2 = File.ReadAllText("c:\\NENAD\\result.txt");
            //text2 = text2.Replace("{" + "\r", "{");
            //File.WriteAllText("c:\\NENAD\\result.txt", text2);
        }
    }
}
