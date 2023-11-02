﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CNPMNC_REPORT1.Models;
using System.Net;
using System.Net.Mail;
using System.Collections;

namespace CNPMNC_REPORT1.Controllers
{
    public class StoreController : Controller
    {
        SQLData data = new SQLData();
        // GET: Store
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                string tentk = Session["Username"].ToString();
                ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                   
            }


            return View();
        }
        [HttpPost]
        public ActionResult Index(string status, int? MaVe, int? TongGia, int? ThanhTien)
        {
            if (Session["Username"] != null)
            {
                string tentk = Session["Username"].ToString();
                ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");

            }

            if (status == "Delete")
            {
                string tentk = Session["Username"].ToString();
                if (MaVe != null)
                {
                    bool isDelete = data.deleteVP(MaVe);
                    if (isDelete)
                    {
                        ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                        ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");

                    }
                }
            }
            else if (status == "ThanhToan")
            {
                string tentk = Session["Username"].ToString();
                if (TongGia != null && ThanhTien != null)
                {
                    bool isAdd = data.insertHD(TongGia, tentk);
                    if (isAdd)
                    {
                        bool isAddCT = false;
                        ViewBag.GetMaVP = data.getData($"SELECT vp.MaVe FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH=kh.MaKH AND kh.TenTKKH='{tentk}'");
                        foreach (var b in ViewBag.GetMaVP)
                        {
                            int mave = int.Parse(b[0]);
                            isAddCT = data.insertCTHD(mave, 1, ThanhTien);

                        }

                        if (isAddCT)
                        {
                            bool isUpdate = false;
                            foreach (var b in ViewBag.GetMaVP)
                            {
                                int mave = int.Parse(b[0]);
                                isUpdate = data.updateTTVP(mave);
                            }
                            if (isUpdate)
                            {
                                ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                                ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");

                                return RedirectToAction("ThankYouPage", "Booking");
                            }
                        }
                        else
                        {
                            ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                            ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");

                            ViewBag.ThongBao = "Error add CTHD!";
                        }
                    }
                    else
                    {
                        ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                        ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");

                        ViewBag.ThongBao = "Error add HD!";
                    }
                }
                else
                {
                    ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                    ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");

                }

            }

            return View();
        }

        
    }
}