
const API_URL = "http://localhost:8080/api/contacts"; // Đổi port theo cấu hình BE của bạn
const API_KEY = "MySecretKey123"; // Key để test chức năng Add/Edit/Delete

let currentPage = 1;

async function loadContacts() {
    const search = document.getElementById('search').value;
    try {
        const res = await fetch(`${API_URL}?page=${currentPage}&pageSize=5&search=${search}`);
        const result = await res.json();
        
        const tbody = document.getElementById('tableBody');
        tbody.innerHTML = '';
        
        result.data.forEach(c => {
            const row = `<tr>
                <td>${c.id}</td>
                <td>${c.name}</td>
                <td>${c.phone}</td>
                <td>${c.email}</td>
                <td>${c.note}</td>
                <td>
                    <button onclick="editContact(${c.id}, '${c.name}', '${c.phone}', '${c.email}', '${c.note}')">Sửa</button>
                    <button onclick="deleteContact(${c.id})">Xóa</button>
                </td>
            </tr>`;
            tbody.innerHTML += row;
        });
        
        document.getElementById('pageInfo').innerText = `Trang ${result.page}`;
    } catch (err) {
        alert("Lỗi kết nối API!");
    }
}

async function saveContact() {
    const id = document.getElementById('contactId').value;
    const data = {
        name: document.getElementById('name').value,
        phone: document.getElementById('phone').value,
        email: document.getElementById('email').value,
        note: document.getElementById('note').value
    };

    const method = id ? "PUT" : "POST";
    const url = id ? `${API_URL}/${id}` : API_URL;

    try {
        const res = await fetch(url, {
            method: method,
            headers: {
                "Content-Type": "application/json",
                "x-api-key": API_KEY // Gửi kèm Key xác thực
            },
            body: JSON.stringify(data)
        });

        if (res.ok) {
            alert("Thành công!");
            clearForm();
            loadContacts();
        } else {
            const err = await res.json();
            document.getElementById('msg').innerText = JSON.stringify(err);
        }
    } catch (e) {
        alert("Lỗi hệ thống");
    }
}

async function deleteContact(id) {
    if(!confirm("Bạn chắc chắn xóa?")) return;
    
    const res = await fetch(`${API_URL}/${id}`, {
        method: "DELETE",
        headers: { "x-api-key": API_KEY }
    });
    
    if(res.ok) loadContacts();
    else alert("Xóa thất bại (Check API Key?)");
}

function editContact(id, name, phone, email, note) {
    // 1. Điền ID vào ô ẩn (để máy biết là đang sửa ai)
    document.getElementById('contactId').value = id;

    // 2. Điền thông tin cũ lên các ô input cho người dùng thấy
    document.getElementById('name').value = name;
    document.getElementById('phone').value = phone;
    document.getElementById('email').value = email;
    document.getElementById('note').value = note;

    // 3. Đổi màu nút Lưu hoặc thông báo nhẹ (Tùy chọn)
    // Không dùng alert nữa cho đỡ phiền
    console.log("Đang sửa ID: " + id);
}

function changePage(delta) {
    if (currentPage + delta > 0) {
        currentPage += delta;
        loadContacts();
    }
}

function clearForm() {
    document.getElementById('contactId').value = '';
    document.getElementById('name').value = '';
    document.getElementById('phone').value = '';
    document.getElementById('email').value = '';
    document.getElementById('note').value = '';
    document.getElementById('msg').innerText = '';
}

// Load ban đầu
loadContacts();
