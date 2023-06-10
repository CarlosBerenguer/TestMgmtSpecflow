using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestJamaTestMgmt.Config
{
    public abstract class PageObjectModel 
    {
        protected readonly SeleniumHelper Helper;
        
        protected PageObjectModel(SeleniumHelper helper)
        {
            Helper = helper;
        }        

        public string GetUrl()
        { 
            return Helper.GetUrl(); 
        }

        public void NavigateToUrl(string url)
        { 
            Helper.GoToUrl(url); 
        }

        public void AccessSystem() 
        {
            Helper.GoToUrl(Helper.Configuration.LoginUrl);
        }
    }
}
