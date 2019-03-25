using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.Entities
{
    public class LinkDto
    {
        public int Id { get; set; }
        public string FullLink { get; set; }
        public string ShortLink { get; set; }
        public int CountOpen { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
