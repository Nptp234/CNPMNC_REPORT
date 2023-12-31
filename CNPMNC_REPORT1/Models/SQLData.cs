﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections; // Sử dụng Lớp ArrayList để lưu kết quả
using System.Data.SqlClient;// Sử dụng các lớp tương tác CSDL
namespace CNPMNC_REPORT1.Models
{
    public class SQLData
    {
        public static string connectionString = "Server=localhost;Database=CNPMNC_DATA1;Trusted_Connection=True";
        public List<Phim> GetPhimList(string sql)
        {
            ArrayList arrayList = getData(sql);
            List<Phim> list = new List<Phim>();

            foreach (ArrayList row in arrayList)
            {
                Phim phim = new Phim
                {
                    TenPhim = row[1].ToString(),
                    MaPhim = row[0].ToString(),
                    TomTatP = row[2].ToString(),
                    NgayCongChieu = row[3].ToString(),
                    ThoiLuongP = row[4].ToString(),
                    LuotMua = row[5].ToString(),
                    LuotThich = row[6].ToString(),
                    HinhAnh = row[7].ToString(),
                    Trailer = row[8].ToString(),
                    GiaPhim = row[9].ToString(),
                    MaGHT = row[10].ToString(),
                // Điền các thuộc tính khác tương ứng với cột trong kết quả truy vấn
            };

                list.Add(phim);
            }

            return list;
        }

        public bool checkName(string nameCol, string nameTable, string text)
        {
            bool isChecked = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            string sql = $"SELECT * FROM {nameTable} WHERE {nameCol} = @text";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@text", text);

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                isChecked = false;
            }
            else isChecked = true;


            connection.Close();

            return isChecked;
        }

        public ArrayList getData(String sql)
        {
            ArrayList listData = new ArrayList();
            
            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            using (SqlDataReader data = command.ExecuteReader())
            {
                //Với biến data là lưu dữ liệu nguyên một bảng
                //Thì data.Read sẽ lưu bảng dưới dạng là 1 hàng

                while (data.Read())
                {
                    ArrayList row = new ArrayList();
                    for (int i = 0; i < data.FieldCount; i++)
                    {
                        row.Add(data.GetValue(i).ToString());
                    }
                    listData.Add(row);
                }
            }

            connection.Close();


            return listData;
        }

        public bool checkDataUsername(string username)
        {
            bool isChecked = true;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            string sql = "SELECT * FROM KHACHHANG WHERE TenTKKH = @username";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@username", username);

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                isChecked = false;
            }
            else isChecked = true;


            connection.Close();

            return isChecked;
        }
        public bool saveDataUser(string username, string password, string email)
        {
            bool isSaved = true;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "INSERT INTO KHACHHANG VALUES (@username, @pass, @email, 0, 'Actived', 1)";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@pass", password);
            command.Parameters.AddWithValue("@email", email);

            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                isSaved = true;
            }
            else isSaved = false;

            return isSaved;
        }
        public bool checkLoginDataUser(string username, string password)
        {
            bool isChecked = true;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "SELECT * FROM KHACHHANG WHERE TenTKKH = @username AND MatKhauKH = @password";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            
            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                isChecked = true;
            }
            else isChecked = false;

            connection.Close();

            return isChecked;
        }

        public bool saveFilm(string name, string des, string date, int? time, string img, string trailer, int? gia, int? codeght)
        {
            bool isSaved = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "INSERT INTO PHIM VALUES (@name, @des, @date, @time, 0, 0, @img, @trailer, @gia, @codeght)";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@des", des);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@time", time);
            command.Parameters.AddWithValue("@img", img);
            command.Parameters.AddWithValue("@trailer", trailer);
            command.Parameters.AddWithValue("@gia", gia);
            command.Parameters.AddWithValue("@codeght", codeght);

            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                isSaved = true;
            }
            else isSaved = false;

            return isSaved;
        }
        public bool updateFilm(int? code, string name, string des, string date, int? time, string img, string trailer, int? gia, int? codeght)
        {
            bool isUpdate = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "UPDATE PHIM SET TenPhim = @name, TomTatP = @des, NgayCongChieu = @date, ThoiLuongP = @time, HinhAnh = @img, Trailer = @trailer, GiaPhim = @gia, MaGHT = @codeght WHERE MaPhim = @code";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@code", code);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@des", des);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@time", time);
            command.Parameters.AddWithValue("@img", img);
            command.Parameters.AddWithValue("@trailer", trailer);
            command.Parameters.AddWithValue("@gia", gia);
            command.Parameters.AddWithValue("@codeght", codeght);


            ArrayList listname = getData($"SELECT * FROM PHIM WHERE TenPhim = N'{name}'");

            if (listname.Count == 0)
            {
                //xử lý sự kiện khi thay đổi tên
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isUpdate = true;
                }
                else isUpdate = false;
            }
            else
            {
                //xử lý sự kiện khi không thay đổi tên
                ArrayList listcheck = getData($"SELECT * FROM PHIM WHERE MaPhim = {code} and TenPhim = N'{name}'");
                if (listcheck.Count == 1)
                {
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        isUpdate = true;
                    }
                    else isUpdate = false;
                }
                else
                {
                    isUpdate = false;
                }
            }

            return isUpdate;
        }


        public int getMaGHT(string name)
        {
            int ma_loai = 0;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "SELECT * FROM GIOIHANTUOI WHERE TenGHT = @name";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@name", name);

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                //gán giá trị cột MaLPC cho biến ma_loai
                if (sqlDataReader.Read())
                {
                    ma_loai = sqlDataReader.GetInt32(0);
                }
            }
            else ma_loai = 0;

            return ma_loai;
        }


        public bool saveFilmType(string name, string des)
        {
            bool isSaved = false;

            if (name != null && des != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "INSERT INTO THELOAIP VALUES (@name, @des)";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@des", des);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isSaved = true;
                }
                else isSaved = false;

            }

            return isSaved;
        }

        public bool updateFilmTypeDetail(int? code, string name, string des)
        {
            bool isUpdate = false;

            if (name != null && des != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "UPDATE THELOAIP SET TenTL=@name, MoTaTL=@des WHERE MaTL = @code";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@des", des);
                command.Parameters.AddWithValue("@code", code);



                ArrayList listname = getData($"SELECT * FROM THELOAIP WHERE TenTL = N'{name}'");

                if (listname.Count == 0)
                {
                    //xử lý sự kiện khi thay đổi tên
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        isUpdate = true;
                    }
                    else isUpdate = false;
                }
                else
                {
                    //xử lý sự kiện khi không thay đổi tên
                    ArrayList listcheck = getData($"SELECT * FROM THELOAIP WHERE MaTL = {code} and TenTL = N'{name}'");
                    if (listcheck.Count == 1)
                    {
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            isUpdate = true;
                        }
                        else isUpdate = false;
                    }
                    else
                    {
                        isUpdate = false;
                    }
                }

            }

            return isUpdate;
        }


        public bool saveAgeLimit(string name, string des)
        {
            bool isSaved = false;

            if (name != null && des != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "INSERT INTO GIOIHANTUOI VALUES (@name, @des)";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@des", des);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isSaved = true;
                }
                else isSaved = false;

            }

            return isSaved;
        }
        public bool updateAgeLimit(int? code, string name, string des)
        {
            bool isUpdate = false;

            if (name != null && des != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "UPDATE GIOIHANTUOI SET TenGHT=@name, MoTaGHT=@des WHERE MaGHT = @code";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@des", des);
                command.Parameters.AddWithValue("@code", code);



                ArrayList listname = getData($"SELECT * FROM GIOIHANTUOI WHERE TenGHT = N'{name}'");

                if (listname.Count == 0)
                {
                    //xử lý sự kiện khi thay đổi tên
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        isUpdate = true;
                    }
                    else isUpdate = false;
                }
                else
                {
                    //xử lý sự kiện khi không thay đổi tên
                    ArrayList listcheck = getData($"SELECT * FROM GIOIHANTUOI WHERE MaGHT = {code} and TenGHT = N'{name}'");
                    if (listcheck.Count == 1)
                    {
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            isUpdate = true;
                        }
                        else isUpdate = false;
                    }
                    else
                    {
                        isUpdate = false;
                    }
                }

            }

            return isUpdate;
        }

        public bool saveRoomType(string name, string des)
        {
            bool isSaved = false;

            if (name != null && des != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "INSERT INTO LOAIPC VALUES (@name, @des)";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@des", des);

                bool isCheck = checkRoomTypeName(name);
                if (isCheck)
                {
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        isSaved = true;
                    }
                    else isSaved = false;
                }
                else return isCheck;

            }

            return isSaved;
        }

        public bool checkRoomTypeName(string name)
        {
            bool isCheck = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "SELECT * FROM LOAIPC WHERE TenLPC = @name";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@name", name);

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                isCheck = false;
            }
            else isCheck = true;

            return isCheck;
        }

        public bool updateRoomTypeDetail(int? code, string name, string des)
        {
            bool isUpdate = false;

            if (name != null && des != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "UPDATE LOAIPC SET TenLPC=@name, MoTaLPC=@des WHERE MaLPC = @code";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@des", des);
                command.Parameters.AddWithValue("@code", code);



                ArrayList listname = getData($"SELECT * FROM LOAIPC WHERE TenLPC = N'{name}'");

                if (listname.Count == 0)
                {
                    //xử lý sự kiện khi thay đổi tên
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        isUpdate = true;
                    }
                    else isUpdate = false;
                }
                else
                {
                    //xử lý sự kiện khi không thay đổi tên
                    ArrayList listcheck = getData($"SELECT * FROM LOAIPC WHERE MaLPC = {code} and TenLPC = N'{name}'");
                    if (listcheck.Count == 1)
                    {
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            isUpdate = true;
                        }
                        else isUpdate = false;
                    }
                    else
                    {
                        isUpdate = false;
                    }
                }

            }

            return isUpdate;
        }

        public bool saveRoom(string name, int? number1, int? number2, int? number3)
        {
            bool isSaved = false;

            if (name != null && number1 > 0 && number2 > 0 && number3 > 0)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "INSERT INTO PHONGCHIEU VALUES (@name, @number1, @number2, @number3)";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@number1", number1);
                command.Parameters.AddWithValue("@number2", number2);
                command.Parameters.AddWithValue("@number3", number3);

                bool isCheck = checkRoomName(name);
                if (isCheck)
                {
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        isSaved = true;
                    }
                    else isSaved = false;
                }
                else return isCheck;

            }

            return isSaved;
        }

        public bool checkRoomName(string name)
        {
            bool isCheck = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "SELECT * FROM PHONGCHIEU WHERE TenPC = @name";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@name", name);

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                isCheck = false;
            }
            else isCheck = true;

            return isCheck;
        }

        public int getMaLoaiPC(string name)
        {
            int ma_loai = 0;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "SELECT * FROM LOAIPC WHERE TenLPC = @name";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@name", name);

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                //gán giá trị cột MaLPC cho biến ma_loai
                if (sqlDataReader.Read())
                {
                    ma_loai = sqlDataReader.GetInt32(0);
                }
            }
            else ma_loai = 0;

            return ma_loai;
        }

        public bool updateRoom(int? code, string name, int? number1, int? number2, int? number3)
        {
            bool isUpdate = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "UPDATE PHONGCHIEU SET TenPC=@name, SLGheThuong=@num1, SLGheVIP=@num2, MaLPC=@num3 WHERE MaPC = @code";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@code", code);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@num1", number1);
            command.Parameters.AddWithValue("@num2", number2);
            command.Parameters.AddWithValue("@num3", number3);

            ArrayList listname = getData($"SELECT * FROM PHONGCHIEU WHERE TenPC = N'{name}'");

            if (listname.Count == 0)
            {
                //xử lý sự kiện khi thay đổi tên
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isUpdate = true;
                }
                else isUpdate = false;
            }
            else
            {
                //xử lý sự kiện khi không thay đổi tên
                ArrayList listcheck = getData($"SELECT * FROM PHONGCHIEU WHERE MaPC = {code} and TenPC = N'{name}'");
                if (listcheck.Count == 1)
                {
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        isUpdate = true;
                    }
                    else isUpdate = false;
                }
                else
                {
                    isUpdate = false;
                }
            }

            connection.Close();

            return isUpdate;
        }

        public bool saveTheLoaiVaPhim(int? number1, int? number2)
        {
            bool isSaved = false;

            if (number1 != null && number2 != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "INSERT INTO TL_P VALUES (@number1, @number2)";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@number1", number1);
                command.Parameters.AddWithValue("@number2", number2);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isSaved = true;
                }
                else isSaved = false;

            }

            return isSaved;
        }

        public bool updateTheLoaiVaPhim(int? code, int? number1, int? number2)
        {
            bool isUpdate = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "UPDATE TL_P SET MaPhim = @number1, MaTL = @number2 WHERE MaTLP = @code";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@code", code);
            command.Parameters.AddWithValue("@number1", number1);
            command.Parameters.AddWithValue("@number2", number2);

            ArrayList listname = getData($"SELECT * FROM TL_P WHERE MaPhim={number1} and MaTL={number2}");

            if (listname.Count == 0)
            {
                //xử lý sự kiện khi thay đổi tên
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isUpdate = true;
                }
                else isUpdate = false;
            }
            else
            {
                isUpdate = false;
            }

            connection.Close();

            return isUpdate;
        }

        public bool saveKHType(string name, double des)
        {
            bool isSaved = false;

            if (name != null && des != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "INSERT INTO LOAIKH VALUES (@name, @des)";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@des", des);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isSaved = true;
                }
                else isSaved = false;

            }

            return isSaved;
        }

        public bool updateKHType(int? code, string name, double des)
        {
            bool isUpdate = false;

            if (name != null && des != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "UPDATE LOAIKH SET TenLKH=@name, ChietKhau=@des WHERE MaLoaiKH = @code";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@des", des);
                command.Parameters.AddWithValue("@code", code);



                ArrayList listname = getData($"SELECT * FROM LOAIKH WHERE TenLKH = N'{name}'");

                if (listname.Count == 0)
                {
                    //xử lý sự kiện khi thay đổi tên
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        isUpdate = true;
                    }
                    else isUpdate = false;
                }
                else
                {
                    //xử lý sự kiện khi không thay đổi tên
                    ArrayList listcheck = getData($"SELECT * FROM LOAIKH WHERE MaLoaiKH = {code} and TenLKH = N'{name}'");
                    if (listcheck.Count == 1)
                    {
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            isUpdate = true;
                        }
                        else isUpdate = false;
                    }
                    else
                    {
                        isUpdate = false;
                    }
                }

            }

            return isUpdate;
        }

        public bool saveNVType(string name)
        {
            bool isSaved = false;

            if (name != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "INSERT INTO LAOINV VALUES (@name)";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isSaved = true;
                }
                else isSaved = false;

            }

            return isSaved;
        }

        public bool updateNVType(int? code, string name)
        {
            bool isUpdate = false;

            if (name != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "UPDATE LAOINV SET TenLoaiNV = @name WHERE MaLoaiNV = @code";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@code", code);



                ArrayList listname = getData($"SELECT * FROM LAOINV WHERE TenLoaiNV = N'{name}'");

                if (listname.Count == 0)
                {
                    //xử lý sự kiện khi thay đổi tên
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        isUpdate = true;
                    }
                    else isUpdate = false;
                }
                else
                {
                    //xử lý sự kiện khi không thay đổi tên
                    ArrayList listcheck = getData($"SELECT * FROM LAOINV WHERE MaLoaiNV = {code} and TenLoaiNV = N'{name}'");
                    if (listcheck.Count == 1)
                    {
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            isUpdate = true;
                        }
                        else isUpdate = false;
                    }
                    else
                    {
                        isUpdate = false;
                    }
                }

            }

            return isUpdate;
        }

        public int getMaLoaiKH(string name)
        {
            int ma_loai = 0;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "SELECT * FROM LOAIKH WHERE TenLKH = @name";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@name", name);

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                //gán giá trị cột MaLPC cho biến ma_loai
                if (sqlDataReader.Read())
                {
                    ma_loai = sqlDataReader.GetInt32(0);
                }
            }
            else ma_loai = 0;

            return ma_loai;
        }

        public bool saveKH(string name, string pass, string email, int? diem, string trangthai, int? maloai)
        {
            bool isSaved = false;

            if (name != null && pass != null && email != null && diem != null && trangthai != null && maloai != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "INSERT INTO KHACHHANG VALUES (@name, @pass, @email, @diem, @trangthai, @maloai)";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@pass", pass);
                command.Parameters.AddWithValue("@diem", diem);
                command.Parameters.AddWithValue("@trangthai", trangthai);
                command.Parameters.AddWithValue("@maloai", maloai);
                command.Parameters.AddWithValue("@email", email);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isSaved = true;
                }
                else isSaved = false;

            }

            return isSaved;

        }

        public bool updateKH(int? code, string name, string pass, string email, int? diem, string trangthai, int? maloai)
        {
            bool isUpdate = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "UPDATE KHACHHANG SET TenTKKH=@name, MatKhauKH=@pass, EmailKH=@email, DiemThuongKH=@diem, TrangThaiTKKH=@trangthai, MaLoaiKH=@maloai WHERE MaKH=@code";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@code", code);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@pass", pass);
            command.Parameters.AddWithValue("@diem", diem);
            command.Parameters.AddWithValue("@trangthai", trangthai);
            command.Parameters.AddWithValue("@maloai", maloai);
            command.Parameters.AddWithValue("@email", email);

            ArrayList listname = getData($"SELECT * FROM KHACHHANG WHERE TenTKKH = N'{name}'");

            if (listname.Count == 0)
            {
                //xử lý sự kiện khi thay đổi tên
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isUpdate = true;
                }
                else isUpdate = false;
            }
            else
            {
                //xử lý sự kiện khi không thay đổi tên
                ArrayList listcheck = getData($"SELECT * FROM KHACHHANG WHERE MaKH = {code} and TenTKKH = N'{name}'");
                if (listcheck.Count == 1)
                {
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        isUpdate = true;
                    }
                    else isUpdate = false;
                }
                else
                {
                    isUpdate = false;
                }
            }

            connection.Close();

            return isUpdate;
        }

        public int getMaLoaiNV(string name)
        {
            int ma_loai = 0;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "SELECT * FROM LAOINV WHERE TenLoaiNV = @name";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@name", name);

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                //gán giá trị cột MaLPC cho biến ma_loai
                if (sqlDataReader.Read())
                {
                    ma_loai = sqlDataReader.GetInt32(0);
                }
            }
            else ma_loai = 0;

            return ma_loai;
        }

        public bool saveNV(string name, string pass, string email, string trangthai)
        {
            bool isSaved = false;

            if (name != null && pass != null && email != null && trangthai != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "INSERT INTO NHANVIEN VALUES (@name, @email, @pass,  @trangthai)";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@pass", pass);
                command.Parameters.AddWithValue("@trangthai", trangthai);
                command.Parameters.AddWithValue("@email", email);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isSaved = true;
                }
                else isSaved = false;

            }

            return isSaved;

        }

        public bool updateNV(int? code, string name, string pass, string email, string trangthai)
        {
            bool isUpdate = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "UPDATE NHANVIEN SET HoTenNV=@name, MatKhauNV=@pass, Email=@email, TrangThaiTKNV=@trangthai WHERE MaNV=@code";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@code", code);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@pass", pass);
            command.Parameters.AddWithValue("@trangthai", trangthai);
            command.Parameters.AddWithValue("@email", email);

            ArrayList listname = getData($"SELECT * FROM NHANVIEN WHERE HoTenNV = N'{name}'");

            if (listname.Count == 0)
            {
                //xử lý sự kiện khi thay đổi tên
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isUpdate = true;
                }
                else isUpdate = false;
            }
            else
            {
                //xử lý sự kiện khi không thay đổi tên
                ArrayList listcheck = getData($"SELECT * FROM NHANVIEN WHERE MaNV = {code} and HoTenNV = N'{name}'");
                if (listcheck.Count == 1)
                {
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        isUpdate = true;
                    }
                    else isUpdate = false;
                }
                else
                {
                    isUpdate = false;
                }
            }

            connection.Close();

            return isUpdate;
        }

        public bool saveChairType(string name, int? price)
        {
            bool isSaved = false;

            if (name != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "INSERT INTO LOAIGHE VALUES (@name, @price)";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@price", price);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isSaved = true;
                }
                else isSaved = false;

            }

            return isSaved;
        }
        public bool updateChairType(int? code, string name, int? price)
        {
            bool isUpdate = false;

            if (name != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "UPDATE LOAIGHE SET TenLG = @name, GiaLGhe = @price WHERE MaGhe = @code";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@price", price);



                ArrayList listname = getData($"SELECT * FROM LOAIGHE WHERE TenLG = N'{name}'");

                if (listname.Count == 0)
                {
                    //xử lý sự kiện khi thay đổi tên
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        isUpdate = true;
                    }
                    else isUpdate = false;
                }
                else
                {
                    //xử lý sự kiện khi không thay đổi tên
                    ArrayList listcheck = getData($"SELECT * FROM LOAIGHE WHERE MaGhe = {code} and TenLG = N'{name}'");
                    if (listcheck.Count == 1)
                    {
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            isUpdate = true;
                        }
                        else isUpdate = false;
                    }
                    else
                    {
                        isUpdate = false;
                    }
                }

            }

            return isUpdate;
        }

        public bool saveXC(string name, string time)
        {
            bool isSaved = false;

            if (name != null && time != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "INSERT INTO XUATCHIEU VALUES (@name, @time)";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@time", time);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isSaved = true;
                }
                else isSaved = false;

            }

            return isSaved;
        }

        public bool updateXC(int? code, string name, string time)
        {
            bool isUpdate = false;

            if (name != null && time != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "UPDATE XUATCHIEU SET CaXC = @name, GioXC = @time WHERE MaXC = @code";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@time", time);



                ArrayList listname = getData($"SELECT * FROM XUATCHIEU WHERE CaXC = '{name}'");

                if (listname.Count == 0)
                {
                    //xử lý sự kiện khi thay đổi tên
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        isUpdate = true;
                    }
                    else isUpdate = false;
                }
                else
                {
                    //xử lý sự kiện khi không thay đổi tên
                    ArrayList listcheck = getData($"SELECT * FROM XUATCHIEU WHERE MaXC = {code} and CaXC = '{name}'");
                    if (listcheck.Count == 1)
                    {
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            isUpdate = true;
                        }
                        else isUpdate = false;
                    }
                    else
                    {
                        isUpdate = false;
                    }
                }

            }

            return isUpdate;
        }

        public int getMaPhim(string name)
        {
            int ma_loai = 0;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "SELECT * FROM PHIM WHERE TenPhim = @name";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@name", name);

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                //gán giá trị cột MaLPC cho biến ma_loai
                if (sqlDataReader.Read())
                {
                    ma_loai = sqlDataReader.GetInt32(0);
                }
            }
            else ma_loai = 0;

            return ma_loai;
        }

        public int getMaXC(string name)
        {
            int ma_loai = 0;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "SELECT * FROM XUATCHIEU WHERE CaXC = @name";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@name", name);

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                //gán giá trị cột MaLPC cho biến ma_loai
                if (sqlDataReader.Read())
                {
                    ma_loai = sqlDataReader.GetInt32(0);
                }
            }
            else ma_loai = 0;

            return ma_loai;
        }

        public int getMaPC(string name)
        {
            int ma_loai = 0;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "SELECT * FROM PHONGCHIEU WHERE TenPC = @name";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@name", name);

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                //gán giá trị cột MaLPC cho biến ma_loai
                if (sqlDataReader.Read())
                {
                    ma_loai = sqlDataReader.GetInt32(0);
                }
            }
            else ma_loai = 0;

            return ma_loai;
        }

        public bool saveLC(string date, string status, int? amount, int? caxc, int? mapc, int? maphim)
        {
            bool isSaved = false;

            if (date != null && status != null && amount != null && caxc != null && mapc != null && maphim != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "INSERT INTO LICHCHIEU VALUES (@date, @status, @amount, @caxc, @mapc, @maphim)";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@caxc", caxc);
                command.Parameters.AddWithValue("@mapc", mapc);
                command.Parameters.AddWithValue("@maphim", maphim);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isSaved = true;
                }
                else isSaved = false;

            }

            return isSaved;
        }

        public bool updateLC(int? code, string date, string status, int? amount, int? caxc, int? mapc, int? maphim)
        {
            bool isUpdate = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "UPDATE LICHCHIEU SET NgayLC=@date, TrangThaiLC=@status, SLVeDat=@amount, CaXC=@caxc, MaPC=@mapc, MaPhim=@maphim WHERE MaLC = @code";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@code", code);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@status", status);
            command.Parameters.AddWithValue("@amount", amount);
            command.Parameters.AddWithValue("@caxc", caxc);
            command.Parameters.AddWithValue("@mapc", mapc);
            command.Parameters.AddWithValue("@maphim", maphim);


            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                isUpdate = true;
            }
            else isUpdate = false;

            connection.Close();

            return isUpdate;
        }

        public bool deleteVP (int? code)
        {
            bool isDelete = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "DELETE FROM VE_GHE WHERE MaVe = @code DELETE FROM VEPHIM WHERE MaVe = @code";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@code", code);


            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                isDelete = true;
            }
            else isDelete = false;

            connection.Close();

            return isDelete;
        }

        public bool insertHD(int? tonggia, string tentk)
        {
            bool isAdd = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "DECLARE @chietkhau FLOAT, @makh INT " +
                        "SELECT @chietkhau=lkh.ChietKhau FROM KHACHHANG kh, LOAIKH lkh WHERE kh.MaLoaiKH=lkh.MaLoaiKH AND kh.TenTKKH = @tentk " +
                        "SELECT @makh = MaKH FROM KHACHHANG WHERE TenTKKH = @tentk " +
                        "INSERT INTO HOADON VALUES (GETDATE(), @tonggia, @tonggia-(@tonggia*@chietkhau), @chietkhau, @makh, 1)";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@tonggia", tonggia);
            command.Parameters.AddWithValue("@tentk", tentk);

            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                isAdd = true;
            }
            else isAdd = false;

            connection.Close();

            return isAdd;
        }

        public bool insertCTHD(int? mave, int? slve, int? thanhtien)
        {
            bool isAdd = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "DECLARE @mahd INT SELECT @mahd=MAX(MaHD) FROM HOADON INSERT INTO CHITIETHD VALUES (@mave, @mahd, @slve, @thanhtien)";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@mave", mave);
            command.Parameters.AddWithValue("@slve", slve);
            command.Parameters.AddWithValue("@thanhtien", thanhtien);

            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                isAdd = true;
            }
            else isAdd = false;

            connection.Close();

            return isAdd;
        }

        public bool updateTTVP(int? mave)
        {
            bool isUpdate = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "UPDATE VEPHIM SET TrangThaiThanhToan = N'ĐÃ THANH TOÁN' WHERE MaVe = @mave";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@mave", mave);

            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                isUpdate = true;
            }
            else isUpdate = false;

            connection.Close();

            return isUpdate;
        }

        public bool updateTTVG(int? mave)
        {
            bool isUpdate = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "UPDATE VE_GHE SET TrangThaiVG = N'ĐÃ THANH TOÁN' WHERE MaVe = @mave";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@mave", mave);

            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                isUpdate = true;
            }
            else isUpdate = false;

            connection.Close();

            return isUpdate;
        }

        public bool updateLuotMua(int? maphim)
        {
            bool isUpdate = false;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "UPDATE PHIM SET LuotMua=LuotMua+1 WHERE MaPhim = @maphim";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@maphim", maphim);

            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                isUpdate = true;
            }
            else isUpdate = false;

            connection.Close();

            return isUpdate;
        }

        public bool updateKH(string name, string email, string pass)
        {
            bool isUpdate = false;

            if (name != null && email != null && pass != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "UPDATE KHACHHANG SET EmailKH=@email WHERE TenTKKH=@name UPDATE KHACHHANG SET MatKhauKH=@pass WHERE TenTKKH=@name";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@pass", pass);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isUpdate = true;
                }
                else isUpdate = false;

            }

            return isUpdate;
        }

        public bool insertComment(int? idphim, int? idkh, string bltext)
        {
            bool isAdd = false;

            if (idphim != null && bltext != null && idkh != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "INSERT INTO BINHLUAN VALUES (@idphim, @idkh, @bltext, 'Active', GETDATE())  ";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@idphim", idphim);
                command.Parameters.AddWithValue("@idkh", idkh);
                command.Parameters.AddWithValue("@bltext", bltext);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isAdd = true;
                }
                else isAdd = false;

            }

            return isAdd;
        }

        public int getMaKH(string tentkkh)
        {
            int ma_loai = 0;

            //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
            SqlConnection connection = new SqlConnection(connectionString);

            //Đối tượng câu truy vấn thêm dữ liệu sql
            string sql = "SELECT * FROM KHACHHANG WHERE TenTKKH = @tentkkh";

            //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
            SqlCommand command = new SqlCommand(sql, connection);

            //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
            connection.Open();

            command.Parameters.AddWithValue("@tentkkh", tentkkh);

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                //gán giá trị cột MaLPC cho biến ma_loai
                if (sqlDataReader.Read())
                {
                    ma_loai = sqlDataReader.GetInt32(0);
                }
            }
            else ma_loai = 0;

            return ma_loai;
        }

        public bool deleteComment(int? idbl)
        {
            bool isDelete = false;

            if (idbl != null)
            {
                //Đối tương SqlConnection sẽ nhận tham số là thông tin chuỗi kết nối CSDL
                SqlConnection connection = new SqlConnection(connectionString);

                //Đối tượng câu truy vấn thêm dữ liệu sql
                string sql = "DELETE FROM BINHLUAN WHERE MaBL=@idbl";

                //Đối tượng SqlCommand sẽ nhận thông tin là biến connection và câu lệnh sql truyền từ tham số hàm
                SqlCommand command = new SqlCommand(sql, connection);

                //Khai báo mở kết nối vào CSDL hay liên kết đến CSDL
                connection.Open();

                command.Parameters.AddWithValue("@idbl", idbl);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    isDelete = true;
                }
                else isDelete = false;

            }

            return isDelete;
        }

    }
}
