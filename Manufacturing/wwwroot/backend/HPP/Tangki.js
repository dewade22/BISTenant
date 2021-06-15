let baseUrl = localStorage.getItem('thisAddress')
$(function () {
    $('#TablesData').bootgrid({
        caseSensitive: false,
        formatters: {
            'commands': function (column, row) {
                return "<button type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" onclick=\"PushUpdate(\'" + row.RateNo + "\',\'" + row.RateName + "\')\"><span class=\"zmdi zmdi-edit\"></span></button>" +
                    "<button type=\"button\" class=\"btn btn-icon command-delete waves-effect waves-circle\" onclick=\"PushDelete(\'" + row.RateNo + "\',\'" + row.RateName + "\')\"><span class=\"zmdi zmdi-delete\"></span></button>"
            },
            'Price': function (column, row) {
                return `Rp. ${parseFloat(row.Price).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
            },
            'SetupCost': function (column, row) {
                return `Rp. ${parseFloat(row.SetupCost).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
            },
            'MaintenanceCost': function (column, row) {
                return `Rp. ${parseFloat(row.MaintenanceCost).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
            },
            'Salvage': function (column, row) {
                return `Rp. ${parseFloat(row.Salvage).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`
            },
            'Age': function (column, row) {
                return `${row.AgeUsed} Month`
            },
            'Capacity': function (column, row) {
                return `${row.Capacity} Liters`
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
        $('#Unit').val('')
        $('#Unit').trigger('chosen:updated')

    })

    //Simpan form Add
    $('#AddNew').click(function () {
        if ($('#formInput').valid()) {
            $.ajax({
                type: 'POST',
                url: baseUrl + '/HPPItem/RatePOST',
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
    })

    //Simpan form Update
    $('#updateform').click(function () {
        if ($('#formInput').valid()) {
            $.ajax({
                type: 'PUT',
                url: baseUrl + '/HPPItem/RateTank',
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
                }
                })
            
        }
    })
})

function Add() {
    $('#modalForm').modal()
    $('#updateform').hide()
    $('#AddNew').show()
    $('.fg-line').removeClass('fg-toggled')
    $('.cost-line').addClass('fg-toggled')
    $('.modal-title').html('Add New Tank Rates')
}

function PushUpdate(No, Name) {
    $('#modalForm').modal()
    $('#updateform').show()
    $('#AddNew').hide()
    $('.modal-title').html(`Edit - ${Name} Rates`)
    $.ajax({
        type: 'GET',
        url: baseUrl + '/HPPItem/SingleRates?No=' + No,
        success: function (result) {
            if (result != false) {
                $('#RateNo').val(result.rateNo)
                $('#RateName').val(result.rateName)
                $('#Price').val(result.price)
                $('#SetupPrice').val(result.setupPrice)
                $('#MaintenancePrice').val(result.maintenancePrice)
                $('#SalvageValue').val(result.salvageValue)
                $('#AgeUsedMonth').val(result.ageUsedMonth)
                $('#Capacity').val(result.capacity)
                $('.fg-line').addClass('fg-toggled')
            }
        }
    })
}

function PushDelete(No, Name) {
    Swal.fire({
        title: 'Warning!',
        text: 'Hapus Rate untuk ' + Name,
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
                url: baseUrl + '/HPPItem/HideRates?No=' + No,
                success: function (result) {
                    if (result == 'sukses') {
                        Swal.fire(
                            'Sukses',
                            'Rate ' + Name + ' Telah dihapus',
                            'success'
                        ).then((result) => {
                            location.reload()
                        })
                    } else {
                        Swal.fire(
                            'Error',
                            '' + result,
                            'error'
                        )
                    }
                }
            })
        }
    })
}