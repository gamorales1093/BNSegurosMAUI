using System;
namespace BNSegurosMAUI.Models
{
	public class OnlineInsurance
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }

        public bool ComingSoon { get; set; }
        public bool IsComingSoon { get { return ValidateUrl(Url); } }
        public bool ValidateUrl(string url)
        {
            if (url != "Próximamente")
                return false;
            else
                return true;
        }
    }
}

