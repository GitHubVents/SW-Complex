using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwSpecification
{
    class ConnectSqlString
    {

        public string Con = @"Data Source=" + Properties.Settings.Default.ComboBoxIP + @";Initial Catalog=SWPlusDB;Persist Security Info=True;User ID=sa;Password=PDMadmin;MultipleActiveResultSets=True;Pooling=True"; 

    }
}
