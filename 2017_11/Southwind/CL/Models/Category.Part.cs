using System;
using System.Linq;

namespace Southwind.Contracts.Models
{
    public partial class Category
    {
        public string ImageSource
        {
            get
            {
                if (Picture == null) return string.Empty;
                var base64 = Convert.ToBase64String(Picture.Skip(78).ToArray());
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                return imgSrc;
            }
        }
    }
}
