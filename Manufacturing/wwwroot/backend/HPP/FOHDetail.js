
let baseUrl = localStorage.getItem('thisAddress')
$(function () {
    $('#tabelFOH').bootgrid({
        caseSensitive: false,
        formatters: {
            "commands": function (column, row) {
                return "<button type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" onclick=\"PushUpdate(\'" + row.ModelDetailFOHNo + "\',\'" + row.OperationName + "\',\'" + row.SubProcessName+"\')\"><span class=\"zmdi zmdi-edit\"></span></button>" +
                    "<button type=\"button\" class=\"btn btn-icon command-delete waves-effect waves-circle\" onclick=\"PushDelete(\'" + row.ModelDetailFOHNo + "\',\'" + row.OperationName + "\',\'" + row.SubProcessName +"\')\"><span class=\"zmdi zmdi-delete\"></span></button>"
            },
            "machine": function (column, row) {
                return `${row.Machine == null ? `-` : row.Machine}`
            }
        }
    })

    //Validasi
    $('#formInput').validate({
        onkeyup: function (element) { $(element).valid() }
    })

    //Jika modal di hide
    $('#modalForm').on('hidden.bs.modal', function () {
        $('#modalForm form')[0].reset()
        $('#FOHType').val('')
        $('#SPID').val('')
        $('#FOHType').trigger('chosen:updated')
        $('#SPID').trigger('chosen:updated')
    })

    //Change pada FOH Type
    $('#FOHType').change(function () {
        if ($('#FOHType').val() == '') {
            HideElectricityField()
            $('#text-satuan').html('')
        }
        else if ($('#FOHType').val() == 'electricity') {
            ShowElectricityField()
            $('#text-satuan').html('Machine')
        }
        else {
            HideElectricityField()
            if ($('#FOHType').val() == 'Gas') {
                $('#text-satuan').html('KG')
            }
            else if ($('#FOHType').val() == 'Fuel') {
                $('#text-satuan').html('Liters')
            }
            else {
                $('#text-satuan').html('Liters')
            }
        }
    })

    //Get machine Speed
    $('#SPMachineID').change(function () {
        if ($('#SPMachineID').val() != '') {
            $.ajax({
                type: 'GET',
                url: baseUrl + '/HPPItem/MachineSpeed?machine=' + $('#SPMachineID').val(),
                success: function (result) {
                    if (result != false) {
                        $('#SPSpeed').val(result)
                        $('.text-speed').addClass('fg-toggled')
                        if ($('#SubProcessSize').val() != '' || $('#SubProcessSize').val() != null || $('#SPQuantity').val() != '' || $('#SPQuantity').val() != null) {
                            $('#SPDuration').val($('#SubProcessSize').val() / ($('#SPQuantity').val()*result))
                        }
                    }
                }
            })
        }        
    })

    //Saat Operation Dirubah
    $('#SPID').change(function () {
        if ($('#SPID').val() == 'SUB-00002') {
            $('#text-MixerCapacity').show()
        } else {
            $('#text-MixerCapacity').hide()
        }
    })

    //Process Size text Change
    $('#SubProcessSize').keyup(function () {
        if ($('#SubProcessSize').val() != '' || $('#SubProcessSize').val() != null || $('#SPSpeed').val() != '' || $('#SPSpeed').val() != null || $('#SPQuantity').val() != '' || $('#SPQuantity').val() != null) {
            $('#SPDuration').val($('#SubProcessSize').val() / ($('#SPQuantity').val() * $('#SPSpeed').val()))
        }
    })

    //Quantity Text Change
    $('#SPQuantity').keyup(function () {
        if ($('#SubProcessSize').val() != '' || $('#SubProcessSize').val() != null || $('#SPSpeed').val() != '' || $('#SPSpeed').val() != null || $('#SPQuantity').val() != '' || $('#SPQuantity').val() != null) {
            $('#SPDuration').val($('#SubProcessSize').val() / ($('#SPQuantity').val() * $('#SPSpeed').val()))
        }
    })

    $('#SPSpeed').keyup(function () {
        if ($('#SubProcessSize').val() != '' || $('#SubProcessSize').val() != null || $('#SPSpeed').val() != '' || $('#SPSpeed').val() != null || $('#SPQuantity').val() != '' || $('#SPQuantity').val() != null) {
            $('#SPDuration').val($('#SubProcessSize').val() / ($('#SPQuantity').val() * $('#SPSpeed').val()))
        }
    })

    //Simpan form Add
    $('#AddNew').click(function () {
        if ($('#formInput').valid()) {
            if ($('#FOHType').val() == '') {
                $('#error-fohtype').html('Tipe FOH Wajib Diisi')
            }
            else if ($('#SPID').val() == '') {
                $('#errorsubprocess').html('Sub Process Wajib Diisi')
            }
            else {
                if ($('#FOHType').val() == 'electricity') {
                    if ($('#SPMachineID').val() == '') {
                        $('#errormachine').html('Mesin Wajib Diisi')
                    } else {
                        $.ajax({
                            type: 'POST',
                            url: baseUrl + '/HPPItem/FOHDetail',
                            data: $('#formInput').serialize(),
                            success: function (result) {
                                if (result == 'sukses') {
                                    Swal.fire(
                                        'Sukses!',
                                        'Data berhasil ditambahkan',
                                        'success'
                                    ).then((result) => {
                                        location.reload()
                                    })
                                }
                                else {
                                    Swal.fire(
                                        'Error!',
                                        '' + result,
                                        'error'
                                    )
                                }
                            }
                        })
                    }
                } else {
                    $.ajax({
                        type: 'POST',
                        url: baseUrl + '/HPPItem/FOHDetail',
                        data: $('#formInput').serialize(),
                        success: function (result) {
                            if (result == 'sukses') {
                                Swal.fire(
                                    'Sukses!',
                                    'Data berhasil ditambahkan',
                                    'success'
                                ).then((result) => {
                                    location.reload()
                                })
                            }
                            else {
                                Swal.fire(
                                    'Error!',
                                    '' + result,
                                    'error'
                                )
                            }
                        }
                    })
                }
            }
        }
    })

    //Simpan Form Edit
    $('#updateform').click(function () {
        if ($('#formInput').valid()) {
            if ($('#FOHType').val() == '') {
                $('#error-fohtype').html('Tipe FOH Wajib Diisi')
            }
            else if ($('#SPID').val() == '') {
                $('#errorsubprocess').html('Sub Process Wajib Diisi')
            }
            else {
                if ($('#FOHType').val() == 'electricity') {
                    if ($('#SPMachineID').val() == '') {
                        $('#errormachine').html('Mesin Wajib Diisi')
                    } else {
                        $.ajax({
                            type: 'PUT',
                            url: baseUrl + '/HPPItem/FOHDetails',
                            data: $('#formInput').serialize(),
                            success: function (result) {
                                if (result == 'sukses') {
                                    Swal.fire(
                                        'Sukses!',
                                        'Data berhasil diubah',
                                        'success'
                                    ).then((result) => {
                                        location.reload()
                                    })
                                }
                                else {
                                    Swal.fire(
                                        'Error!',
                                        '' + result,
                                        'error'
                                    )
                                }
                            }
                        })
                    }
                }
                else {
                    $.ajax({
                        type: 'PUT',
                        url: baseUrl + '/HPPItem/FOHDetails',
                        data: $('#formInput').serialize(),
                        success: function (result) {
                            if (result == 'sukses') {
                                Swal.fire(
                                    'Sukses!',
                                    'Data berhasil diubah',
                                    'success'
                                ).then((result) => {
                                    location.reload()
                                })
                            }
                            else {
                                Swal.fire(
                                    'Error!',
                                    '' + result,
                                    'error'
                                )
                            }
                        }
                    })
                }
            }
        }
    })
})

function AddFOH() {
    
    $('#modalForm').modal()
    $('#updateform').hide()
    $('#AddNew').show()
    //remove toggle
    $('.fg-line').removeClass('fg-toggled')
    $.ajax({
        type: 'GET',
        url: baseUrl + '/HPPItem/ProcessSize?ModelId=' + $('#ModelId').val(),
        success: function (result) {
            if (result != false) {
                $('#SubProcessSize').val(result.subProcessSize)
                $('#SPMixerCapacity').val(result.spMixerCapacity)
                $('.SubProcessSize').addClass('fg-toggled')
                $('#MixerCapacity').addClass('fg-toggled')
                //$('#SubProcessSize').prop('readonly', true)
                $('#SPMixerCapacity').prop('readonly', true)
            }
        }
    })
    HideElectricityField()
    $('.modal-title').html('Add New FOH')  
}
function HideElectricityField() {
    $('#text-duration').hide()
    $('#text-speed').hide()
    $('#text-machine').hide()
    $('#text-MixerCapacity').hide()
}
function ShowElectricityField() {
    $('#text-duration').show()
    $('#text-speed').show()
    $('#text-machine').show()
    $('#text-MixerCapacity').show()
    $('#duration').addClass('fg-toggled')
}
function PushUpdate(Id, Name, Process) {
    $('#modalForm').modal()
    $('#updateform').show()
    $('#AddNew').hide()
    $('.modal-title').html(`Edit - ${Name} pada proses ${Process}`)
    $.ajax({
        type: 'GET',
        url: baseUrl + '/HPPItem/FOHDetails?ModelId=' + Id,
        success: function (result) {
            if (result != false) {
                
                $('#ModelId').val(result.modelId)
                $('#ModelDetailFOHNo').val(result.modelDetailFOHNo)
                $('#FOHType').val(result.fohType)
                $('#FOHType').trigger('chosen:updated')
                $('#SPID').val(result.spid)
                $('#SPID').trigger('chosen:updated')
                $('#OperationName').val(result.operationName)
                $('#SubProcessSize').val(result.subProcessSize)
                $('#SPMachineID').val(result.spMachineID)
                $('#SPMachineID').trigger('chosen:updated')
                $('#SPSpeed').val(result.spSpeed)
                $('#SPQuantity').val(result.spQuantity)
                $('#SPDuration').val(result.spDuration)
                $('#SPMixerCapacity').val(result.spMixerCapacity)
                $('#SPMixerCapacity').prop('readonly', true)
                //$('#SubProcessSize').prop('readonly', true)
                $('.fg-line').addClass('fg-toggled')
                if (result.spMachineID == null) {
                   HideElectricityField()
                } else {
                    ShowElectricityField()
                }
                
            } else {
                Swal.fire(
                    'Error!',
                    'Gagal menemukan data ' + Name,
                    'error'
                )
            }
        }
    })
}
function PushDelete(Id, Name, Process) {
    Swal.fire({
        title: 'Hapus?',
        text: "Anda Akan Menghapus " + Name + " Pada Proses "+Process+"!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya!',
        cancelButtonText: 'Kembali'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'PATCH',
                url: baseUrl + '/HPPItem/FOHDetaill?FOHNo=' + Id,
                success: function (result) {
                    if (result == 'sukses') {
                        Swal.fire(
                            'Sukses!',
                            'FOH ' + Name + ' pada proses '+Process+' berhasil dihapus',
                            'success'
                        ).then((result) => {
                            location.reload()
                        })
                    } else {
                        Swal.fire(
                            'Error',
                            ''+result+'',
                            'error'
                        )
                    }
                }
            })
        }
    })
}