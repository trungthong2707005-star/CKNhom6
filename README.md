# CKNhom6

📘 Contact Management System (Bài Thi Cuối Kỳ)

Dự án quản lý danh bạ đơn giản gồm RESTful API (ASP.NET Core) và Front-end (HTML/JS).

🚀 Công nghệ sử dụng

.Back-end: .NET 8 Web API

.Front-end: HTML5, CSS3, JavaScript thuần

.Deployment: IIS (Internet Information Services)

🛠️ Yêu cầu hệ thống

.NET 8 SDK

.IIS Web Server (đã bật tính năng)

.NET Core Hosting Bundle 8.0 (để chạy trên IIS)

⚙️ Hướng dẫn cài đặt & Chạy

1. Back-end (API)

Cách 1: Chạy bằng Visual Studio Code
cd nhom6CK
dotnet restore
dotnet run
# API sẽ chạy tại: http://localhost:5xxx hoặc http://localhost:8080 (tùy cấu hình)
Cách 2: Chạy trên IIS (Production)

1.Publish dự án: dotnet publish -c Release -o ./publish

2.Mở IIS Manager -> Add Website -> Trỏ về thư mục publish.

3.Chọn App Pool -> Chỉnh .NET CLR Version thành No Managed Code.

4.Truy cập: http://localhost:8080/api/contacts

2. Front-end

1.Vào thư mục ClientApp.

2.Mở file script.js, cập nhật biến URL trùng với port của API (ví dụ: 8080).

3.Deploy lên IIS với port 8081 hoặc mở trực tiếp file index.html.
📋 Danh sách API

Method                    Endpoint                    Mô tả                          Auth

GET                       /api/contacts               Lấy danh sách, tìm kiếm        Public

POST                      /api/contacts               Thêm liên hệ mới               Key

PUT                       /api/contacts/{id}          Cập nhật liên hệ               Key

DELETE                    /api/contacts/{id}          Xóa liên hệ                    Key

       
       Note: API Key mặc định là 123456







