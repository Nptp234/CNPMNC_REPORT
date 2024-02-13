﻿using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryBL
{
    public class SingletonBL : BinhLuanFactory
    {
        private static SingletonBL instance;
        private static readonly object lockObject = new object();

        private SingletonBL()
        {
            // Logic tạo danh sách phim
            SQLConnection db = SQLConnection.Instance;
            string query = "SELECT * FROM BINHLUAN;";
            db.OpenConnection();
            allBL = db.Select<BinhLuan>(query);
            db.CloseConnection();
        }

        public static SingletonBL Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SingletonBL();
                    }
                    return instance;
                }
            }
        }

        public void ResetInstance()
        {
            instance = null;
        }

        public override List<BinhLuan> CreateBL()
        {
            return allBL;
        }
    }
}