using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBooksTranslator.Shared
{
    public class TranslateModel
    {
        [Required]
        public string EmailAddressForResults { get; set; }
        [Required]
        [Url]
        public string SourceFileUrl { get; set; }
        [Required]
        public string BookSourceLanguage { get; set; } = "English";
        [Required]
        public string BookOutputLanguage { get; set; } = "Spanish";
        [Required]
        public string TranslationMode { get; set; } = "KeepFormatting";
    }
}
