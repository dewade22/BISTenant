let baseUrl = localStorage.getItem('thisAddress')

$(function () {
    $('#tabelMachine').bootgrid({
        caseSensitive: false,
        formatters: {
            "commands": function (column, row) {
                return "<button type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" onclick=\"PushUpdate(\'" + row.MachineNo + "\',\'" + row.MachineName + "\')\"><span class=\"zmdi zmdi-edit\"></span></button>" +
                    "<button type=\"button\" class=\"btn btn-icon command-delete waves-effect waves-circle\" onclick=\"PushDelete(\'" + row.MachineNo + "\',\'" + row.MachineName + "\')\"><span class=\"zmdi zmdi-delete\"></span></button>"
            },
            "MachinePrice": function (column, row) {
                return `Rp. ${parseFloat(row.MachinePrice).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 5 })}`
            },
            "PowerConsumption": function (column, row) {
                return `${(parseFloat(row.PowerConsumption)).toFixed(3)} KWh`
            }
        }
    })
    //Custom Validation 
    $('#formInput').validate({
        onkeyup: function (element) { $(element).valid() },
        rules: {
            MachineName: {
                required: true,
            },
            MachineType: {
                required: true
            },
            MachinePrice: {
                required: true
            },
            MachineSetupPrice: {
                required : true
            },
            MachineMaintenancePrice: {
                required : true
            },
            MaximumAgeUse: {
                required: true
            },
            SalvageValue: {
                required: true
            },
            PowerConsumption: {
                required: true
            }
        }
    })


    //Jika Button Add di klik
    $('#AddNew').click(function () {
        if ($('#formInput').valid()) {
            if ($('#MachineType').val() == '') {
                $('#errorselect').html('Please Select The Type of Machine !!')
            }
            else {
                let data = $('#formInput').serialize()
                $.ajax({
                    type: 'POST',
                    url: baseUrl + '/HPPItem/MesinMaster',
                    data: data,
                    success: function (result) {
                        if (result == 'sukses') {
                            Swal.fire(
                                'Sukses!',
                                'Data berhasil ditambahkan',
                                'success'
                            ).then((result) => {
                                location.reload()
                            })
                        } else {
                            Swal.fire(
                                'Error!',
                                ''+result,
                                'error'
                            )
                        }
                    }
                })
            }
        }
    })

    //Jika modal di hide
    $('#modalForm').on('hidden.bs.modal', function () {       
        $('#modalForm form')[0].reset()
        $('#MachineType').val('')
        $('#MachineType').trigger('chosen:updated')
    })

    //Untuk Update Form
    $('#updateform').click(function () {
        if ($('#formInput').valid()) {
            if ($('#MachineType').val() == "") {
                $('#errorselect').html('Please Choose Machine Type')
            }
            else {
                Swal.fire({
                    title: 'Update?',
                    text: "Anda Akan Melakukan Update!",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Ya!',
                    cancelButtonText: 'Kembali'
                }).then((result) => {
                    if (result.isConfirmed) {
                        let data = $('#formInput').serialize()
                        $.ajax({
                            type: 'PATCH',
                            url: baseUrl + '/HPPItem/MesinMasters',
                            data: data,
                            success: function (result) {
                                if (result == 'sukses') {
                                    Swal.fire(
                                        'Sukses!',
                                        'Data berhasil dirubah',
                                        'success'
                                    ).then((result) => {
                                        location.reload()
                                    })
                                }
                                else {
                                    Swal.fire(
                                        'Error!',
                                        result,
                                        'error'
                                    )
                                }
                            }
                        })
                    }
                })
            }
        }
    })

})

function AddNewMachine() {
    $('#modalForm').modal()
    $('#updateform').hide()
    $('#AddNew').show()
    $('.modal-title').html('Add New Machine')
    //remove toggle
    $('.fg-line').removeClass('fg-toggled')
}

function PushUpdate(MachineNo, MachineName) {
    $('#modalForm').modal()
    $('#updateform').show()
    $('#AddNew').hide()
    $('.modal-title').html(`Edit - ${MachineName}`)
    $.ajax({
        type: 'GET',
        url: baseUrl + '/HPPItem/MesinMasters?MachineNo=' + MachineNo,
        success: function (result) {
            if (result == false) {
                Swal.fire(
                    'Error!',
                    'Gagal menemukan data ' + MachineNo,
                    'error'
                )
            }
            else {
                $('#Id').val(result.id)
                $('#MachineNo').val(result.machineNo)
                $('#MachineName').val(result.machineName)
                $('#MachineType').val(result.machineType)
                $('#MachinePrice').val(result.machinePrice)
                $('#MachineSetupPrice').val(result.machineSetupPrice)
                $('#MachineMaintenancePrice').val(result.machineMaintenancePrice)
                $('#MaximumAgeUse').val(result.maximumAgeUse)
                $('#SalvageValue').val(result.salvageValue)
                $('#MachineSpeed').val(result.machineSpeed)
                $('#PowerConsumption').val(result.powerConsumption)
                $('#MachineType').trigger('chosen:updated')
                //Togle input form
                $('.fg-line').addClass('fg-toggled')
            }
        }
    })
}

function PushDelete(MachineNo, MachineName) {
    Swal.fire({
        title: 'Hapus?',
        text: "Anda Akan Menghapus "+MachineName+"!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya!',
        cancelButtonText: 'Kembali'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type : 'PATCH',
                url: baseUrl + '/HPPItem/MesinMaster?MachineNo=' + MachineNo,
                success: function (result) {
                    if (result == true) {
                        Swal.fire(
                            'Sukses!',
                            'Mesin ' + MachineName + ' berhasil dihapus',
                            'success'
                        ).then((result) => {
                            location.reload()
                        })
                    } else {
                        Swal.fire(
                            'Error',
                            'Terjadi kesalahan saat mencoba menghapus data',
                            'error'
                        )
                    }
                }
            })
        }
    })
}