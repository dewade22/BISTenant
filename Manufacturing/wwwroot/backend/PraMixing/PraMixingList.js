
let baseUrl = localStorage.getItem('thisAddress')
$(function () {
    HideErrorMsg()
    $('#tableForm').bootgrid({
        caseSensitive: false,
        formatters: {
            'commands': function (column, row) {
                return `<button type="button" class="btn btn-icon command-edit waves-effect waves-circle" onclick="PushUpdate('${row.Id}', '${row.ItemName}')"><span class="zmdi zmdi-edit"></span></button>` +
                    `<button type="button" class="btn btn-icon command-delete waves-effect" onclick="PushDelete('${row.Id}', '${row.ItemName}')"><span class="zmdi zmdi-delete"></span></button>`
            },
            'Amount': function (column, row) {
                return `${row.ItemType != 'Labour' ? (parseFloat(row.ItemQty) * parseFloat(row.Cost)).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : (parseFloat(row.ItemQty) * parseFloat(row.Cost) * parseFloat(row.Hour)).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
            },
            'cost': function (column, row) {
                return `${parseFloat(row.Cost).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}`
            },
            'Qty': function (column, row) {
                return `${parseFloat(row.ItemQty)}`
            }
        }
    })

    CalculateProcess()


    //Validasi
    $('#formInput #saveProcess').validate({
        onkeyup: function (element) { $(element).valid() }
    })

    //Save Modal
    $('#AddNew').click(function () {
        if ($('#formInput').valid()) {
            if ($('#selectRate').val() == null || $('#selectRate').val() == '') {
                $('#ErrorSelect1').html('Please Select One!!')
            }
            else if ($('#ItemNo').val() == null || $('#ItemNo').val() == '') {
                $('#ErrorSelect2').html('Please Select One!!')
            }
            else {
                $.ajax({
                    type: 'POST',
                    url: baseUrl + '/HPPHelper/SavePramixing',
                    data: $('#formInput').serialize(),
                    success: function (result) {
                        if (result == 'sukses') {
                            Swal.fire(
                                'Sukses',
                                'Data Berhasil Ditambahkan',
                                'success',
                            ).then((hasil) => {
                                window.location.reload()
                            })
                        } else {
                            Swal.fire(
                                'Error !!',
                                ''+result,
                                'error'
                            )
                        }
                    },
                    error: function (jqXHR, exception) {
                        Swal.fire(
                            'ERROR !!',
                            '' + jqXHR.status,
                            'error'
                        )
                    }
                })
            }
        }
    })

    //Jika modal di hide
    $('#modalForm').on('hidden.bs.modal', function () {
        $('#modalForm form')[0].reset()
        $('.chosen').val('')
        $('.chosen').trigger('chosen:updated')

    })

    //Simpan form Update
    $('#updateform').click(function () {
        if ($('#formInput').valid()) {
            if ($('#selectRate').val() == null) {
                $('#ErrorSelect1').html('Please Select One!!')
            }
            else if ($('#ItemNo').val() == null) {
                $('#ErrorSelect2').html('Please Select One!!')
            }
            else {
                 $.ajax({
                    type: 'PUT',
                     url: baseUrl + '/HPPHelper/UpdatePramixing',
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
                        } else {
                            Swal.fire(
                                'Error!',
                                '' + result,
                                'error'
                            )
                        }
                     },
                     error: function (jqXHR) {
                         Swal.fire(
                             'Error..!',
                             'Error ' + jqXHR.status,
                             'error'
                         )
                     }
                 })
            }
        }
    })

    $('#AddItem').click(function () {
        $('#modalForm').modal()
        $('#updateform').hide()
        $('#AddNew').show()
        $('.modal-title').html('Add New Items')
    })

    $('#selectRate').change(function () {
        if ($('#selectRate').val() != null || $('#selectRate').val() != '') {
            $('#itemname').html($('#selectRate option:selected').text())

            AppendItemNo($('#selectRate').val())
        }
    })

    $('#ItemNo').change(function () {
        $('#ItemName').val($('#ItemNo option:selected').text())
    })

    $('#savePramixing').click(function () {
        Swal.fire({
            title: 'Save',
            text: 'Save This Calculation Result ?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ya!',
            cancelButtonText: 'Kembali'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: baseUrl + '/HPPHelper/SaveWIPLineCost',
                    data: $('#saveProcess').serialize(),
                    success: function (result) {
                        if (result == 'sukses') {
                            Swal.fire(
                                'Success !!',
                                'Data Has Been Saved',
                                'success'
                            ).then((result) => {
                                window.location.reload()
                            })
                        } else {
                            Swal.fire(
                                'Error !',
                                'Error ' + result,
                                'error'
                            )
                        }
                    },
                    error: function (jqXHR) {
                        Swal.fire(
                            'Error !',
                            'Error ' + jqXHR.status,
                            'error'
                        )
                    }
                })
            }
        })
        
    })
})

function AppendItemNo(ItemType, ItemNo='') {
    $('#ItemNo').empty()
    $('#ItemNo').append(`<option value="">Choose One</option>`)
    $.ajax({
        type: 'GET',
        url: baseUrl + '/HPPHelper/SelectItem?No=' + ItemType,
        success: function (result) {
            result.forEach(function (result) {
                $('#ItemNo').append(`<option value="${result.valueCode}">${result.valueName}</option>`)
            })
            $('#ItemNo').val(ItemNo)
            $('.chosen').trigger('chosen:updated')
        },
        error: function (jqXHR, exception) {
            Swal.fire(
                'Error !!',
                'Error ' + jqXHR.status,
                'error'
            )
        }
    })
}

function HideErrorMsg() {
    $('#ErrorSelect1').hide()
    $('#ErrorSelect2').hide()
}

function PushUpdate(No, Name) {
    $.ajax({
        type: 'GET',
        url: baseUrl + '/HPPHelper/GetPraMixingLine?No=' + No,
        success: function (result) {
            if (Object.keys(result).length > 0) {
                $('#modalForm').modal()
                $('#updateform').show()
                $('#AddNew').hide()
                $('.modal-title').html(`Edit - ${Name}`)

                AppendItemNo(result.itemType, result.itemNo)

                $('#ModelWIPHeaderId').val(result.modelWIPHeaderId)
                $('#ModelWIPLineId').val(result.modelWIPLineId)
                $('#selectRate').val(result.itemType)
                //$('#ItemNo').val(result.itemNo).trigger('chosen:updated')
                $('#ItemName').val(result.itemName)
                $('#ItemQty').val(result.itemQty)
                $('#ItemUnit').val(result.itemUnit)
                $('#ProcessHour').val(result.processHour)
                $('.chosen').trigger('chosen:updated')
                $('.fg-line').addClass('fg-toggled')
                               
            } else {
                Swal.fire(
                    'Error.. !',
                    'Error 404',
                    'error'
                )
            }
        },
        error: function (jqXHR) {
            Swal.fire(
                'Error.. !',
                'Error '+jqXHR.status,
                'error'
            )
        }
       
    })
}

function PushDelete(No, Name) {
    Swal.fire({
        title: 'Warning!',
        text: 'Delete ' + Name,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya!',
        cancelButtonText: 'Kembali'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'PUT',
                url: baseUrl + '/HPPHelper/DisposePramixingLine?No=' + No,
                success: function (result) {
                    if (result == 'sukses') {
                        Swal.fire(
                            'Sukses',
                            'Data Berhasil Dihapus',
                            'success'
                        ).then((result) => {
                            window.location.reload()
                        })
                    } else {
                        Swal.fire(
                            'Error !',
                            result.result,
                            'error'
                        )
                    }
                },
                error: function (jqXHR) {
                    Swal.fire(
                        'Error !',
                        'Error ' + jqXHR.status,
                        'error'
                    )
                }
            })
        }
    })
}

function CalculateProcess() {
    $.ajax({
        type: 'GET',
        url: baseUrl + '/HPPHelper/CalculateWIPLine?No=' + $('#ModelWIPHeaderId').val(),
        success: function (result) {
            if (result == 0) {
                $('#BatchCost').val(0)
                $('#UnitCost').val(0)
            } else {
                $('#BatchCost').val((result[0].batchCost).toFixed(2))
                $('#UnitCost').val((result[0].unitCost).toFixed(2))
            }
        },
        error: function (jqXHR) {
            Swal.fire(
                'Error !',
                'Error ' + jqXHR.status,
                'error'
            )
        }
    })
}
