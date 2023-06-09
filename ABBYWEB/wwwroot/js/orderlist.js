﻿let table;
$(function () {
    var url = window.location.search;
    if (url.includes("cancelled")) {
        LoadList("cancelled");
    }
    else if (url.includes("ready")) {
        LoadList("ready");
    }
    else if (url.includes("inprocess")) {
        LoadList("inprocess");
    }
    else if (url.includes("completed")) {
        LoadList("completed");
    }
    else {
        LoadList(null);
    }
})

function LoadList(param) {
    $(function () {
        table = new DataTable('#DT_Load', {
            "ajax": {
                "url": "/api/order?status=" + param,
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "id", "width": "15%" },
                { "data": "pickupName", "width": "15%" },
                { "data": "applicationUser.email", "width": "15%" },
                { "data": "orderTotal", "width": "15%" },
                { "data": "pickupTime", "width": "15%" },

                {
                    "data": "id",
                    "render": function (data) {
                        return `<div class="w-75 btn-group">
                    <a href="/Admin/Order/OrderDetails?id=${data}" class="btn btn-success text-white mx-2">Edit</a>
                    
                    </div>`
                    }

                    , "width": "15%"
                }
            ],
            "width": "100%"

        });


    })
}
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {

                        Swal.fire(
                            'Deleted!',
                            'Your file has been deleted.',
                            'success'
                        )
                        table.ajax.reload();
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Something went wrong!',
                            footer: '<a href="">Why do I have this issue?</a>'
                        })
                    }
                }
            })

        }
    })
}
