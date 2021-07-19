using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBooksTranslator.Server.Configuration
{
    public class PtiApisConfiguration
    {
        public BooksTranslationConfiguration BooksTranslationConfiguration { get; set; }
    }

    public class BooksTranslationConfiguration
    {
        public string RapidApiHost { get; set; }
        public string RapidApiKey { get; set; }
    }

}
