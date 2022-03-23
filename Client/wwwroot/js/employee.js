
function addEmployee() {
    let obj = new Object();

    obj.FirstName = $("#FirstName").val();
    obj.LastName = $("#LastName").val();
    obj.PhoneNumber = $("#PhoneNumber").val();

    var listGender = document.getElementsByName('genderRadio');
    for (var i = 0; i < listGender.length; i++) {
        if (listGender[i].checked) {
            obj.gender = parseInt(listGender[i].value);
            break;
        }
    }
    obj.BirthDate = $("#DateOfBirth").val();
    obj.Salary = Number($("#Salary").val());
    obj.Email = $("#Email").val();
    obj.Password = $("#Password").val();
    obj.universityId = Number($("#inputUniversities").val());
    obj.Degree = $("#Degree").val();
    obj.GPA = $("#Gpa").val();
    obj.RoleId = Number($("#inputRole").val());

    console.log("Sebelum masuk nih");
    console.log(obj);

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type':'application/json'
        },
        type: "POST",
        url: "https://localhost:44300/api/accounts/register",
        dataType:"json",
        data: JSON.stringify(obj)
    }).done((result) => {
        console.log(result);

    }).fail((error) => {
        //alert pemberitahuan jika gagal
        console.log(error);

    })
}




$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "https://localhost:44300/api/roles",
        data: {}
    }).done((result) => {
        var textRoles = `<option value="hide" style="display: none;">Pick role</option>`;
        $.each(result, function (key, val) {

            textRoles += `<option value="${result[key].id}">${result[key].jobRole}</option>`;
        });
        $('#inputRole').html(textRoles);
    }).fail((err) => {
        console.log(err);
    });

    $.ajax({
        type: "GET",
        url: "https://localhost:44300/api/universities",
        data: {}
    }).done((result) => {
        var textUniversities = `<option value="hide" style="display: none;">Pick universities</option>`;
        $.each(result, function (key, val) {

            textUniversities += `<option value="${result[key].id}">${result[key].name}</option>`;
        });
        $('#inputUniversities').html(textUniversities);
    }).fail((err) => {
        console.log(err);
    });



    $("#employeeDatatable").DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excel',
                exportOptions: {
                    columns:[0,1,2,3,4,5]
                }
            }
        ],
        "ajax": {
            "url": "https://localhost:44300/API/Employees/MasterEmployeeData",
            "dataSrc":"",
        },
        "columns": [
            {
                "data": null, "sortable": false,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "nik" },
            { "data": "fullName" },
            { "data": "role" },
            { "data": "email" },
            { "data": "phone" },
            { "data": "gender" },
            {
                "data": null,
                render: function (data, type, row) {
                    var btnDetail = `<a href="#costumModal8" role="button" class="btn btn-info" data-toggle="modal" title="Show Detail"> <i class="fa fa-info" aria-hidden="true"></i></a >`;
                    var btnUpdate = `<a href="#costumModal8" role="button" class="btn btn-warning" data-toggle="modal" title="Update data" style="color:white"> <i class="fa fa-pencil-square-o" aria-hidden="true"></i></a >`;
                    var btnDelete = `<a href="#costumModal8" role="button" class="btn btn-danger" data-toggle="modal" title="Delete"> <i class="fa fa-trash-o" aria-hidden="true"></i></a >`
                    return btnDetail+` ` + btnUpdate+` ` +btnDelete;
                }
            },

        ],
        
    });
})