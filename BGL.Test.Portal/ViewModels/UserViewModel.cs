using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGL.Test.Portal.Models
{
    public class UserViewModel
    {
        public string UserName
        { 
            get;
            set;
        }

         public string Location
         {
             get;
             set;
         }

         public string Avatar
         {
             get;
             set;
         }

        public IList<string> Repository
        {
            get;
            set;
        }
    }
}