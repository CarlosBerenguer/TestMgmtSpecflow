using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestJamaTestMgmt.Test_Management.Login;

namespace TestJamaTestMgmt.Config
{
    [CollectionDefinition(nameof(AutomationWebTestFixture))]

    public class AutomationWebFixtureCollection: ICollectionFixture<AutomationWebTestFixture> { }

    public class AutomationWebTestFixture
    {
        public SeleniumHelper BrowserHelper;
        public readonly ConfigurationHelper Configuration;
        public User User;

        public AutomationWebTestFixture()
        {
            User = new User();
            Configuration = new ConfigurationHelper();
            BrowserHelper = new SeleniumHelper(Browser.Chrome, Configuration, false);
        }

    }
}
