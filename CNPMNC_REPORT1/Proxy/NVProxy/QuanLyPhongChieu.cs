﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Proxy.NVProxy
{
    public class QuanLyPhongChieu : PhanTrangNV
    {
        public override List<string> LayDSTrangTheoNV()
        {
            List<string> managedPages = new List<string>(); // Danh sách các trang quản lý được phân loại

            // Thêm tên các trang quản lý đã phân loại vào danh sách
            managedPages.Add("Room");
            managedPages.Add("RoomType");

            return managedPages;
        }
    }
}