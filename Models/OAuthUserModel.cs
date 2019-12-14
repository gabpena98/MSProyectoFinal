using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metodología_de_Software.Models
{
    public class OAuthUserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string Email { get; set; }
    }
}