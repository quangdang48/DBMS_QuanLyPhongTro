using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BAL
{
    public class BAL_ConnectDatabase
    {
        public static string nameDatabase;
        public static string username;
        public static string password;
        public BAL_ConnectDatabase()
        { 
            
        }
        public static void ConnectDataBase(string dtb,string user,string pass)
        {
            nameDatabase = dtb;
            username = user;
            password = pass;    
            DataAccessLayer db = new DataAccessLayer(nameDatabase,username,password);
        }
        public static bool SuccesConn(out string err)
        {
            err = DataAccessLayer.err;
            if(string.IsNullOrEmpty(DataAccessLayer.err))
            {
                return true;
            }
            return false;
        }
    }
}
