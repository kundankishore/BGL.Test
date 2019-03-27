using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BGL.Test.Portal.Models
{
    public class SearchViewModel
    {
        [Required]
        public string Name
        { get; set; }
    }
}