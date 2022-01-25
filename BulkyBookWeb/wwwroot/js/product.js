var dataTable;

$(document).ready(function () {
   loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Product/GetAll"
        },
        "columns": [
            { "title": "Title", "data": "title", "width": "15%"},
            { "title": "ISBN", "data": "isbn", "width": "15%"},
            { "title": "Price", "data": "price", "width": "15%"},
            { "title": "Author", "data": "author", "width":"15%"},
            { "title": "Category", "data": "category.name", "width":"15%"},
        ]
    });
}