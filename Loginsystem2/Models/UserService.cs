using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loginsystem2.Models
{
    public class UserService
    {
        public int Id { get; set; }

        public string AdvertiserName { get; set; }

        public string Service { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Contacts { get; set; }

        public string Location { get; set; }

        public string image { get; set; }

        [NotMapped]

        public HttpPostedFileBase File { get; set; }
    }
}