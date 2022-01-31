var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    }
    else {
        if (url.includes("completed")) {
            loadDataTable("completed");
        }
        else {
            if (url.includes("pending")) {
                loadDataTable("pending");
            }
            else {
                if (url.includes("approved")) {
                    loadDataTable("approved");
                }
                else {
                    loadDataTable("all");
                }
            }
        }
    }
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Order/GetAll?status=" + status
        },
        "columns": [
            { "title": "ID", "data": "id", "width": "5%" },
            { "title": "Name", "data": "name", "width": "25%" },
            { "title": "Phone Number", "data": "phoneNumber", "width": "15%" },
            { "title": "Email", "data": "applicationUser.email", "width": "15%" },
            { "title": "Order Status", "data": "orderStatus", "width": "15%" },
            { "title": "Order Total", "data": "orderTotal", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Order/Details?orderId=${data}"
                            class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i></a>
                            </div>
                        `
                },
                "width": "10%"
            }
        ]
    });
}