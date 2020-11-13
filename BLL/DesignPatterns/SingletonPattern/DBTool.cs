using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DesignPatterns.SingletonPattern
{
    public class DBTool
    {
        private DBTool()
        {

        }

        private static MyContext _dbInstance;
        public static MyContext DBInstance
        {
            get
            {
                if (_dbInstance is null)
                {
                    _dbInstance = new MyContext();
                }
                return _dbInstance;
            }
        }

    }
}
