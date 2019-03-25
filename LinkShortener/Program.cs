using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkShortener.Repositories;

namespace LinkShortener
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkRepo lr = new LinkRepo();

           lr.convertInShortURL("shdjskldhf");
        }
    }
}
