﻿
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
    obj.Email = $("#email").val();
    obj.Password = $("#password").val();
    obj.universityId = Number($("#inputUniversities").val());
    obj.Degree = $("#Degree").val();
    obj.GPA = $("#Gpa").val();
    obj.RoleId = Number($("#inputRole").val());

    console.log(obj);

    $("#formInsertDataEmployee").validate({
        rules: {
            email: {
                required: true,
                email: true,
            },
            password: {
                required: true,
            },
        },
        messages: {
            email: {
                required: "Please enter a email address",
                email: "Please enter a valid email address"
            },
            password: {
                required: "Please provide a password",
            }
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
    if ($("#formInsertDataEmployee").valid()) {
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            type: "POST",
            url: "https://localhost:44300/api/accounts/register",
            dataType: "json",
            data: JSON.stringify(obj)
        }).done((result) => {
            Swal.fire({
                icon: 'success',
                title: 'Data berhasil ditambahkan',
            }).then((result) => {
                window.location.reload();
            })

        }).fail((error) => {
            console.log(error);

        })
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Gagal menambahkan, silahkan dicek kembali',
            timer: 2000,
        })
    }

    
}

function detailEmployee(nik) {
    $.ajax({
        type: "GET",
        url: "https://localhost:44300/api/employees/MasterEmployeeData/"+nik,
        data: {}
    }).done((result) => {
        $('#employeeNIK').html('<h4 style="font-weight:bolder; line-height: 35px;">' + result[0].nik + '</h4>');
        $('#employeeName').html('<h4 style="font-weight:bolder; line-height: 35px;">' + result[0].fullName + '</h4>');
        $('#employeeRole').html('<h4 style="font-weight:bolder; line-height: 35px;">' + result[0].role + '</h4>');
        $('#employeeEmail').html('<h4 style="font-weight:bolder; line-height: 35px;">' + result[0].email + '</h4>');
        $('#employeeGender').html('<h4 style="font-weight:bolder; line-height: 35px;">' + result[0].gender + '</h4>');
        $('#employeePhone').html('<h4 style="font-weight:bolder; line-height: 35px;">' + result[0].phone + '</h4>');
        $('#employeeSalary').html('<h4 style="font-weight:bolder; line-height: 35px;"> $' + result[0].salary + '</h4>');
        $('#employeeUniversities').html('<h4 style="font-weight:bolder; line-height: 35px;">' + result[0].universityName + '</h4>');
        $('#employeeDegree').html('<h4 style="font-weight:bolder; line-height: 35px;">' + result[0].degree + '</h4>');
        $('#employeeGpa').html('<h4 style="font-weight:bolder; line-height: 35px;">' + result[0].gpa + '</h4>');
    }).fail((err) => {
        console.log(err);
    });

}

function dataUpdate(nik) {
    $.ajax({
        type: "GET",
        url: "https://localhost:44300/api/employees/" + nik,
        data: {}
    }).done((result) => {
        console.log(result);
        $('#updateNik').html('<input id="FormUpdateNik" type="text" class="form-control input-group-sm" name="Nik" placeholder="' + result.nik + '" value="' + result.nik +'" disabled>');
        $('#updateFirstName').html('<input id="FormUpdateFirstName" type="text" class="form-control input-group-sm" name="FirstName" placeholder="' + result.firstName + '" value="' + result.firstName +'" disabled>');
        $('#updateLastName').html('<input id="FormUpdateLastName" type="text" class="form-control input-group-sm" name="LastName" placeholder="' + result.lastName + '" value="' + result.lastName +'" disabled>');
        $('#updatePhoneNumber').html('<input id="FormUpdatePhoneNumber" type="text" class="form-control input-group-sm" name="PhoneNumber" placeholder="' + result.phone + '" value="' + result.phone +'" disabled>');
        gender = result.gender;
        if (gender == 0) {
            var inputMale = `<input type='radio' id="updateMale" name="GenderRadioUpdate" value="0"  disabled checked='checked'>`;
            var labelMale = `<label for='updateMale'>Male</label>`;
            var inputFemale = `<input type='radio' id="updateFemale" name="GenderRadioUpdate" value="1" disabled>`;
            var labelFemale = `<label for='updateFemale'>Female</label>`;
            $(`#updateGender`).html(inputMale + labelMale + inputFemale + labelFemale);
        }
        else {
            var inputMale = `<input type='radio' id="updateMale" name="GenderRadioUpdate" value="0"  disabled>`;
            var labelMale = `<label for='updateMale'>Male</label>`;
            var inputFemale = `<input type='radio' id="updateFemale" name="GenderRadioUpdate" value="1" disabled checked='checked' >`;
            var labelFemale = `<label for='updateFemale'>Female</label>`;
            $(`#updateGender`).html(inputMale + labelMale + inputFemale + labelFemale);
        }
        $('#updateDate').html('<input id="UpdateFormBirthDate" type="text" class="form-control input-group-sm" name="BirthDate" placeholder="' + result.birthDate + '" value="' + result.birthDate + '" disabled>');
        $('#updateSalary').html('<input id="UpdateFormSalary" type="number" class="form-control input-group-sm" name="Salary" placeholder="' + result.salary + '" value="" >');
        $('#updateEmail').html('<input id="UpdateFormEmail" type="text" class="form-control input-group-sm" name="Email" placeholder="' + result.email + '" value="' + result.email + '" disabled>');

    }).fail((err) => {
        console.log(err);
    });
}

function updateEmployee() {
    let obj = new Object();

    obj.Nik = $("#FormUpdateNik").val();
    obj.FirstName = $("#FormUpdateFirstName").val();
    obj.LastName = $("#FormUpdateLastName").val();
    obj.Phone = $("#FormUpdatePhoneNumber").val();

    var listGender = document.getElementsByName('GenderRadioUpdate');
    for (var i = 0; i < listGender.length; i++) {
        if (listGender[i].checked) {
            obj.gender = parseInt(listGender[i].value);
            break;
        }
    }
    obj.BirthDate = $("#UpdateFormBirthDate").val();
    obj.Salary = Number($("#UpdateFormSalary").val());
    obj.Email = $("#UpdateFormEmail").val();

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: "PUT",
        url: "https://localhost:44300/api/Employees/",
        dataType: "json",
        data: JSON.stringify(obj)
    }).done((result) => {
        Swal.fire({
            icon: 'success',
            title: 'Data dengan NIK '+obj.Nik +' berhasil diubah',
        }).then((result) => {
            window.location.reload();
        })

    }).fail((error) => {
        //alert pemberitahuan jika gagal
        console.log(error);

    })
}

function deleteEmployee(nik) {
    Swal.fire({
        title: 'Apakah anda yakin?',
        text: "Data dengan NIK "+nik+" akan dihapus",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya',
        cancelButtonText: 'Tidak',
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: "https://localhost:44300/api/employees/" + nik,
                data: {}
            }).done((result) => {
                Swal.fire(
                    'Deleted!',
                    'Data dengan NIK '+nik+" telah dihapus",
                    'success'
                ).then((result) => {
                    window.location.reload();
                })
            }).fail((err) => {
                console.log(err);
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!'+err,
                })
            });
            
        }
    })

}

$(document).ready(function () {
    //BUAT SELECT OPTION ROLE
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
    //BUAT SELECT OPTION UNIVERSITAS
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

    //BUAT COUNT GENDER DAN CHART
    $.ajax({
        type: "GET",
        url: "https://localhost:44300/api/employees/CountGender",
        data: {}
    }).done((result) => {
        
        var options = {
            labels: [result[0].gender, result[1].gender],
            series: [result[0].count, result[1].count],
            colors: ['#2b5737','#691d57' ],
            chart: {
                type: 'donut',
            },
            responsive: [{
                breakpoint: 480,
                options: {
                    chart: {
                        width: 200
                    },
                    legend: {
                        position: 'bottom'
                    }
                }
            }]
        };

        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();

    }).fail((err) => {
        console.log(err);
    });


    //BUAT DATATABLE
    $("#employeeDatatable").DataTable({
        responsive: true,
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
                    var btnDetail = `<button onclick="detailEmployee(${data.nik})" data-target="#DetailDataModal" data-toggle="modal" class="btn btn-info"  title="Show Detail"> <i class="fa fa-info" aria-hidden="true"></i></button>`;
                    var btnUpdate = `<button onclick="dataUpdate(${data.nik})" data-target="#UpdateDataModal"  data-toggle="modal" class="btn btn-warning" title="Update Data" style="color:white"> <i class="fa fa-pencil-square-o" aria-hidden="true"></i></button >`;
                    var btnDelete = `<button onclick="deleteEmployee(${data.nik})" class="btn btn-danger"  title="Delete"> <i class="fa fa-trash-o" aria-hidden="true"></i></button >`;
                    return btnDetail+` ` + btnUpdate+` ` +btnDelete;
                }
            },

        ],
        
    });
})