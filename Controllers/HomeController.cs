using dov.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.IO;

namespace dov.Controllers
{
    public class HomeController : Controller
    {
        QLHHDataContext db = new QLHHDataContext();
        private object _db;

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult dangki()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult dangki(FormCollection collection)
        {
            var MaNv = collection["MaNV"];
            var TenNV = collection["TenNV"];
            var TenDN = collection["TenTK"];
            var MK = collection["MatKhau"];
            var NhapMK = collection["NhapMK"];
            var SDT = collection["SDT"];
            var GioiTinh = collection["GioiTinh"];
            var Ngaysinh = collection["Ngaysinh"];
            var DiaChi = collection["DiaChi"];
            var LuongNV = collection["LuongNV"];
            var NgayLam = collection["NgayVaoLam"];
            var cv = collection["Chucvu"];
            //var MaNV = collection["MaNV"];

            if (MK != NhapMK)
            {
                ViewData["Err"] = "Nhập lại mật khẩu không đúng";
                return View();
            }

            NhanVien nv1 = new NhanVien();

            nv1.MaNV = MaNv;
            nv1.TenNV = TenNV;
            nv1.SDT = SDT;
            nv1.GioiTinh = GioiTinh;
            nv1.NgaySinh = DateTime.Parse(Ngaysinh);
            nv1.DiaChi = DiaChi;
            nv1.NgayVaoLam = DateTime.Parse(NgayLam);
            nv1.TenTK = TenDN;
            nv1.LuongNV = Int32.Parse(LuongNV);
            nv1.MatKhau = MK;
            nv1.MaCV = cv;

            db.NhanViens.InsertOnSubmit(nv1);

            db.SubmitChanges();

            return RedirectToAction("thucdon", "Home");
        }
        [HttpGet]
        public ActionResult dangnhap()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult dangnhap(FormCollection collection)
        {
            var dn = collection["TenTK"];
            var mk = collection["MatKhau"];
            //var cv = collection["MaCV"];


            var a = from b in db.NhanViens
                    where Equals(b.TenTK, dn) && Equals(b.MatKhau, mk)
                    select b;
            foreach (var c in a)
            {

                Session["Login"] = c.TenTK;

                return RedirectToAction("themnl", "Home");



            }
            ViewData["Err"] = "Đăng nhập thất bại";
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("dangnhap", "Home");
        }


        // load thực đơn
        [HttpGet]

        public ActionResult thucdon()
        {
            return View();
        }
        public ActionResult getMenu()
        {
            var v = from t in db.ThucDons
                    select t;
            return PartialView(v.ToList());
        }
        //[HttpPost, ValidateInput(false)]

        //  public ActionResult thucdoni(FormCollection collection, ThucDon add, HttpPostedFileBase fileupload)
        //  {
        //      var MaTD = collection["MaTD"];

        //      var TenLoaiMonAn = collection["TenLoaiMonAn"];
        //      var TenMonAn = collection["TenMonAn"];
        //      var DonGia = Int32.Parse("DonGia");
        //      var Hinh = collection["Hinh"];

        //      //var ID_ROLE = collection["ID_ROLE"];
        //      //var HINH = collection["HINH"];

        //      if (String.IsNullOrEmpty(TenLoaiMonAn))
        //      {
        //          ViewData["Loi1"] = "Vui long nhap ten loai mon an"; return View();
        //      }
        //      else if (String.IsNullOrEmpty(TenMonAn))
        //      {
        //          ViewData["Loi1"] = "Vui long nhap ten mon an"; return View();
        //      }
        //      else if (String.IsNullOrEmpty(DonGia))
        //      {
        //          ViewData["Loi1"] = "Vui long nhap don gia"; return View();
        //      }

        //      else
        //      {
        //          if (ModelState.IsValid)
        //          {
        //              if (fileupload == null) { ViewData["Loi8"] = "Nhập hình ảnh"; return View(); }
        //              var filename1 = DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetFileName(fileupload.FileName);
        //              var path1 = Path.Combine(Server.MapPath("~/Content/img"), filename1);
        //              if (System.IO.File.Exists(path1))
        //              {
        //                  ViewData["Err1"] = "(*) Hình ảnh đã tồn tại !";
        //                  return View();
        //              }
        //              else
        //              {



        //                  add.TenLoaiMonAn = TenLoaiMonAn;
        //                  add.TenMonAn = TenMonAn;
        //                  add.DonGia = DonGia;
        //                  add.Hinh = filename1;

        //                  db.SubmitChanges();
        //                  fileupload.SaveAs(path1);
        //                  ViewData["Err1"] = "Đăng thành công";

        //              }
        //          }
        //      }
        //      return RedirectToAction("Home");
        //  }


        ////Ket thuc





        public ActionResult themnl()
        {
            //LoadNguyenLieu();
            return View(db.NguyenLieus.ToList());
        }
        //[NonAction]
        ////public ActionResult LoadNguyenLieu()
        ////{
        ////    IList<NguyenLieu> nl = new List<NguyenLieu>();
        ////    var a = from b in db.NguyenLieus
        ////            select b;
        ////    foreach (var c in a)
        ////    {
        ////        nl.Add(new NguyenLieu
        ////        {
        ////            MaNV = c.MaNV,
        ////            MaNguyenLieu = c.MaNguyenLieu,
        ////            TenNguyenLieu = c.TenNguyenLieu,
        ////            SL = c.SL,
        ////            DVT = c.DVT,
        ////            Gia = c.Gia
        ////        });

        ////    }
        ////    return View(nl);

        ////}


        [HttpGet]
        public ActionResult suanl(string idne)
        {
            if (idne == null) return HttpNotFound();
            var a = from b in db.NguyenLieus
                    where Equals(b.MaNguyenLieu, idne)
                    select b;
            return View(a.ToList());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult suanl(FormCollection collect)
        {
            var idne = collect["idne"];
            if (String.IsNullOrEmpty(idne)) return HttpNotFound();

            var MaNV = collect["MaNV"];
            var TenNguyenLieu = collect["TenNguyenLieu"];
            var DonViTinh = collect["DonViTinh"];
            var SL = collect["SL"];
            var Gia = collect["Gia"];
            if (String.IsNullOrEmpty(MaNV))
            {
                ViewBag.Message = "Vui lòng nhập Mã NV";
                return View();
            }
            var a = from b in db.NguyenLieus
                    where Equals(b.MaNguyenLieu, idne)
                    select b;
            foreach (var c in a)
            {
                c.MaNV = MaNV;
                c.TenNguyenLieu = TenNguyenLieu;
                c.DVT = DonViTinh;
                c.SL = SL;
                c.Gia = double.Parse(Gia);
            }
            db.SubmitChanges();
            ViewBag.Message = "Cập nhật thành công";
            return View(a.ToList());
        }
        public ActionResult xoanl(string idne)
        {
            if (idne == null) return HttpNotFound();
            var a = from b in db.NguyenLieus
                    where Equals(b.MaNguyenLieu, idne)
                    select b;
            foreach (var c in a)
                db.NguyenLieus.DeleteOnSubmit(c);
            db.SubmitChanges();
            return RedirectToAction("themnl", "home");
        }
    }


}