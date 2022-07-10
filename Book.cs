using System;
using System.Collections.Generic;
using System.Text;

namespace Books
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Book
    {
        public int id { get; set; }
        public string display_name { get; set; }
        public string abbr { get; set; }
        public string source_name { get; set; }
        public Meta meta { get; set; }
        public string parent_name { get; set; }
        public int? book_parent_id { get; set; }
        public int? affiliate_id { get; set; }
    }

    public class Deeplink
    {
        public bool has_multi { get; set; }
        public bool is_supported { get; set; }
    }

    public class Logos
    {
        public string promo { get; set; }
        public string primary { get; set; }
        public string thumbnail { get; set; }
        public string betslip_carousel { get; set; }
    }

    public class Meta
    {
        public Logos logos { get; set; }
        public List<string> states { get; set; }
        public bool is_promoted { get; set; }
        public string website { get; set; }
        public Deeplink deeplink { get; set; }
        public bool? is_legal { get; set; }
        public bool? is_preferred { get; set; }
        public string primary_color { get; set; }
        public int? betsync_status { get; set; }
        public string secondary_color { get; set; }
        public bool? is_fastbet_enabled_app { get; set; }
        public bool? is_fastbet_enabled_web { get; set; }
    }

    public class Root
    {
        public List<Book> books { get; set; }
    }


}
