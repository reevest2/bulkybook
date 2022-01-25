var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "title": "Title", "data": "title", "width": "15%" },
            { "title": "ISBN", "data": "isbn", "width": "15%" },
            { "title": "Price", "data": "price", "width": "15%" },
            { "title": "Author", "data": "author", "width": "15%" },
            { "title": "Category", "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Product/Upsert?id=${data}"
                            class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                            <a 
                            class="btn btn-danger mx-2"><i class="bi bi-trash"></i> Delete</a>
                        </div>
                        `
                },
                "width": "15%"
            }
        ]
    });
}